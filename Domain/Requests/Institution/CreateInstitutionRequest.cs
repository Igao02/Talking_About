using System.ComponentModel.DataAnnotations;
using Talking_About.Domain.Constants;

namespace Talking_About.Domain.Requests.Institution;

public class CreateInstitutionRequest : Request
{
    [Required(ErrorMessage = "O nome da corporação é obrigatório")]
    [StringLength(LenghtConst.MaxName, ErrorMessage = "O máximo de caracteres é 150")]
    public string CorporateName { get; set; } = "";

    [Required(ErrorMessage = "O documento é obrigatório")]
    [StringLength(LenghtConst.MaxDocNumber, ErrorMessage = "O máximo de caracteres é 14")]
    public string Document { get; set; } = "";

    [Required(ErrorMessage = "O CEP e obrigatório")]
    [StringLength(LenghtConst.MaxCep, ErrorMessage = "O máximo de caracteres é 10")]
    public string Cep { get; set; } = "";

    [Required(ErrorMessage = "A cidade é obrigatória")]
    [StringLength(LenghtConst.MaxName, ErrorMessage = "O máximo de caracteres é 150")]
    public string City { get; set; } = "";

    [Required(ErrorMessage = "A rua é obrigatória")]
    [StringLength(LenghtConst.MaxAddName, ErrorMessage = "O máximo de caracteres é 150")]
    public string Street { get; set; } = "";

    [Required(ErrorMessage = "O bairro é obrigatório")]
    [StringLength(LenghtConst.MaxName, ErrorMessage = "O máximo de caracteres é 150")]
    public string Neighborhood { get; set; } = "";

    [Required(ErrorMessage = "A sigla do estado é obrigatório")]
    [StringLength(LenghtConst.NumUf, ErrorMessage = "O máximo de caracteres é 2")]
    public string Uf { get; set; } = "";

    [Required(ErrorMessage = "O número do endereço é obrigatório")]
    public int NumHome { get; set; } = 0;

    [StringLength(LenghtConst.MaxName, ErrorMessage = "O máximo de caracteres é 150")]
    public string? Complement { get; set; } = "";
}
