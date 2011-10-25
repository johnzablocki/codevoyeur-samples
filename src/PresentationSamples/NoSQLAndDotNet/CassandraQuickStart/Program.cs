using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentCassandra;
using FluentCassandra.Connections;
using Apache.Cassandra;
using FluentCassandra.Types;

namespace CassandraQuickStart {
    class Program {

        private const string KEYSPACE = "cazunedra";
        private const string COLUMN_FAMILY = "artists";

        private const string COMPARATOR_TYPE_UTF8 = "UTF8Type";
        private const string COMPARATOR_TYPE_TIME_UUID = "TimeUUIDType";


        private readonly static CassandraSession _session;
        private readonly static Server _server;
        private readonly static CassandraKeyspace _keyspace;

        static Program() {
            //_session = new CassandraSession();
            _server = new Server();
            _keyspace = new CassandraKeyspace(KEYSPACE);
        }

        static void Main(string[] args) {

            doSetup();

            doColumnFamily();

            doCRUD();

        }
        
        private static void doSetup() {
            if (CassandraSession.KeyspaceExists(_server, KEYSPACE)) {
                CassandraSession.DropKeyspace(_server, KEYSPACE);
            }

            CassandraSession.AddKeyspace(_server, new KsDef {
                            Name = KEYSPACE, 
                            Strategy_class = "org.apache.cassandra.locator.SimpleStrategy",
                            Replication_factor = 1,
                            Cf_defs = new List<CfDef>()
             });
        }

        private static void doColumnFamily() {
            var result = _keyspace.AddColumnFamily(_server, new CfDef {
                Name = COLUMN_FAMILY,
                Keyspace = KEYSPACE,
                Column_type = "Super",                
                Comparator_type = COMPARATOR_TYPE_UTF8,
                Subcomparator_type = COMPARATOR_TYPE_UTF8,
                Comment = "Used for storing artist information"                
            });            
        }

        private static void doCRUD() {
            using (var db = new CassandraContext(keyspace: KEYSPACE, server: _server)) {

                var colFamily = db.GetColumnFamily<UTF8Type, UTF8Type>(COLUMN_FAMILY);
                dynamic artist = colFamily.CreateRecord(key: "radiohead");

                dynamic artistDetails = artist.CreateSuperColumn();
                artistDetails.Genre = "Indie";

                artist.Details = artistDetails;

                db.Attach(artist);
                db.SaveChanges();

                dynamic savedArtist = colFamily.Get("radiohead").First();
                Console.WriteLine((string)savedArtist.Details.Genre);
            }
        }
        
    }
}
