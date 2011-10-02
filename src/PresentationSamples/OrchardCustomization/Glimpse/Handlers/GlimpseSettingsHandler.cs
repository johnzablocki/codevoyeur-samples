using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Glimpse.Models;

namespace Glimpse.Handlers {
    public class GlimpseSettingsHandler : ContentHandler{

        public GlimpseSettingsHandler(IRepository<GlimpseSettingsPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }

    }
}