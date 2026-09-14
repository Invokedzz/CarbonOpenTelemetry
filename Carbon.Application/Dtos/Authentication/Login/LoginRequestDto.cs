using System.ComponentModel.DataAnnotations;

namespace Carbon.Application.Dtos.Authentication.Login;

public record LoginRequestDto(
    [EmailAddress] 
    [Required(ErrorMessage = "Email field is required!", AllowEmptyStrings = false)]
    string Email,
    
    [StringLength(maximumLength: 40, MinimumLength = 10)]
    [Required(ErrorMessage = "Password field is required!", AllowEmptyStrings = false)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]+$",
        ErrorMessage = "Password must contain letters, digits and special characters!")]
    string Password);