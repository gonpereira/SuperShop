using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class DataContext : DbContext
    {

        public DbSet<Product> Products { get; set; } //isto é a propriedade que vai fazer a tabela

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
