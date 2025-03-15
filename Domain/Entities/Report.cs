using Talking_About.DomainCore.Entities;

namespace Talking_About.Domain.Entities;

public class Report : Entity
{
    public Report()
    {
        //ORM Purpose
    }

    public string ReportName { get; set; }

    public string TypeReport { get; set; }

    public string ReportDescription { get; set; }

    public string UserName { get; set; }
    public bool? IsEvent { get; set; } = true;
    public DateTime ReportsDate { get; set; } = DateTime.Now;

    public virtual List<Comment> Comments { get; set; } = new List<Comment>();

    public virtual List<Like> Likes { get; set; } = new List<Like>();

    public virtual List<Image> Images { get; set; } = new List<Image>();

    public Report(string reportName, string typeReport, string reportDescription, DateTime reportDate, string userName, bool? isEvent) : base()
    {
        ReportName = reportName;
        TypeReport = typeReport;
        ReportDescription = reportDescription;
        ReportsDate = reportDate;
        UserName = userName;
        IsEvent = isEvent;
    }
}
