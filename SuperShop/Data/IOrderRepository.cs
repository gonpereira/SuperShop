﻿using SuperShop.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string userName); //dá as encomendas todas de um determinado user

        Task<IQueryable<OrderDetailTemp>> GetDetailsTempAsync(string userName);
    }
}
