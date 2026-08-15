using System.ComponentModel.DataAnnotations;

namespace E_Commerce_iti.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
    }
}