using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie1.DAL;
using Zadanie1.Models;

namespace Zadanie1
{
    public class TestController : Controller
    {
        private readonly AppDbContext _db;

        public TestController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Customers.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                await _db.Customers.AddAsync(customer);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            CustomerModel? customer = await _db.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
            if (customer is not null)
            {
                return View(customer);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CustomerModel customer)
        {
            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CustomerModel? customer = await _db.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
            if (customer is not null)
            {
                return View(customer);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerModel editedCustomer)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(editedCustomer).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return BadRequest();
        }
    }
}