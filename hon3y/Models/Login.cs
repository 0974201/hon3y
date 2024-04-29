using System.ComponentModel.DataAnnotations;

namespace hon3y.Models
{
    public class Login
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
