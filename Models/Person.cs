using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace mongo_db_demo.Models;

public class Person
{
    [BsonId] // This attribute will keep the title "Id" instead of the default title "_id".
    public Guid Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string? Phone { get; set; }

    public Address PrimaryAddress { get; set; } = default!;

    public override string ToString()
    {
        var result = new StringBuilder($"{this.FirstName} {this.LastName}");
        
        if (this.PrimaryAddress != null)
        {
            result.Append($", {this.PrimaryAddress}");
        }

        return result.ToString();
    }
}