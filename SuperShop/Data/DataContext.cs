using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        /*Esta classe, vai ser a ponte de ligação entre a base de dados e as classes em Entity. 
         Ela herda da IdentityDbContext (Microsoft.EntityFrameWorkCore) as várias propriedades
        para fazer o conversion. Tal é a simplicidade que nesta classe só fazemos isto. Depois
        trabalhamos na consola (Package Manager Console) 
        */



        public DbSet<Product> Products { get; set; } //isto é a propriedade que vai criar a tabela

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderDetailTemp> OrderDetailTemps { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
