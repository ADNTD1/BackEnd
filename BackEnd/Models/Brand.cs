using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }

        public string Name { get; set; }

        // Relación 1 a muchos: una marca tiene muchos productos
        public virtual ICollection<Product> Products { get; set; }
    }
}
