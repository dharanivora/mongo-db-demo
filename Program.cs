using MongoDB.Driver;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, Mongo!");

        // Connect to the db.
        var db = new MongoCRUD("AddressBook");

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

}