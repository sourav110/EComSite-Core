using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;

namespace EShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.Include(x => x.ProductType).Include(y => y.SpecialTag).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes.ToList(), "Id", "TypeName");
            ViewBag.SpecialTags = new SelectList(_context.SpecialTags.ToList(), "Id", "TagName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile photo)
        {
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes.ToList(), "Id", "TypeName");
            ViewBag.SpecialTags = new SelectList(_context.SpecialTags.ToList(), "Id", "TagName");

            if (ModelState.IsValid)
            {
                //if (photo != null)
                //{
                //    var fileName = Path.Combine(_hostingEnvironment.WebRootPath + "/images", Path.GetFileName(photo.FileName));
                //    await photo.CopyToAsync(new FileStream(fileName, FileMode.Create));
                //    product.Photo = "images/" + photo.FileName;
                //}
                
                string uniqueFileName = null;

                if (photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    product.Photo = uniqueFileName;
                }

                if (photo == null)
                {
                    product.Photo = "/images/noimage.png";
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes.ToList(), "Id", "TypeName");
            ViewBag.SpecialTags = new SelectList(_context.SpecialTags.ToList(), "Id", "TagName");

            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(x => x.ProductType).Include(y => y.SpecialTag).FirstOrDefault(z=> z.Id == id);
            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile photo)
        {
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes.ToList(), "Id", "TypeName");
            ViewBag.SpecialTags = new SelectList(_context.SpecialTags.ToList(), "Id", "TagName");

            if (ModelState.IsValid)
            {
                
                string uniqueFileName = null; 

                if (photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    product.Photo = uniqueFileName;
                }

                if (photo == null)
                {
                    product.Photo = "/images/noimage.png";
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(x => x.ProductType).Include(y => y.SpecialTag).FirstOrDefault(z=> z.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Products.Include(x => x.ProductType).Include(y => y.SpecialTag).FirstOrDefault(z => z.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}