using Talking_About.Domain.Constants;
using Talking_About.DomainCore.Entities;
using System.ComponentModel.DataAnnotations;

public class Institution : Entity
{
    public Institution()
    {
        //ORM Purpose
    }

    [Required(ErrorMessage = "O nome da corpora��o � obrigat�rio")]
    [StringLength(LenghtConst.MaxName, ErrorMessage = "O m�ximo de caracteres � 150")]
    public string CorporateName { get; set; } = "";

    [Required(ErrorMessage = "O documento � obrigat�rio")]
    [StringLength(LenghtConst.MaxDocNumber, ErrorMessage = "O m�ximo de caracteres � 14")]
    public string Document { get; set; } = "";

    [Required(ErrorMessage = "O CEP e obrigat�rio")]
    [StringLength(LenghtConst.MaxCep, ErrorMessage = "O m�ximo de caracteres � 10")]
    public string Cep { get; set; } = "";

    [Required(ErrorMessage = "A cidade � obrigat�ria")]
    [StringLength(LenghtConst.MaxName, ErrorMessage = "O m�ximo de caracteres � 150")]
    public string City { get; set; } = "";

    [Required(ErrorMessage = "A rua � obrigat�ria")]
    [StringLength(LenghtConst.MaxAddName, ErrorMessage = "O m�ximo de caracteres � 150")]
    public string Street { get; set; } = "";

    [Required(ErrorMessage = "O bairro � obrigat�rio")]
    [StringLength(LenghtConst.MaxName, ErrorMessage = "O m�ximo de caracteres � 150")]
    public string Neighborhood { get; set; } = "";

    [Required(ErrorMessage = "A sigla do estado � obrigat�rio")]
    [StringLength(LenghtConst.NumUf, ErrorMessage = "O m�ximo de caracteres � 2")]
    public string Uf { get; set; } = "";

    [Required(ErrorMessage = "O n�mero do endere�o � obrigat�rio")]
    public int NumHome { get; set; } = 0;

    [StringLength(LenghtConst.MaxName, ErrorMessage = "O m�ximo de caracteres � 150")]
    public string? Complement { get; set; } = "";

    public DateTime? CreationDate { get; set; } = DateTime.Now;

    public string UserName { get; set; }

    public Institution(string corporateName, string document, string cep, string street, int numHome, string? complement, DateTime? creationDate, string userName, string neighborhood, string uf) : base()
    {
        CorporateName = corporateName;
        Document = document;
        Cep = cep;
        Street = street;
        NumHome = numHome;
        CreationDate = creationDate;
        UserName = userName;
        Complement = complement;
        Neighborhood = neighborhood;
        Uf = uf;
    }
}