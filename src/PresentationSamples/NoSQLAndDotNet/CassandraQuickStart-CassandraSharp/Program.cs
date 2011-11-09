using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CassandraSharp;
using CassandraSharp.Commands;
using CassandraSharp.Config;
using CassandraSharp.Model;
using Apache.Cassandra;

namespace CassandraQuickStart_CassandraSharp {
    class Program {
        
        private const string KEYSPACE = "demosharp";
        private const string COLUMN_FAMILY = "artists";

        private const string COMPARATOR_TYPE_UTF8 = "UTF8Type";
        private const string COMPARATOR_TYPE_TIME_UUID = "TimeUUIDType";


        private static readonly ICluster _cluster;

        static Program() {

            XmlConfigurator.Configure();
            _cluster = ClusterManager.GetCluster("TestCluster");
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
                _cluster.ExecuteCql("DROP KEYSPACE " + KEYSPACE);
            } catch {                
            }

            _cluster.ExecuteCql(@"CREATE KEYSPACE " + KEYSPACE +" WITH strategy_class='SimpleStrategy' AND strategy_options:replication_factor=1;");
            _cluster.ExecuteCql("USE " + KEYSPACE);            

        }

        private static void doColumnDef() {

            try {
                _cluster.DropColumnFamily(COLUMN_FAMILY);                
            } catch {
            }

            var cfDef = new CfDef {
                Name = COLUMN_FAMILY,
                Keyspace = KEYSPACE,
                Comparator_type = COMPARATOR_TYPE_UTF8,
                Comment = "Used for storing artist information"
            };

            _cluster.AddColumnFamily(cfDef);
        }

        private static void doInsert() {

            _cluster.Insert(COLUMN_FAMILY, new Utf8NameOrValue("The Fratellis"),
                                           new Utf8NameOrValue("Genre"),
                                           new Utf8NameOrValue("Rock"), 2, ConsistencyLevel.QUORUM);

            
        }

        private static void doQuery() {
            var genre = _cluster.Get(COLUMN_FAMILY, new Utf8NameOrValue("The Fratellis"), new Utf8NameOrValue("Genre"));
            Console.WriteLine("Genre for The Fratellis: " + Encoding.ASCII.GetString(genre.Column.Value));            
        }

        private static void doTearDown() {
            _cluster.Dispose();            
        }
    }
}
