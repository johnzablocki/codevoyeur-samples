using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HostedIronPython2.Models {
    
    public class Purchase {

        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductDescription { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public static IEnumerable<Purchase> GetPurchases(string dataSource) {

            XDocument doc = XDocument.Load(dataSource);

            var purchases = from purchase in doc.Descendants("purchase")
                            select new Purchase() {
                                Id = int.Parse(purchase.Attribute("id").Value),
                                ProductDescription = purchase.Element("product_description").Value,
                                ProductId = purchase.Element("product_id").Value,
                                Category = purchase.Element("category").Value,
                                Price = double.Parse(purchase.Element("price").Value),
                                State = purchase.Element("state").Value,
                                City = purchase.Element("city").Value
                            };

            

            return purchases;
        }

    }
}
