using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using AuditColumnsAndActiveRecord.Model;
using Castle.ActiveRecord;
using System.Reflection;
using Castle.ActiveRecord.Framework.Config;

namespace AuditColumnsAndActiveRecord.Tests
{
    [TestFixture]
    public class ActiveRecordAuditBaseTestFixture
    {
        static ActiveRecordAuditBaseTestFixture()
        {
            ActiveRecordStarter.Initialize(
                Assembly.GetExecutingAssembly(),
                ActiveRecordSectionHandler.Instance);
        }


        [TestFixtureTearDown]
        [TestFixtureSetUp]
        public void ClearDB()
        {
            try
            {
                Product.DeleteAll();                
                Manufacturer.DeleteAll();                
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.GetBaseException().Message);
                throw;
            }
        }
        
        
        [Test]
        public void CreateAndFindLeavesModifiedColumnsNull()
        {
            Product p = getProduct();

            p.Manufacturer.Create();
            p.Create();

            Product savedProduct = Product.Find(p.Id);
            Manufacturer savedManufacturer = Manufacturer.Find(p.Manufacturer.Id);

            Assert.IsNull(savedProduct.ModifiedUser, "Product modified user is not null.");
            Assert.IsNull(savedManufacturer.ModifiedUser, "Manufacturer modified user is not null.");
            Assert.IsNull(savedProduct.ModifiedDate, "Product modified date is not null.");
            Assert.IsNull(savedManufacturer.ModifiedDate, "Manufacturer modified date is not null.");            
        }

        [Test]
        public void SaveLeavesModifiedColumnsNullWhenCreating()
        {
            Product p = getProduct();

            p.Manufacturer.Save();
            p.Save();

            Product savedProduct = Product.Find(p.Id);
            Manufacturer savedManufacturer = Manufacturer.Find(p.Manufacturer.Id);

            Assert.IsNull(savedProduct.ModifiedUser, "Product modified user is not null.");
            Assert.IsNull(savedManufacturer.ModifiedUser, "Manufacturer modified user is not null.");
            Assert.IsNull(savedProduct.ModifiedDate, "Product modified date is not null.");
            Assert.IsNull(savedManufacturer.ModifiedDate, "Manufacturer modified date is not null.");
        }

        [Test]
        public void UpdatePopulatesModifiedColumns()
        {
            Product p = getProduct();

            p.Manufacturer.Create();
            p.Create();

            p.Manufacturer.Name = "Roland USA";
            p.Manufacturer.Update();

            p.Name = "Juno-G";
            p.Update();

            Product savedProduct = Product.Find(p.Id);
            Manufacturer savedManufacturer = Manufacturer.Find(p.Manufacturer.Id);
            
            Assert.IsNotNull(savedProduct.ModifiedUser, "Product modified user is null.");
            Assert.IsNotNull(savedManufacturer.ModifiedUser, "Manufacturer modified user is null.");
            Assert.IsNotNull(savedProduct.ModifiedDate, "Product modified date is null.");
            Assert.IsNotNull(savedManufacturer.ModifiedDate, "Manufacturer modified date is null.");
        }

        [Test]
        public void SavePopulatesModifiedColumnsWhenUpdating()
        {
            Product p = getProduct();

            p.Manufacturer.Create();
            p.Create();

            p.Manufacturer.Name = "Roland USA";
            p.Manufacturer.Save();

            p.Name = "Juno-G";
            p.Save();

            Product savedProduct = Product.Find(p.Id);
            Manufacturer savedManufacturer = Manufacturer.Find(p.Manufacturer.Id);

            Assert.IsNotNull(savedProduct.ModifiedUser, "Product modified user is null.");
            Assert.IsNotNull(savedManufacturer.ModifiedUser, "Manufacturer modified user is null.");
            Assert.IsNotNull(savedProduct.ModifiedDate, "Product modified date is null.");
            Assert.IsNotNull(savedManufacturer.ModifiedDate, "Manufacturer modified date is null.");
        }


        private Product getProduct()
        {            
            Product p = new Product();
            p.Manufacturer = getManufacturer();
            p.Name = "Juno G";

            return p;
        }

        private Manufacturer getManufacturer()
        {
            Manufacturer m = new Manufacturer();
            m.Name = "Roland";

            return m;
        }
    }
}
