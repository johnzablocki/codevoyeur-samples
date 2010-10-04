using System;
namespace MVCContribSamples.Models {
    public interface ICustomerRepository {
        System.Collections.Generic.IList<Customer> GetAll();
    }
}
