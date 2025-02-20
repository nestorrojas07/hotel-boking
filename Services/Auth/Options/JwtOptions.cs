using System.ComponentModel.DataAnnotations;

namespace Services.Auth.Options;

public class JwtOptions
{
    [Required]
    [MinLength(1, ErrorMessage = "The Jwt:Issuer setting cannot be empty.")]
    public string Issuer { get; set; } = string.Empty;
    [Required]
    [MinLength(1, ErrorMessage = "The jwt:Secret setting cannot be empty.")]
    public string Secret { get; set; } = string.Empty;
    [Required]
    [MinLength(1, ErrorMessage = "The jwt:Audience setting cannot be empty.")]
    public string Audience { get; set; } = string.Empty;
    [Required]
    public TimeSpan Expire { get; set; } = TimeSpan.FromDays(1);
}
