using System.ComponentModel.DataAnnotations;

namespace AspNetCoreAngularTemplate.Infrastructure.Email.Templates;

public class EmailTemplateServiceOptions
{
    [Required] public string AppUrl { get; set; } = null!;
}
