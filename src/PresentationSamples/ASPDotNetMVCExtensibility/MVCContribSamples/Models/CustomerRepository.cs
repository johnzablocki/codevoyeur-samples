using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCContribSamples.Models {
    public class CustomerRepository : ICustomerRepository {

        private List<Customer> _customers = new List<Customer>() {
                new Customer() { ID = 1, FirstName = "John", LastName="Lennon", Email="john@thebeatles.com" },
                new Customer() { ID = 2, FirstName = "Paul", LastName="McCartney", Email="paul@thebeatles.com" },
                new Customer() { ID = 3, FirstName = "George", LastName="Harrison", Email="george@thebeatles.com" },
                new Customer() { ID = 4, FirstName = "Ringo", LastName="Starr", Email="ringo@thebeatles.com" },
                new Customer() { ID = 5, FirstName = "Pete", LastName="Best" }
            };

        public IList<Customer> GetAll() {
            return _customers;
        }
    }
}
