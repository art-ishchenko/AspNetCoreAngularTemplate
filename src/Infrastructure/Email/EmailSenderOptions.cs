using System.ComponentModel.DataAnnotations;

namespace AspNetCoreAngularTemplate.Infrastructure.Email;

public class EmailSenderOptions
{
    [Required] public string Host { get; set; } = null!;
    
    [Required]
    public int Port { get; set; }

    public bool UseSsl { get; set; }

    [Required] public string Name { get; set; } = null!;

    [Required] public string Username { get; set; } = null!;

    [Required] public string EmailAddress { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;
}
