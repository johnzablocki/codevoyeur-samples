using System;
using System.Collections.Generic;
using System.Text;
using Castle.ActiveRecord;

namespace AuditColumnsAndActiveRecord.Model
{
    [ActiveRecord]
    public abstract class ActiveRecordAuditBase<T> : ActiveRecordBase<T>
    {        
        private string _createdUser;

        [Property("created_user", Update=false, Access=PropertyAccess.NosetterCamelcaseUnderscore)]
        public string CreatedUser
        {
            get { return _createdUser; }            
        }

        private string _modifiedUser;

        [Property("modified_user", Insert = false, Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public string ModifiedUser
        {
            get { return _modifiedUser; }
        }

        private DateTime _createdDate;

        [Property("created_date", Update = false, Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime CreatedDate
        {
            get { return _createdDate; }            
        }

        private DateTime? _modifiedDate;

        [Property("modified_date", Insert = false, Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? ModifiedDate
        {
            get { return _modifiedDate; }
        }  

        public override void Update()
        {
            auditUpdate();
            base.Update();
        }

        public override void Create()
        {
            auditCreate();
            base.Create();
        }

        public override void Save()
        {
            auditCreate();
            auditUpdate();
            base.Save();
        }

        public override void CreateAndFlush()
        {

            auditCreate();
            base.CreateAndFlush();
        }

        public override void UpdateAndFlush()
        {
            auditUpdate();
            base.UpdateAndFlush();
        }

        public override object SaveCopy()
        {
            auditCreate();
            auditUpdate();
            return base.SaveCopy();
        }

        public override object SaveCopyAndFlush()
        {
            auditCreate();
            auditUpdate();
            return base.SaveCopyAndFlush();
        }

        private void auditUpdate()
        {
            _modifiedDate = DateTime.Now;
            _modifiedUser = Environment.UserName;
        }

        private void auditCreate()
        {
            _createdDate = DateTime.Now;
            _createdUser = Environment.UserName;
        }
        
    }
}
