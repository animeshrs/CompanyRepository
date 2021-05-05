using System.ComponentModel.DataAnnotations;

namespace Services.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Ticker { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]{2}[A-Za-z0-9]*$", ErrorMessage = "ISIN should have first 2 letters alphanumeric")]
        public string ISIN { get; set; }
        public string Website { get; set; }
    }
}
