using Talking_About.DomainCore.Entities;

namespace Talking_About.Domain.Entities;

public class Like : Entity
{
    public Like()
    {
        //ORM Purpose
    }

    public DateTime? LikeDate { get; set; } = DateTime.Now;

    public string UserName { get; set; }

    public virtual Guid ReportId { get; set; }

    public virtual Report Report { get; set; }

    public Like(DateTime? likeDate, string userName, Guid reportId) : base()
    {
        LikeDate = likeDate;
        UserName = userName;
        ReportId = reportId;
    }
}
