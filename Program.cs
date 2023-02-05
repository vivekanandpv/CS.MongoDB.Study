using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace CS.MongoDB.Study
{
    //  Create a database called mongo-sample
    //  In mongo-sample create a collection calle persons
    //  In persons collection, insert a document through MongoDb Compass
    //  with: firstName, lastName, city (all strings)
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");

            //  Getting the database
            var mongoSampleDb = client.GetDatabase("mongo-sample");

            //  Getting the collection
            var personsCollection = mongoSampleDb.GetCollection<Person>("persons");

            //  For type-safe collections, the external filters do not work; This is LINQ!
            //  Getting the cursor
            var cursor = await personsCollection.FindAsync(p => p.FirstName.StartsWith("Vivek"));

            //  Getting the first matching document
            var document = await cursor.FirstAsync();

            Console.WriteLine(document);
        }
    }

    

    //  Model class
    class Person
    {
        //  This is a document identifier. As such this is required
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        public override string ToString()
        {
            return $"FirstName: {FirstName}; LastName: {LastName}; City: {City}";
        }
    }
}