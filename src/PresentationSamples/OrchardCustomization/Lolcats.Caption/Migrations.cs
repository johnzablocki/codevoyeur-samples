using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Lolcats.Caption {
    public class Migrations : DataMigrationImpl {

        public int Create() {

            SchemaBuilder.CreateTable("KittehPartRecord", table => table
                .ContentPartRecord()
                .Column("ImageSource", DbType.String)
                .Column("TextTop", DbType.String)
                .Column("TextMiddle", DbType.String)
                .Column("TextBottom", DbType.String)
                );
            return 1;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterPartDefinition("KittehPartRecord",
              builder => builder.Attachable());
            return 2;
        }

        public int UpdateFrom2() {

            ContentDefinitionManager.AlterTypeDefinition("KittehWidget", cfg => cfg
                .WithPart("KittehPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 3;
        }
    }
}