using Microsoft.AspNetCore.Identity;
using Talking_About.Domain.Entities;

namespace Talking_About.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public List<Report> Reports { get; set; } = new List<Report>();

    public List<Comment> Comments { get; set; } = new List<Comment>();

    public List<Like> Likes { get; set; } = new List<Like>();

    public List<Institution> Institutions { get; set; } = new List<Institution>();
}
