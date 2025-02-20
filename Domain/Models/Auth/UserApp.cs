using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Enums.Auth;

namespace Domain.Models.Auth;

public class UserApp
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
