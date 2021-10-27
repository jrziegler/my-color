using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyColor.Application.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength (100)]
        [DisplayName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [MaxLength(100)]
        [DisplayName("lastname")]
        public string LastName { get;  set; }

        [MaxLength(10)]
        [DisplayName("zipcode")]
        public string ZipCode { get;  set; }

        [MaxLength(100)]
        [DisplayName("city")]
        public string City { get;  set; }

        [DisplayName("color")]
        public string Color { get;  set; }
    }
}
