using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using IronPython.Hosting;

namespace IronPythonInversionOfControl.Core
{
    public class XmlContainer : ContainerBase
    {        
        private string _filePath = null;
        
        public XmlContainer(string filePath)
        {
            _filePath = filePath;
        }

        public override object GetObject(string objectName)
        {
            XDocument doc = XDocument.Load(_filePath);

            var objects = from obj in doc.Descendants("object")
                          where obj.Attribute("name").Value == objectName
                          select new ContainedObject
                          {
                              Name = objectName,
                              Script = obj.Value.Trim(),
                              IsStatic = bool.Parse(obj.Attribute("static").Value)
                          };

            foreach (var o in objects)
            {
                return ConstructObject(o);                
            }

            throw new ApplicationException("No object found for given name.");
        }
       
        
        #region Generics Support - not quite there yet
        public override T GetObject<T>(string objectName)
        {
            throw new NotImplementedException("Generic support has not been implmented.");
        }        

        #endregion
    }
}
