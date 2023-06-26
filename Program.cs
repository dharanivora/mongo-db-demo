using mongo_db_demo.Models;
using MongoDB.Bson;
using MongoDB.Driver;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("The app started running...");
        Console.WriteLine("Hello, Mongo!");

        // Connect to the db.
        var db = new MongoCRUD("AddressBook");

        // Insert data into the db.
        var person = new Person
        {
            FirstName = "Rose",
            LastName = "Tree",
            PrimaryAddress = new Address
            {
                Street = "101 Oak tree",
                City = "Toronto",
                ZipCode = "1H7 6Q0",
                Priovince = "ON",
                Country = "CA"
            }
        };

        //db.InsertDocument("Persons", person);

        // Read data from the db.
        foreach (var document in db.LoadDocuments<Person>("Persons"))
        {
            Console.WriteLine(document);
        }

        Console.WriteLine("The app finished running...");
        Console.ReadLine();
    }
}

public class MongoCRUD
{
    private readonly IMongoDatabase _db;

    public MongoCRUD(string dbName)
    {
        var client = new MongoClient("mongodb://localhost:27017");
        _db = client.GetDatabase(dbName);
    }

    // Insert record data in the db.
    public void InsertDocument<T>(string collectionName, T document)
    {
        var collection = _db.GetCollection<T>(collectionName);
        collection.InsertOne(document);
    }

    public void InsertDocuments<T>(string collectionName, IEnumerable<T> document)
    {
        var collection = _db.GetCollection<T>(collectionName);
        collection.InsertMany(document);
    }

    // Read data from the db
    public IEnumerable<T> LoadDocuments<T>(string collectionName)
    {
        var collection = _db.GetCollection<T>(collectionName);

        return collection.Find(new BsonDocument()).ToList();
    }

    // Update record in the db.
    //public void UpdateRecord<T>(string tableName, T record)
    //{

    //}




}