using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.Cassandra;
using Aquiles.Cassandra10;

namespace CassandraQuickStart_Aquiles
{
    class Program
    {
        private const string KEYSPACE = "cazunedra";

        static void Main(string[] args)
        {
            doSetup();
        }

        private static void doSetup()
        {
            AquilesHelper.Initialize();

            var cluster = AquilesHelper.RetrieveCluster("TestCluster");

            var keyspaceDef = new KsDef();
            keyspaceDef.Name = "Cazunedra";

            var result = cluster.Execute(new ExecutionBlock(c => c.recv_system_drop_keyspace()));


        }
    }
}
