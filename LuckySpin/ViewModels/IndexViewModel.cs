using System.ComponentModel.DataAnnotations;

namespace LuckySpin.ViewModels
{
    public class IndexViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public int LuckNumber { get; set; }
        [Required]
        public double Balance { get; set; }
    }
}
