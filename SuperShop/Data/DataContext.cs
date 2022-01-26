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

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //Habilitar a regra de apagar em Cascata (Delete in Cascade Rule)

        //protected override void OnModelCreating(ModelBuilder modelBuilder) //como está a herdar do DBcontext, fazemos um override do método em q ele cria o modelo
        //{
        //    var cascadeFKs = modelBuilder.Model
        //        .GetEntityTypes()
        //        .SelectMany(t => t.GetForeignKeys())
        //        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        //    foreach (var fk in cascadeFKs)
        //    {
        //        fk.DeleteBehavior = DeleteBehavior.Restrict;
        //    }

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
