using System;
using System.Collections.Generic;
using System.Text;
using Castle.ActiveRecord;

namespace AuditColumnsAndActiveRecord.Model
{
    [ActiveRecord("products")]
    public class Product : ActiveRecordAuditBase<Product>
    {
        private int _id;

        [PrimaryKey(PrimaryKeyType.Identity, "product_id")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        [Property("product_name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Manufacturer _manufacturer;

        [BelongsTo("manufacturer_id")]
        public Manufacturer Manufacturer
        {
            get { return _manufacturer; }
            set { _manufacturer = value; }
        }	
    }
}
