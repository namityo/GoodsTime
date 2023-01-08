﻿using GoodsTime.Models;
using SqlKata;
using SqlKata.Execution;

namespace GoodsTime.Context
{
    public class GoodsStore : StoreBase
    {
        private const string TABLE_NAME = nameof(Goods);

        private Query Query => QueryFactory.Query(TABLE_NAME);

        public async ValueTask<IEnumerable<Goods>> SelectAsync(int releaseType = 0)
        {
            var result = await Query
                .OrderByDesc("UpdateDate")
                .Where("ReleaseFlag", releaseType)
                .GetAsync<Goods>();

            return result.ToList();
        }

        public async ValueTask<IEnumerable<Goods>> SelectAtAsync(IEnumerable<int> ids)
        {
            var result = await Query.WhereIn("Id", ids).GetAsync<Goods>();

            return result.ToList();
        }

        public async ValueTask<Goods?> SelectAtAsync(int id)
        {
            var result = await Query
                .Where("Id", id)
                .GetAsync<Goods>();

            return result.FirstOrDefault();
        }

        public async ValueTask AddAsync(Goods goods)
        {
            await Query.InsertAsync(goods);
        }

        public async ValueTask<int> UpdateAtAsync(Goods goods, string updateId)
        {
            return await Query
                .Where("Id", goods.Id)
                .Where("UpdateId", updateId)
                .UpdateAsync(goods);
        }
    }
}