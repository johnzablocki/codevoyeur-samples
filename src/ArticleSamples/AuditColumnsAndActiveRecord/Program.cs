using System;
using System.Collections.Generic;
using System.Text;
using AuditColumnsAndActiveRecord.Model;
using Castle.ActiveRecord;
using System.Reflection;
using Castle.ActiveRecord.Framework.Config;

namespace AuditColumnsAndActiveRecord
{
    class Program
    {
        static Program()
        {
            ActiveRecordStarter.Initialize(
                Assembly.GetExecutingAssembly(),
                ActiveRecordSectionHandler.Instance);
        }

        static void Main(string[] args)
        {
            try
            {
                Manufacturer m = new Manufacturer();
                m.Name = "Roland";
                m.Create();

                Product p = new Product();
                p.Manufacturer = m;
                p.Name = "Juno G";
                p.Create();

                //create a different date updated
                System.Threading.Thread.Sleep(2000);

                p.Name = "Juno D 61 Key Pro Keyboard";
                p.Update();

                Product[] products = Product.FindAll();

                foreach (Product product in products)
                {
                    Console.WriteLine("{0} by {1} was last modified on {2}", 
                            product.Name, product.Manufacturer.Name, product.ModifiedDate);
                }
                            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }        
    }
}
