using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Server.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage ="Product title is required",AllowEmptyStrings =false)]
    [MinLength(3,ErrorMessage ="Title must be at least 3 characters")]
    public string ProductName { get; set; }
    [Required(ErrorMessage ="Description is required",AllowEmptyStrings =false)]
    [MinLength(10,ErrorMessage ="Description should be at least 10 characters")]
    public string ProductDescription { get; set; }
    [Required(ErrorMessage ="Quantity is required",AllowEmptyStrings =false)]
    [Range(1,int.MaxValue,ErrorMessage ="Quantity should be at least 1")]
    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }= DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    //Navigation Properties
    [ForeignKey(nameof(Owner))]
    [Required]
    public int OwnerId { get; set; }
    public User? Owner { get; set; }
    public ICollection<ProductPhoto> Photos { get; set; } = [];
}
