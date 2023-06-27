using System.Data.Common;
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
        var id = new Guid("24d8b1af-9cc3-4c87-9da8-7fcf33751e27");
        var person = new Person
        {
            Id = id,
            FirstName = "Plant",
            LastName = "Green",
            Phone = "9994567890",
            PrimaryAddress = new Address
            {
                Street = "102 Oak tree",
                City = "Toronto",
                ZipCode = "1H7 6Q6",
                Province = "ON",
                Country = "CA"
            }
        };

        //db.InsertDocument("Persons", person);

        // Read data from the db.
        var persons = db.LoadDocuments<Person>("Persons");
        foreach (var p in persons)
        {
            Console.WriteLine(p);
        }

        var foundPerson = db.LoadDocument<Person>("Persons", new Guid("24d8b1af-9cc3-4c87-9da8-7fcf33751e26"));

        Console.WriteLine($"Found person: {foundPerson}");

        // Upsert data into the db.
        foundPerson.Phone = "1234567990";
        foundPerson.FirstName = "Green";

        foundPerson.PrimaryAddress ??= new Address
            {
                Province = "MB"
            };

        db.UpsertDocument("Persons", foundPerson.Id, foundPerson);

        db.UpsertDocument("Persons", id, person);

        // Delete data from the db.
        db.DeleteDocument<Person>("Persons", id);

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

    public void InsertDocuments<T>(string collectionName, IEnumerable<T> documents)
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

    public void UpsertDocument<T>(string collectionName, Guid id, T document)
    {
        var collection = _db.GetCollection<T>(collectionName);

        collection.ReplaceOne(
            filter: new BsonDocument("_id", id),
            replacement: document,
            options: new ReplaceOptions { IsUpsert = true });
    }

    public void DeleteDocument<T>(string collectionName, Guid id)
    {
        var collection = _db.GetCollection<T>(collectionName);
        collection.DeleteOne(new BsonDocument("_id", id));
    }
}