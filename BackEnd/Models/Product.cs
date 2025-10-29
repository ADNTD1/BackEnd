using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BackEnd.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int BrandId { get; set; }  // foreign key de la tabla de brands 

    [ForeignKey("BrandId")]
    public virtual Brand Brand { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int Stock { get; set; }

    public decimal Price { get; set; }

    public string ImageUrl { get; set; }
}
