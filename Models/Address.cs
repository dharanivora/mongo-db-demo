using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mongo_db_demo.Models;

public class Address
{
    public string? Street { get; set; }

    public string City { get; set; } = default!;

    public string ZipCode { get; set; } = default!;
        
    public string Priovince { get; set; } = default!;

    public string Country { get; set; } = default!;

    public override string ToString() => $"{this.City}, {this.Country} ";
}