using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using HostedIronPython2.Models;
using HostedIronPython2.Core;

namespace HostedIronPython2 {

    public class Foo {
        public string Size { get; set; }
        public string Bar { get; set; }
    }

    class Program {

        static void Main(string[] args) {

            try {

                PyRunner runner = new PyRunner();
                runner.Load("sales_tax_rules.py");

                IEnumerable<Purchase> purchases = Purchase.GetPurchases("Data.xml");
                foreach (var purchase in purchases) {
                    Console.WriteLine("Executing {0} purchase for state {1}", purchase.Category, purchase.State);
                    Console.WriteLine("\tOriginal Price: ${0:##.##}", purchase.Price);
                    runner.Execute(purchase.State.ToLower(), purchase);
                    Console.WriteLine("\tPrice with State Sales Tax ${0:##.##}\r\n", purchase.Price);
                }

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);
            }

        }     
    }
}
