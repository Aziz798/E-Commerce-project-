using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce.Server.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage ="First Name is required",AllowEmptyStrings =false)]
    [MinLength(3,ErrorMessage ="First Name should be at least 3 characters")]
    [MaxLength(15,ErrorMessage ="First Name shouldn't exceed 15 characters")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Last Name is required",AllowEmptyStrings = false)]
    [MinLength(3, ErrorMessage = "Last Name should be at least 3 characters")]
    [MaxLength(15, ErrorMessage = "Last Name shouldn't exceed 15 characters")]
    public string LastName { get; set; }
    [Required(ErrorMessage ="Photo is required")]
    public string UserPhoto {  get; set; }
    [Required(ErrorMessage ="Email address is required")]
    [DataType(DataType.EmailAddress,ErrorMessage ="Email address should in the proper format")]
    public string UserEmail { get; set; }
    [Required(ErrorMessage ="Password is required")]
    [MinLength(8,ErrorMessage ="Password should be at least 8 characters")]
    [JsonIgnore]
    public string Password { get; set; }
    [Required(ErrorMessage ="Confirm password is required")]
    [NotMapped]
    [Compare("Password",ErrorMessage ="Confirm Password must match password")]
    public string ConfirmPassword { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.Now;
    public DateTime UpdatedAt {  get; set; }= DateTime.Now;
    public Role Role { get; set; }


    //Navigation Properties
    public ICollection<Product> Products { get; set; }= [];

}
public enum Role
{
    User,
    Admin,
}
