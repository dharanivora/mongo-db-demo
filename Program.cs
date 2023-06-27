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
        var persons = db.LoadDocuments<Person>("Persons");
        //foreach (var p in persons)
        //{
        //    Console.WriteLine(p);
        //}

        var foundPerson = db.LoadDocument<Person>("Persons", new Guid("24d8b1af-9cc3-4c87-9da8-7fcf33751e26"));

        Console.WriteLine($"Found person: {foundPerson}");

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
    public void InsertDocument<T>(string collectionName, T documents)
    {
        var collection = _db.GetCollection<T>(collectionName);
        collection.InsertOne(documents);
    }

    public void InsertDocuments<T>(string collectionName, IEnumerable<T> document)
    {
        var collection = _db.GetCollection<T>(collectionName);
        collection.InsertMany(documents);
    }

    // Read data from the db
    public IEnumerable<T> LoadDocuments<T>(string collectionName)
    {
        var collection = _db.GetCollection<T>(collectionName);

        return collection.Find(new BsonDocument()).ToList();
    }

    public T LoadDocument<T>(string collectionName, Guid id)
    {
        var collection = _db.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq("Id", id);

        return collection.Find(filter).FirstOrDefault();
    }
}