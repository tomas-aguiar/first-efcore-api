using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstEFCore.Api.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Required field")]
        [MinLength(3, ErrorMessage = "Field must contain at least 3 characters")]
        [MaxLength(64, ErrorMessage = "Field must contain less than 64 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Required field")]
        public decimal Value { get; set; }
    }
}