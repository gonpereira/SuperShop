using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperShop.Data.Entities;

namespace SuperShop.Data
{   
    //Extract Interface
    //public class Repository : IRepository
    //{
    //    //private readonly DataContext _context;

    //    //construtor
    //    public Repository(DataContext context)
    //    {
    //        _context = context;
    //    }

    //    //metodo que mostra todos os produtos
    //    public IEnumerable<Product> GetProducts()
    //    {
    //        return _context.Products.OrderBy(p => p.Name);
    //    }

    //    //metodo que mostra um só produto
    //    public Product GetProduct(int id)
    //    {
    //        return _context.Products.Find(id);
    //    }

    //    public void AddProduct(Product product)
    //    {
    //        _context.Products.Add(product);
    //    }

    //    public void UpdateProduct(Product product)
    //    {
    //        _context.Products.Update(product);
    //    }

    //    public void RemoveProduct(Product product)
    //    {
    //        _context.Products.Remove(product);
    //    }


    //    public async Task<bool> SaveAllAsync()
    //    {
    //        return await _context.SaveChangesAsync() > 0;
    //        //Grava tudo o que estiver pendente na BD
    //        //a entity framework é que faz esta gestão
    //    }

    //    //vai apenas saber se ele lá está... não precisa de ir buscar
    //    public bool ProductExists(int id)
    //    {
    //        return _context.Products.Any(p => p.Id == id);
    //    }
    //}
}
