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
        db.InsertRecord("Persons", new Person
        {
            FirstName = "Cherry",
            LastName = "Blueberry"
        });

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

    // Create data in the db.
    public void InsertRecord<T>(string tableName, T record)
    {
        var collection = _db.GetCollection<T>(tableName);
        collection.InsertOne(record);
    }



}