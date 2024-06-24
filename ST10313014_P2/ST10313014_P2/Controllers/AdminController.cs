using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST10313014_P2.Data;
using ST10313014_P2.Models;
using ST10313014_P2.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace ST10313014_P2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserOrderRepository _userOrderRepo;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IUserOrderRepository userOrderRepo, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _userOrderRepo = userOrderRepo;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> AllOrders()
        {
            var orders = await _userOrderRepo.GetAllOrdersAsync();
            return View(orders);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.ProductImage != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "ProductImages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProductImage.CopyToAsync(fileStream);
                    }
                }
                var product = new Product
                {
                    ProductName = model.ProductName,
                    ProductImage = uniqueFileName,
                    CategoryId = model.CategoryId,
                    Price = model.Price,
                    ArtistName = model.ArtistName
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = new EditProductViewModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                Price = product.Price,
                ArtistName = product.ArtistName,
                ExistingImagePath = product.ProductImage
            };

            ViewData["Categories"] = _context.Categories.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(model.ProductID);
                if (product == null)
                {
                    return NotFound();
                }

                product.ProductName = model.ProductName;
                product.CategoryId = model.CategoryId;
                product.Price = model.Price;
                product.ArtistName = model.ArtistName;

                if (model.ProductImage != null)
                {
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }

                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    product.ProductImage = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                    string newFilePath = Path.Combine(uploadsFolder, product.ProductImage);
                    using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                    {
                        await model.ProductImage.CopyToAsync(fileStream);
                    }
                }

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            ViewData["Categories"] = _context.Categories.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(product.ProductImage))
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", product.ProductImage);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatusId = 2;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AllOrders));
        }

    }
}






