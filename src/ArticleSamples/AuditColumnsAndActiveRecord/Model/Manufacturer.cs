using System;
using System.Collections.Generic;
using System.Text;
using Castle.ActiveRecord;

namespace AuditColumnsAndActiveRecord.Model
{
    [ActiveRecord("manufacturers")]
    public class Manufacturer : ActiveRecordAuditBase<Manufacturer>
    {
        private int _id;

        [PrimaryKey(PrimaryKeyType.Identity, "manufacturer_id")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        [Property("manufacturer_name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
	
	
    }
}
