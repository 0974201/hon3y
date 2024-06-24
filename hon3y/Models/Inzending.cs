using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hon3y.Models
{
    public class Inzending
    {
        public int InzendingId { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Bestand { get; set; }
    }
}
