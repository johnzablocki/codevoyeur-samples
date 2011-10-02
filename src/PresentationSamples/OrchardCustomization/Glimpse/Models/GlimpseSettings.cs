using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;
using Orchard.ContentManagement;

namespace Glimpse.Models {
    
    public class GlimpseSettingsPartRecord : ContentPartRecord {

        public virtual bool IsEnabled { get; set; }

    }

    public class GlimpseSettingsPart : ContentPart<GlimpseSettingsPartRecord> {

        public bool IsEnabled {
            get { return Record.IsEnabled; } 
            set { Record.IsEnabled = value; }
        }
    }
}