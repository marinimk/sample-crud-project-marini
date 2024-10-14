using CRUDApp.Data;
using CRUDApp.Models;
using CRUDApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDBContext dbContext;

        public CustomerController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //to add new customer
        [HttpGet]
        public IActionResult Add()
        {
            //added a view Add.cshtml that auto renders
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddCustomerViewModel viewModel)
        {
            var customer = new Customer
            {
                Name = viewModel.Name,
                Phone = viewModel.Phone,
                Email = viewModel.Email,
                Member = viewModel.Member
            };

            await dbContext.Customer.AddAsync(customer);
            await dbContext.SaveChangesAsync(); //unsaved changes will be saved into db after this line

            return View();
        }

        [HttpGet] //async sebab nak pakai dbcontext to retrieve the list from the db
        public async Task<IActionResult> List()
        {
            var customers = await dbContext.Customer.ToListAsync();

            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await dbContext.Customer.FindAsync(id);

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer viewModel)
        {
            var customer = await dbContext.Customer.FindAsync(viewModel.Id);

            if (customer is not null)
            {
                customer.Id = viewModel.Id;
                customer.Name = viewModel.Name;
                customer.Phone = viewModel.Phone;
                customer.Email = viewModel.Email;
                customer.Member = viewModel.Member;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Customer");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Customer viewModel)
        {
            var customer = await dbContext.Customer
                .AsNoTracking() //not letting EFC to track this entitiy
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            
            if (customer is not null)
            {
                dbContext.Customer.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Customer");
        }

    }
}
