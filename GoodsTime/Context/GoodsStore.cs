using GoodsTime.Models;
using SqlKata;
using SqlKata.Execution;

namespace GoodsTime.Context
{
    public class GoodsStore : StoreBase
    {
        private const string TABLE_NAME = nameof(Goods);

        private Query Query => QueryFactory.Query(TABLE_NAME);

        public async ValueTask<IEnumerable<Goods>> Select(int releaseType = 0)
        {
            var result = await Query
                .OrderByDesc("UpdateDate")
                .Where("ReleaseFlag", releaseType)
                .GetAsync<Goods>();

            return result.ToList();
        }

        public async ValueTask<IEnumerable<Goods>> SelectAt(IEnumerable<int> ids)
        {
            var result = await Query.WhereIn("Id", ids).GetAsync<Goods>();

            return result.ToList();
        }

        public async ValueTask<Goods?> SelectAt(int id)
        {
            var result = await Query
                .Where("Id", id)
                .GetAsync<Goods>();

            return result.FirstOrDefault();
        }

        public async ValueTask Add(Goods goods)
        {
            await Query.InsertAsync(goods);
        }

        public async ValueTask<int> UpdateAt(Goods goods, string updateId)
        {
            return await Query
                .Where("Id", goods.Id)
                .Where("UpdateId", updateId)
                .UpdateAsync(goods);
        }
    }
}
