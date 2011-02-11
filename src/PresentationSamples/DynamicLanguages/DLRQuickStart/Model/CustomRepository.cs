using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TipsAndTechniques.Model {
        
    public class CustomRepository {

        private IList<Customer> _customers = new List<Customer>();

        public Customer GetCustomer(int id) {
            return _customers.FirstOrDefault(c => c.ID == id);
        }

        public void SaveCustomer(Customer c) {
            _customers.Add(c);
        }

    }
}
