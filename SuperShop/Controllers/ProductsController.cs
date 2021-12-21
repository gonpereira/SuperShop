using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data;
using SuperShop.Data.Entities;
using System.Threading.Tasks;


namespace SuperShop.Controllers
{
    public class ProductsController : Controller
    {
        //Apos criar o Repositorio...
        //private readonly DataContext _context;

        //Apos criar o Repositorio
        //public ProductsController(DataContext context)
        //{
        //    _context = context;
        //}

        //create a field repository
        //private readonly IRepository _repository;

        private readonly IProductRepository _productRepository;

        ////temos de instanciar o repository no startup para o Irepository poder correr
        //public ProductsController(IRepository repository)
        public ProductsController(IProductRepository productRepository)
        {
            //create a field repository
            //_repository = repository;
            _productRepository = productRepository;
        }



        // GET: Products
        public IActionResult Index()
        {
            //return View(_repository.GetProducts());
            return View(_productRepository.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Apos o Repositorio
            //var product = await _context.Products
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //var product = _repository.GetProduct(id.Value); //tem de ser value porque se o valor for null e pode ser pelo ? rebenda se for só                                                     id... se for id.value nao rebenta. isto é apenas para o compilador.
            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //apos repository
                //_context.Add(product);
                //_repository.AddProduct(product);
                await _productRepository.CreateAsync(product);

                //apos repositorio
                //await _context.SaveChangesAsync();
                //await _repository.SaveAllAsync();
                
                
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //apos repositorio
            //var product = await _context.Products.FindAsync(id);
            //var product = _repository.GetProduct(id.Value);

            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {   //apos repositorio
                    //_context.Update(product);
                    //await _context.SaveChangesAsync();

                    //_repository.UpdateProduct(product);
                    //await _repository.SaveAllAsync();

                    await _productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!ProductExists(product.Id))
                    //if (!_repository.ProductExists(product.Id))
                    if(!await _productRepository.ExistAsync(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Apos repositorio
            //var product = await _context.Products
            //    .FirstOrDefaultAsync(m => m.Id == id);

            //var product = _repository.GetProduct(id.Value);
            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //apos repositorio
            //var product = await _context.Products.FindAsync(id);
            //_context.Products.Remove(product);
            //await _context.SaveChangesAsync();


            //var product = _repository.GetProduct(id);
            //_repository.RemoveProduct(product);
            //await _repository.SaveAllAsync();

            var product = await _productRepository.GetByIdAsync(id);

            await _productRepository.DeleteAsync(product);
            
            
            return RedirectToAction(nameof(Index));
        }

        //apos repositorio
        //private bool ProductExists(int id)
        //{

        //    return _context.Products.Any(e => e.Id == id);

        //}
    }
}
