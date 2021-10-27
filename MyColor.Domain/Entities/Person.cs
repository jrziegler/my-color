using MyColor.Domain.Validation;

namespace MyColor.Domain.Entities
{
    public sealed class Person : Entity
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public int ColorId { get; private set; }

        public Person() 
        { } // Necessary because of AutoMapper

        public Person(int id, string name, string lastName, string zipCode, string city, int colorId) : base(id)
        {
            ValidateDomain(name, lastName, zipCode, city, colorId);
        }

        private void ValidateDomain(string name, string lastName, string zipCode, string city, int colorId)
        { 
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name. Name is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(lastName),
                "Invalid lastname. Lastname is required.");

            DomainExceptionValidation.When(zipCode?.Length < 5,
                "Invalid zipcode. Zipcode must have at least 5 characters.");

            this.Name = name;
            this.LastName = lastName;
            this.ZipCode = zipCode;
            this.City = city;
            this.ColorId = colorId;
        }
    }
}
