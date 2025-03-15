using System.ComponentModel.DataAnnotations;

namespace Talking_About.Domain.Requests.Report;

public class CreateReportRequest : Request
{
    [Required(ErrorMessage = "O titulo da publicação é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome da publicação é no máximo 100 caracteres")]
    public required string ReportName { get; set; }

    [Required(ErrorMessage = "O tipo de publicação é obrigatório")]
    public required string TypeReport { get; set; }

    [Required(ErrorMessage = "O conteúdo da publicação é obrigatória")]
    [StringLength(2000, ErrorMessage = "O conteúdo é no máximo de 2000 caracteres")]
    public required string ReportDescription { get; set; }
}
