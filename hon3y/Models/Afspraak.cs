using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hon3y.Models
{
    public class Afspraak
    {
        public int AfspraakId { get; set; }
        
        [Required]
        public string Voornaam { get; set; }
        
        [Required]
        public string Achternaam { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        public int? Telefoonnummer { get; set; }
        
        [Required]
        public string AfspraakReden { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? Datum { get; set; }
    }
}
