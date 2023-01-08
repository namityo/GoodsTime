using SqlKata.Execution;
using SqlKata;
using GoodsTime.Models;
using Humanizer;

namespace GoodsTime.Context
{
    public class StocktakingGoodsEventStore : StoreBase
    {
        private const string TABLE_NAME = nameof(StocktakingGoodsEvent);

        private Query Query => QueryFactory.Query(TABLE_NAME);

        public StocktakingGoodsEventStore(ILogger<StocktakingGoodsEventStore> logger)
            => Logger = logger;

        public async ValueTask<IEnumerable<StocktakingGoodsEvent>> SelectAtStocktakingEventAsync(int id)
        {
            var result = await Query
                .Where("StocktakingId", id)
                .GetAsync<StocktakingGoodsEvent>();

            return result.ToList();
        }

        public async ValueTask Add(StocktakingGoodsEvent model)
        {
            await Query.InsertAsync(model);
        }
    }
}
