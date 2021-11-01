using System.ComponentModel;

namespace MyColor.Application.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }

        [DisplayName("name")]
        public string Name { get; set; }

        [DisplayName("lastname")]
        public string LastName { get;  set; }

        [DisplayName("zipcode")]
        public string ZipCode { get;  set; }

        [DisplayName("city")]
        public string City { get;  set; }

        [DisplayName("color")]
        public string Color { get;  set; }
    }
}
