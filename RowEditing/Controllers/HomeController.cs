using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RowEditing.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RowEditing.Controllers
{
    public class HomeController : Controller
    {
        public static List<Customer> customers;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            customers = new List<Customer>();
            Customer customer;

            for (int i = 0; i < 5; i++)
            {
                customer = new Customer { 
                Id = i,
                Name = "customer "+ i.ToString(),
                Role = "customer Role "+ i.ToString()
                };
                customers.Add(customer);
            }
        }

        public IActionResult Index()
        {
            
            CustomersViewModel model = new CustomersViewModel();
            model.Customers = customers;
            model.SelectedCustomer = null;
            return View(model);
            
        }

        [HttpPost]
        public ActionResult New()
        {
            
                CustomersViewModel model = new CustomersViewModel();
                model.Customers = customers.OrderBy(
                               m => m.Id).Take(5).ToList();
                model.SelectedCustomer = null;
                model.DisplayMode = "WriteOnly";
                return View("Index", model);
            
        }
        [HttpPost]
        public ActionResult Insert(Customer obj)
        {
            
                customers.Add(obj);
                

                CustomersViewModel model = new CustomersViewModel();
                model.Customers = customers.OrderBy(
                                 m => m.Id).Take(5).ToList();
            model.SelectedCustomer = customers.Find(x => x.Id == obj.Id);
                model.DisplayMode = "ReadOnly";
                return View("Index", model);
            
        }

        [HttpPost]
        public ActionResult Select(string id)
        {
            
                CustomersViewModel model = new CustomersViewModel();
                model.Customers = customers.OrderBy(
                            m => m.Id).Take(5).ToList();
                model.SelectedCustomer = customers.Find(x => x.Id.ToString() == id);
            model.DisplayMode = "ReadOnly";
                return View("Index", model);
            
        }
        [HttpPost]
        public ActionResult Edit(string id)
        {
             
                CustomersViewModel model = new CustomersViewModel();
                model.Customers = customers.OrderBy(
                                m => m.Id).Take(5).ToList();
                model.SelectedCustomer = customers.Find(x => x.Id.ToString() == id);
            model.DisplayMode = "ReadWrite";
                return View("Index", model);
            
        }

        [HttpPost]
        public ActionResult Update(Customer obj)
        {
             
                Customer existing = customers.Find(x => x.Id == obj.Id);
            existing.Id = obj.Id;
                existing.Name = obj.Name;
                existing.Role = obj.Role;
                

                CustomersViewModel model = new CustomersViewModel();
                model.Customers = customers.OrderBy(
                              m => m.Id).Take(5).ToList();

                model.SelectedCustomer = existing;
                model.DisplayMode = "ReadOnly";
                return View("Index", model);
            
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            
                Customer existing = customers.Find(x => x.Id.ToString() == id);
            customers.Remove(existing);
                

                CustomersViewModel model = new CustomersViewModel();
                model.Customers = customers.OrderBy(
                                  m => m.Id).Take(5).ToList();

                model.SelectedCustomer = null;
                model.DisplayMode = "";
                return View("Index", model);
            
        }

        [HttpPost]
        public ActionResult Cancel(string id)
        {
             
                CustomersViewModel model = new CustomersViewModel();
                model.Customers = customers.OrderBy(
                                  m => m.Id).Take(5).ToList();
                model.SelectedCustomer = customers.Find(x => x.Id.ToString() == id);
            model.DisplayMode = "ReadOnly";
                return View("Index", model);
            
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
