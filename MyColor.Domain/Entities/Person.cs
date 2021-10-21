using MyColor.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyColor.Domain.Entities
{
    public sealed class Person : Entity
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string Color { get; private set; }

        public Person(int id, string name, string lastName, string zipCode, string city, string color) : base(id)
        {
            ValidateDomain(name, lastName, zipCode, city, color);
        }

        private void ValidateDomain(string name, string lastName, string zipCode, string city, string color)
        {
            //TODO: the name and lastname can be null?  
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name.Name is required");

            DomainExceptionValidation.When(string.IsNullOrEmpty(lastName),
                "Invalid lastname. Lastname is required");

            if(!string.IsNullOrEmpty(zipCode))
                DomainExceptionValidation.When(zipCode.Length < 5,
                   "Invalid zipcode, too short, minimum 5 characters");

            this.Name = name;
            this.LastName = lastName;
            this.ZipCode = zipCode;
            this.City = city;
            this.Color = color;
        }
    }
}
