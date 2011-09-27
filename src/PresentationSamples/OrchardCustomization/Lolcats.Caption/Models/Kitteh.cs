using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;
using Orchard.ContentManagement;
using System.ComponentModel.DataAnnotations;

namespace Lolcats.Caption.Models {
    
    public class KittehPartRecord : ContentPartRecord {

        public virtual string ImageSource { get; set; }

        public virtual string TextTop { get; set; }

        public virtual string TextMiddle { get; set; }

        public virtual string TextBottom { get; set; }

    }

    public class KittehPart : ContentPart<KittehPartRecord> {

        [Required]
        public string ImageSource {
            get { return Record.ImageSource; }
            set { Record.ImageSource = value; }
        }

        [Required]
        public string TextTop {
            get { return Record.TextTop; }
            set { Record.TextTop = value; }
        }

        public string TextMiddle {
            get { return Record.TextMiddle; }
            set { Record.TextMiddle = value; }
        }

        [Required]
        public string TextBottom {
            get { return Record.TextBottom; }
            set { Record.TextBottom = value; }
        }

    }
}