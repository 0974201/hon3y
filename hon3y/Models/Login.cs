using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hon3y.Models
{
    public class Login
    {
        public int LoginId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; } //geen salt/hash toevoegen voor extra veiligheid
    }
}