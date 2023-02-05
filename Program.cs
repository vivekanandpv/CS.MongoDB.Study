using MongoDB.Bson;
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
            var personsCollection = mongoSampleDb.GetCollection<BsonDocument>("persons");

            //  Creating a filter
            var filter = Builders<BsonDocument>.Filter.Eq("city", "Bengaluru");
            
            //  Getting the cursor
            var cursor = await personsCollection.FindAsync(filter);

            //  Getting the first matching document
            var document = await cursor.FirstAsync();

            Console.WriteLine(document);
        }
    }
}