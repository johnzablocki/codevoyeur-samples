using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Handlers;
using Lolcats.Caption.Models;
using Orchard.Data;

namespace Lolcats.Caption.Handlers {
    public class KittehHandler : ContentHandler {
        public KittehHandler(IRepository<KittehPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}