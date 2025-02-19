using Microsoft.AspNetCore.Mvc;
using SMU.BSIT.Protacio.MonolithicApp.Models;
using SMU.BSIT.Protacio.MonolithicApp.Services;

namespace SMU.BSIT.Protacio.MonolithicApp.Controllers
{
    public class ProductsController : Controller
    {
        private ProductServices _productService;
        public ProductsController()
        {
            _productService = new ProductServices();
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            var viewModel = new ProductsViewModel();

            ViewBag.ProductId = TempData["ProductId"]?.ToString();
            return View();
        }

        public IActionResult SubmitProduct(Product product)
        {
            _productService.Add(product);
            TempData["ProductId"] = product.Id;
            return RedirectToAction("Index");
        }

        public IActionResult Details([FromQuery] int id)
        {
            var product = _productService.GetById(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit([FromQuery] int id)
        {
            var product = _productService.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit([FromQuery] int id, Product product)
        {
            product.Id = id;
            _productService.Update(product);
            return RedirectToAction("Details", new { id = product.Id });
        }

        public IActionResult Delete([FromQuery] int id)
        {
            var product = _productService.GetById(id);
            if (product != null)
            {
                _productService.Delete(id);
                TempData["ProductId"] = id;
            }
            return RedirectToAction("Index");
        }
    }
}
