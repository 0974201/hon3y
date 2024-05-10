using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hon3y.Models
{
    public class Logs
    {
        public int LogId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Datum { get; set; }

        //json??? of txt file direct opslaan
    }
}
