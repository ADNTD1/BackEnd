using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "El nombre de una marca es obligatorio")]
        public string Name { get; set; }

        // Relación 1 a muchos: una marca tiene muchos productos
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
