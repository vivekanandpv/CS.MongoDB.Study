using MongoDB.Driver;

namespace CS.MongoDB.Study
{
    //  Install MongoDB.Driver package
    internal class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");

            //  List of databases
            var dbList = client.ListDatabases().ToList();

            //  enumerating
            foreach (var document in dbList)
            {
                Console.WriteLine(document);
            }
        }
    }
}