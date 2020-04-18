using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SpecialTagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var specialTags = _context.SpecialTags.ToList();
            return View(specialTags);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _context.SpecialTags.Add(specialTag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specialTag);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _context.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _context.Update(specialTag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specialTag);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _context.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = _context.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Remove(specialTag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specialTag);
        }
    }
}