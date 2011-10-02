using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glimpse.Models;
using Orchard.ContentManagement.Drivers;

namespace Glimpse.Drivers {
    public class GlimpseDriver : ContentPartDriver<GlimpseSettingsPart> {

        protected override DriverResult Editor(GlimpseSettingsPart part, dynamic shapeHelper) {
            return ContentShape("Parts_GlimpseSettingsPart_Edit",
                                () => shapeHelper.EditorTemplate(
                                      TemplateName: "Parts/GlimpseSettings",
                                      Model: part,
                                      Prefix: Prefix));
        }

        protected override DriverResult Editor(GlimpseSettingsPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

    }

}