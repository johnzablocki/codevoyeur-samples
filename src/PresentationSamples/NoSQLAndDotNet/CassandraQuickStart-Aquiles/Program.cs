using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.Cassandra;
using Aquiles.Cassandra10;
using Aquiles.Core.Cluster;
using Aquiles.Helpers.Encoders;
using Aquiles.Helpers;

namespace CassandraQuickStart_Aquiles {
    class Program {

        private const string KEYSPACE = "foosharp";
        private const string COLUMN_FAMILY = "artists";

        private const string COMPARATOR_TYPE_UTF8 = "UTF8Type";
        private const string COMPARATOR_TYPE_TIME_UUID = "TimeUUIDType";


        private static readonly ICluster _cluster;

        static Program() {

            AquilesHelper.Initialize();
            _cluster = AquilesHelper.RetrieveCluster("TestCluster");
        }

        static void Main(string[] args) {

            doSetUp();

            doColumnDef();

            doInsert();

            doQuery();

            doTearDown();
        }


        private static void doSetUp() {

            try {
                _cluster.Execute(new ExecutionBlock(c => { c.system_drop_keyspace(KEYSPACE); return null; }));
            } catch {
            }

            var ksDef = new KsDef {
                            Name = KEYSPACE, 
                            Strategy_class = "org.apache.cassandra.locator.SimpleStrategy",
                            Replication_factor = 1,
                            Cf_defs = new List<CfDef>() };

            _cluster.Execute(new ExecutionBlock(c => { c.system_add_keyspace(ksDef); return null; }));
            _cluster.Execute(new ExecutionBlock(c => { c.set_keyspace(KEYSPACE); return null; }));
        }

        private static void doColumnDef() {

            try {
                _cluster.Execute(new ExecutionBlock(c => { c.system_drop_column_family(COLUMN_FAMILY); return null; }));
            } catch {
            }

            var cfDef = new CfDef {
                Name = COLUMN_FAMILY,
                Keyspace = KEYSPACE,
                Comparator_type = COMPARATOR_TYPE_UTF8,
                Comment = "Used for storing artist information"
            };

            _cluster.Execute(new ExecutionBlock(c => { c.set_keyspace(KEYSPACE);  c.system_add_column_family(cfDef); return null; }));

        }

        private static void doInsert() {

            ColumnParent columnParent = new ColumnParent();
            Column column = new Column() {
                Name = ByteEncoderHelper.UTF8Encoder.ToByteArray("Genre"),
                Timestamp = UnixHelper.UnixTimestamp,
                Value = ByteEncoderHelper.UTF8Encoder.ToByteArray("Folk"),
            };
            columnParent.Column_family = COLUMN_FAMILY;
            
            _cluster.Execute(new ExecutionBlock(c => { 
                c.insert(Encoding.UTF8.GetBytes("The Decemberists"), columnParent, column, ConsistencyLevel.ONE);
                return null;
            }), KEYSPACE);
        }

        private static void doQuery() {
            var key = Encoding.ASCII.GetBytes("The Decemberists");
            ColumnPath columnPath = new ColumnPath() {
                Column = ByteEncoderHelper.UTF8Encoder.ToByteArray("Genre"),
                Column_family = COLUMN_FAMILY,
            };
            var retVal = _cluster.Execute(new ExecutionBlock(c => {
                return c.get(key, columnPath, ConsistencyLevel.ONE);
            }), KEYSPACE);

            Console.WriteLine("Genre for The Decemberists: " + Encoding.ASCII.GetString(((ColumnOrSuperColumn)retVal).Column.Value));            

        }

        private static void doTearDown() {

        }
    }
}
