using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using SuperShop.Models;
using System;
using System.IO;
using System.Linq;
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
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        ////temos de instanciar o repository no startup para o Irepository poder correr
        //public ProductsController(IRepository repository)
        public ProductsController(IProductRepository productRepository, IUserHelper userHelper, IBlobHelper blobHelper, IConverterHelper converterHelper)
        {
            //create a field repository
            //_repository = repository;
            _productRepository = productRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }



        // GET: Products
        public IActionResult Index()
        {
            //return View(_repository.GetProducts());
            return View(_productRepository.GetAll().OrderBy(p => p.Name));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            //Apos o Repositorio
            //var product = await _context.Products
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //var product = _repository.GetProduct(id.Value); //tem de ser value porque se o valor for null e pode ser pelo ? rebenda se for só                                                     id... se for id.value nao rebenta. isto é apenas para o compilador.
            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
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
        //public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                //var path = string.Empty;

                if(model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    //path = await _imageHelper.UploadImageAsync(model.ImageFile, "products");
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
                }

                var product = _converterHelper.ToProduct(model, imageId, true);

                //TODO: Change to the logged User
                product.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                //apos repository
                //_context.Add(product);
                //_repository.AddProduct(product);
                await _productRepository.CreateAsync(product);

                //apos repositorio
                //await _context.SaveChangesAsync();
                //await _repository.SaveAllAsync();
                
                
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //private Product ToProduct(ProductViewModel model, string path)
        //{
        //    return new Product
        //    {
        //        Id = model.Id,
        //        ImageUrl = path,
        //        IsAvailable = model.IsAvailable,
        //        LastPurchase = model.LastPurchase,
        //        LastSale = model.LastSale,
        //        Name = model.Name,
        //        Price = model.Price,
        //        Stock = model.Stock,
        //        User = model.User
        //    };
        //}

        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            //apos repositorio
            //var product = await _context.Products.FindAsync(id);
            //var product = _repository.GetProduct(id.Value);

            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }
            
            var model = _converterHelper.ToProductViewModel(product); 

            return View(model);
        }

        //private ProductViewModel ToProducViewModel(Product product)
        //{
        //    return new ProductViewModel
        //    {
        //        Id = product.Id,
        //        IsAvailable = product.IsAvailable,
        //        LastPurchase = product.LastPurchase,
        //        LastSale = product.LastSale,
        //        ImageUrl = product.ImageUrl,
        //        Name = product.Name,
        //        Price = product.Price,
        //        Stock = product.Stock,
        //        User = product.User
        //    };
        //}

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Edit(/*int id,*/ ProductViewModel model)
        {
            //if (id != product.Id)
            //{
            //    return NotFound();
            //}

            //agora já passamos o Id na view do EDIT portanto podemos tirar o que está em cima

            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;
                    //var path = model.ImageUrl;

                    if(model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
                    }         

                    var product = _converterHelper.ToProduct(model, imageId, false);

                    //TODO: Change to the logged User
                    product.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                    //apos repositorio
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
                    if(!await _productRepository.ExistAsync(model.Id))
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
            return View(model);
        }

        // GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }
            //Apos repositorio
            //var product = await _context.Products
            //    .FirstOrDefaultAsync(m => m.Id == id);

            //var product = _repository.GetProduct(id.Value);
            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize(Roles = "Admin")]
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

            try
            {
                await _productRepository.DeleteAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if(ex is DbUpdateException) //(ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"Problably the item {product.Name} is in use... ";
                    ViewBag.ErrorMessage = $"</br></br>{product.Name} cant be delete! There are orders with that item included! </br>";
                }           

                return View("Error");
            }                                 

        }

        //apos repositorio
        //private bool ProductExists(int id)
        //{

        //    return _context.Products.Any(e => e.Id == id);

        //}

        public IActionResult ProductNotFound()
        {
            return View();
        }
    }
}
