using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCContribSamples.Models {
    public class AlternateCustomerRepository : ICustomerRepository {

         private List<Customer> _customers = new List<Customer>() {
                new Customer() { ID = 1, FirstName = "John", LastName="Jones", Email="johnj@ledzeppelin.com" },
                new Customer() { ID = 2, FirstName = "Jimmy", LastName="Page", Email="jimmy@ledzeppelin.com" },
                new Customer() { ID = 3, FirstName = "Robert", LastName="Plant", Email="robert@ledzeppelin.com" },
                new Customer() { ID = 4, FirstName = "John", LastName="Bonham", Email="johnb@ledzeppelin.com" },                
            };

        public IList<Customer> GetAll() {
            return _customers;
        }
    }
}
