using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EShop.Data;
using EShop.Models;

namespace EShop.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var productTypes = _context.ProductTypes.ToList();
            return View(productTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.ProductTypes.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productType);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var productType = _context.ProductTypes.Find(id);
            if(productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.Update(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productType);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _context.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = _context.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Remove(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productType);
        }


    }
}