using DiGi.GIS.PostgreSQL.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class Building2DPostgreSQLManager : IGISPostgreSQLObject
    {
        private readonly Building2DPostgreSQLConverter? building2DPostgreSQLConverter;
        private CancellationTokenSource? cancellationTokenSource;

        public Building2DPostgreSQLManager(Building2DPostgreSQLConverter building2DPostgreSQLConverter)
        {
            this.building2DPostgreSQLConverter = building2DPostgreSQLConverter;
        }

        public Building2DPostgreSQLRefreshOptions Building2DPostgreSQLRefreshOptions { get; set; } = new Building2DPostgreSQLRefreshOptions();

        public bool IsRunning => cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested;

        public void Start()
        {
            if (IsRunning || building2DPostgreSQLConverter is null)
            {
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                try
                {
                    await building2DPostgreSQLConverter.RefreshAsync(Building2DPostgreSQLRefreshOptions, cancellationToken: cancellationTokenSource.Token);
                }
                finally
                {
                    cancellationTokenSource = null;
                }
            }, cancellationTokenSource.Token);
        }

        public void Stop()
        {
            cancellationTokenSource?.Cancel();
        }
    }
}