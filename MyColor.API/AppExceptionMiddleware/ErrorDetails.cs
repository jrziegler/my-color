using System.ComponentModel;
using System.Text.Json;

namespace MyColor.API.AppExceptionMiddleware
{
    public sealed class ErrorDetails
    {
        [DisplayName("status")]
        public int Status { get; set; }

        [DisplayName("message")]
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
