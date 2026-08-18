using DiGi.Core.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.PostgreSQL.Classes
{
    /// <summary>
    /// Abstract base class for referenced objects associated with 2D buildings, providing county and identification properties.
    /// <para><b>How a row is addressed.</b> Data belonging to a building is not keyed by one column but by two levels:</para>
    /// <para>1. <c>(CountyId, Reference)</c> identifies the <b>building</b>, and therefore the whole set of objects held for it. <c>Reference</c> is the BDOT10k <c>ot:lokalnyId</c> of the building; <c>CountyId</c> is the <c>id</c> of the county row in <c>administrative_areal_2d</c> - the identifier, never the county code, which maps to one row per polygon part. <c>building_2d</c> is the source of truth for the pair and constrains it <c>UNIQUE (reference, county_id)</c>, so the combination is nationally unique.</para>
    /// <para>2. <c>UniqueId</c> identifies <b>one stored object within that set</b>. It carries the identifier of the object itself - for a <see cref="Core.Classes.GuidObject"/> its guid - not the reference of the building it describes.</para>
    /// <para><b>A building may hold several rows.</b> Occupancy computed on different days or under different rules belongs to the same building as several records, so the table constrains <c>UNIQUE (county_id, unique_id)</c> and deliberately places no constraint on <c>(county_id, reference)</c>. Writes therefore append: storing an object that was built fresh adds a row rather than replacing one.</para>
    /// <para><b>Consequences for callers.</b> Read everything held for a building with <c>GetItemsByReferenceAsync(reference, countyId)</c>; the singular <c>GetItemByReferenceAsync</c> returns only the most recently stored of them. Delete everything held for a building with <c>RemoveAsync(references, countyId)</c>. Update a single object by reading the set, finding the one of interest, removing it with <c>RemoveByUniqueIdsAsync(uniqueIds, reference, countyId)</c> and writing its replacement - there is no upsert that targets it.</para>
    /// <para><b>Do not key a row on the reference</b> to make writes idempotent. It reads like a fix for a table that grows on every run, but it pins the table to one row per building and silently discards the second and later records the design is there to hold.</para>
    /// </summary>
    /// <typeparam name="TUniqueObject">The type of the unique object this referenced object points to.</typeparam>
    public abstract class Building2DReferencedObject<TUniqueObject> : ReferencedObject<TUniqueObject> where TUniqueObject : IUniqueObject
    {
        /// <summary>
        /// Initializes a new instance of the Building2DReferencedObject class from a JsonObject.
        /// </summary>
        /// <param name="jsonObject">The JsonObject containing the serialized data.</param>
        public Building2DReferencedObject(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Building2DReferencedObject class by copying data from another Building2DReferencedObject instance.
        /// </summary>
        /// <param name="building2DReferencedObject">The Building2DReferencedObject instance to copy data from.</param>
        public Building2DReferencedObject(Building2DReferencedObject<TUniqueObject>? building2DReferencedObject)
            : base(building2DReferencedObject)
        {
            if (building2DReferencedObject is not null)
            {
                CountyId = building2DReferencedObject.CountyId;
                Id = building2DReferencedObject.Id;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Building2DReferencedObject class.
        /// </summary>
        public Building2DReferencedObject()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the county ID associated with this referenced object.
        /// </summary>
        [JsonInclude, JsonPropertyName("CountyId")]
        public int? CountyId { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of this referenced object.
        /// </summary>
        [JsonInclude, JsonPropertyName("Id")]
        public long Id { get; set; }
    }
}