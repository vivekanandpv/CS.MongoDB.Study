using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CS.MongoDB.Study
{
    //  In the mongo-sample database, create a new collection called books
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");

            //  Getting the database
            var mongoSampleDb = client.GetDatabase("mongo-sample");

            //  Getting the collection
            var booksCollections = mongoSampleDb.GetCollection<Book>("books");

            //  Creating a complex aggregate
            var book = new Book
            {
                Title = "The C# Programming Language",
                Authors = new[] { "Anders Heijlsberg", "Eric Lippert" },
                Dimensions = new Dimensions
                {
                    Length = 160,
                    Width = 130,
                    Thickness = 45
                },
                Pages = 800,
                Price = 928.50,
                YearOfPublication = 2010
            };

            await booksCollections.InsertOneAsync(book);

            //  _id is allocated
            Console.WriteLine(book._id);

            var cursor = await booksCollections.FindAsync(b => b._id == book._id);
            
            //  returns the newly created book
            var bookDb = await cursor.FirstAsync();
            
            //  Updating. Take a look at the result
            var result = await booksCollections.UpdateOneAsync(
                b => b._id == book._id, 
                Builders<Book>.Update.Set(b => b.Pages, 900)
                );
        }
    }

    

    //  Model class
    class Book
    {
        //  This is a document identifier. As such this is required
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("pages")]
        public int Pages { get; set; }

        [BsonElement("authors")]
        public string[] Authors { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("yearOfPublication")]
        public int YearOfPublication { get; set; }

        [BsonElement("dimensions")]
        public Dimensions Dimensions { get; set; }
    }

    public class Dimensions
    {
        [BsonElement("length")]
        public int Length { get; set; }

        [BsonElement("width")]
        public int Width { get; set; }

        [BsonElement("thickness")]
        public int Thickness { get; set; }
    }
}