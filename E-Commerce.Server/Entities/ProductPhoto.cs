using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Server.Entities;

public class ProductPhoto
{
    [Key]
    public int Id { get; set; }
    public string PhotoPath { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.Now;
    public DateTime UpdatedAt {  get; set; }= DateTime.Now;

    //Navigation Properties
    [Required]
    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
