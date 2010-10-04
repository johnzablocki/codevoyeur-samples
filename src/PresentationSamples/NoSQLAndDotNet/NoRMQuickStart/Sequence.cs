using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;

namespace NoRMQuickStart {
    public class Sequence {

        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

    }

    public class Product {

        public ObjectId Id { get; set; }

        public int FriendlyId { get; set; }

        public string Name { get; set; }

    }

    public class SequenceRepository {

        private object _sequenceLock = new Object();

        public int GetNextValue(string name) {

            Sequence sequence = null;
            lock (_sequenceLock) {
                using (Mongo mongo = Mongo.Create("mongodb://localhost/CodeVoyeur")) {
                    var coll = mongo.Database.GetCollection<Sequence>("Sequences");
                    sequence = coll.FindOne(new { Name = name });

                    if (sequence == null) {
                        sequence = new Sequence() { Name = name };
                    } 
                    sequence.Value += 1;
                    coll.Save(sequence);
                    return sequence.Value;
                }
            }
        }
    }

    public class ProductRepository {

        public void Create(Product product) {
            using (Mongo mongo = Mongo.Create("mongodb://localhost/CodeVoyeur")) {

                product.FriendlyId = new SequenceRepository().GetNextValue("Products");
                mongo.Database.GetCollection<Product>("Products").Insert(product);
            }
        }
    }

}
