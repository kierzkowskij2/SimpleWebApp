using System;

namespace SimpleWebApp.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Initials { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Initials: {Initials}; Name: {Name}";
        }
    }
}
