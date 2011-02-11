using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExtensions.Controllers;

namespace MvcExtensions.Core {
    public class PigLatinControllerFactory : IControllerFactory {
        
        #region IControllerFactory Members

        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName) {

            string controllerClass = pigLatinToEnglish(requestContext.RouteData.Values["controller"].ToString());
            requestContext.RouteData.Values["controller"] = controllerClass;

            string controllerAction = pigLatinToEnglish(requestContext.RouteData.Values["action"].ToString());
            requestContext.RouteData.Values["action"] = controllerAction;

            IController controller = Type.GetType(string.Format("MvcExtensions.Controllers.{0}Controller", controllerClass), true, true)
                                        .GetConstructor(Type.EmptyTypes).Invoke(null) as IController;
            
            return controller;
        }

        public void ReleaseController(IController controller) {
            
        }

        #endregion

        private string pigLatinToEnglish(string textInPigLatin) {
            int firstLetterPosition = textInPigLatin.Length-3;
            
            //ignore rules about multiple consonants and starts with vowel
            return textInPigLatin[firstLetterPosition] +  textInPigLatin.Substring(0, firstLetterPosition);            
        }
    }
}
