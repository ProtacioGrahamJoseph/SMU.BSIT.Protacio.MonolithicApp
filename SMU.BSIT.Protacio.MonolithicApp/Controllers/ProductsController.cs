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
            try
            {
                Product product = _productService.GetById(id);
                return View(product);
            }
            catch(Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditProduct([FromQuery] int id)
        {
            try
            {
                Product product = _productService.GetById(id);
                return View(product);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult EditProduct([FromQuery] int id, Product product)
        {
            _productService.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete([FromQuery] int id)
        {
            var product = _productService.GetById(id);
            if (product != null)
            {
                _productService.DeleteProductById(id);
                TempData["ProductId"] = id;
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]


        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productService.GetById(id);
            if (product != null)
            {
                _productService.DeleteProductById(id);
                TempData["ProductId"] = id;
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
