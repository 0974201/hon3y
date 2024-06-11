using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hon3y.Models
{
    public class Inzending
    {
        public int InzendingId { get; set; }
        
        [Required (ErrorMessage = "bruh")]
        public string Voornaam { get; set; }
        
        [Required(ErrorMessage = "bruh")]
        public string Achternaam { get; set; }
        
        [Required (ErrorMessage = "bruh")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Bestand { get; set; }
    }
}
