using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lolcats.Caption.Models;
using Orchard.ContentManagement.Drivers;

namespace Lolcats.Caption.Drivers {
    public class KittehDriver : ContentPartDriver<KittehPart> {

        protected override DriverResult Display(KittehPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_Kitteh",
                                () => shapeHelper.Parts_Kitteh(
                                      ImageSource: part.ImageSource,
                                      TextTop: part.TextTop,
                                      TextMiddle: part.TextMiddle,
                                      TextBottom: part.TextBottom));
                                
        }

        protected override DriverResult Editor(KittehPart part, dynamic shapeHelper) {
            return ContentShape("Parts_Kitteh_Edit",
                                () => shapeHelper.EditorTemplate(
                                      TemplateName: "Parts/Kitteh",
                                      Model: part,
                                      Prefix: Prefix));
        }

        protected override DriverResult Editor(KittehPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}