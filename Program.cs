using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

            //  Creating a new document
            var person = new Person
            {
                FirstName = "Manish",
                LastName = "T",
                City = "Pune"
            };

            //  Inserting a document to the collection
            await personsCollection.InsertOneAsync(person);

            //  Till date, there is no API in MongoDB driver to get all documents
            //  The nearest is personsCollection.FindAsync(_ => true) which returns a cursor
            //  It's easier to use the AsQueryable() which returns IQueryable<T>
            //  for Linq related operations
            var nDocuments = await personsCollection.AsQueryable().CountAsync();

            //  Queryable can be used for searching
            //  Perhaps a better approach would be to relegate it to the driver
            //  with personsCollection.FindAsync(...)
            var pDoc = await personsCollection.AsQueryable().FirstAsync(p => p.City == "Pune");

            Console.WriteLine($"Currently we have {nDocuments} documents");
            Console.WriteLine(pDoc);
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