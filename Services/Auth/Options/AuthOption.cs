using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth.Options;

public class AuthOption
{
    [Required]
    [MinLength(1, ErrorMessage = "The Auth:PasswordSalt setting cannot be empty.")]
    public string PasswordSalt { get; set; }
}
