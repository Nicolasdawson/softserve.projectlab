using System.ComponentModel.DataAnnotations;

namespace Frontend.DTO;
public class CustomerDTO
{

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password must be at least 6 characters long.")]
    public string Password { get; set; } = default!;

    [Compare("Password", ErrorMessage = "Password and confirmation different")]
    [Display(Name = "PasswordConfirm")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required Field")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password must be at least 6 characters long.")]
    public string PasswordConfirm { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string phoneNumber { get; set; } = null!;
}
