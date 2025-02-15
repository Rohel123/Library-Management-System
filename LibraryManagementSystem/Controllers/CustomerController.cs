﻿using LibraryManagementSystem.Data.Interfaces;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookRepository _bookRepository;

        public CustomerController(ICustomerRepository customerRepository, IBookRepository bookRepository)
        {
            _customerRepository = customerRepository;
            _bookRepository = bookRepository;
        }

        [Route("Customer")]
        public IActionResult List()
        {
            var customerVM = new List<CustomerViewModel>();

            var customers = _customerRepository.GetAll();

            if (customers.Count() == 0)
            {
                return View("Empty");
            }

            foreach (var customer in customers)
            {
                customerVM.Add(new CustomerViewModel
                {
                    Customer = customer,
                    BookCount = _bookRepository.Count(x=> x.BorrowerId== customer.CustomerId)
                });
            }

            return View(customerVM);
        }

        public IActionResult Delete(int id)
        {
            var customer = _customerRepository.GetById(id);

            _customerRepository.Delete(customer);

            return RedirectToAction("List");
        }

    }    
}
