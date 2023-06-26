using MongoDB.Bson.Serialization.Attributes;

public class Person
{
    [BsonId] // This attribute will keep the title "Id" instead of the default title "_id".
    public Guid Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;
}