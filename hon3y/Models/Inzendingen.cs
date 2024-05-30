using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hon3y.Models
{
    public class Inzendingen
    {
        public int UploadId { get; set; }
        
        [Required]
        public string Voornaam { get; set; }
        
        [Required]
        public string Achternaam { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Bestand { get; set; }
    }
}
