using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zadanie1.Models
{
    [Table(name: "Klienci")]
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50)]
        public string Surename { get; set; }

        [Required]
        [MinLength(11), MaxLength(11)]
        public String PESEL { get; set; }

        [Required] 
        public int BirthYear { get; set; }
        public Plec Płeć { get; set; }
    }
}

public enum Plec
{
    Mezczyzna = 0,
    Kobieta = 1
}