using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MvcContrib.Pagination;
using MVCContribSamples.Models;

namespace MVCContribSamples.Controllers
{
    public class UIHelpersController : Controller
    {
        private ICustomerRepository _customerRepository { get; set; }

        public UIHelpersController() {
            _customerRepository = new CustomerRepository();
        }

        //
        // GET: /UIHelper/
        public UIHelpersController(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        public ActionResult FluentHtml()
        {
            return View();
        }

        public ActionResult Grid(int? page) {
            
            return View(_customerRepository.GetAll().AsPagination(page ?? 1, 2));
            
        }

    }
}
