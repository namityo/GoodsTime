using GoodsTime.Models;
using SqlKata;
using SqlKata.Execution;

namespace GoodsTime.Context
{
    public class StocktakingEventStore : StoreBase
    {
        private const string TABLE_NAME = nameof(StocktakingEvent);

        private Query Query => QueryFactory.Query(TABLE_NAME);

        public async ValueTask<IEnumerable<StocktakingEvent>> SelectAtGoodsAsync(int id)
        {
            var result = await Query
                    .Join("StocktakingGoodsEvent", "StocktakingEvent.Id", "StocktakingGoodsEvent.StocktakingId")
                    .Where("StocktakingGoodsEvent.GoodsId", id)
                    .OrderByDesc("StocktakingEvent.CreatedAt")
                    .GetAsync<StocktakingEvent>();

            return result.ToList();
        }

        public async ValueTask<IEnumerable<StocktakingEvent>> SelectAsync()
        {
            var result = await Query
                .OrderByDesc("CreatedAt")
                .GetAsync<StocktakingEvent>();

            return result.ToList();
        }

        public async ValueTask<StocktakingEvent?> SelectAtAsync(int id)
        {
            var result = await Query
                .Where("Id", id)
                .GetAsync<StocktakingEvent>();

            return result.FirstOrDefault();
        }

        public async ValueTask AddAsync(StocktakingEvent stocktakingEvent)
        {
            await Query.InsertAsync(stocktakingEvent);
        }
    }
}
