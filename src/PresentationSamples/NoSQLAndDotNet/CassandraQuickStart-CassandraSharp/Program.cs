using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CassandraSharp;
using CassandraSharp.Commands;
using CassandraSharp.Config;

namespace CassandraQuickStart_CassandraSharp
{
    class Program
    {
        private const string KEYSPACE = "Cazunedra";
        private const string COLUMN_FAMILY = "Artists";

        static void Main(string[] args)
        {
            doSetup();

        }

        private static void doSetup()
        {
            XmlConfigurator.Configure();
            
            using (var cluster = ClusterManager.GetCluster("TestCluster"))
            {
            }
        }

    }
}
