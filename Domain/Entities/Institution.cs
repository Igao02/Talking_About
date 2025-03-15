using Talking_About.Domain.Constants;
using Talking_About.DomainCore.Entities;
using System.ComponentModel.DataAnnotations;

public class Institution : Entity
{
    public Institution()
    {
        //ORM Purpose
    }

    public string CorporateName { get; set; } = "";

    public string Document { get; set; } = "";

    public string Cep { get; set; } = "";

    public string City { get; set; } = "";

    public string Street { get; set; } = "";

    public string Neighborhood { get; set; } = "";

    public string Uf { get; set; } = "";

    public int NumHome { get; set; } = 0;

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