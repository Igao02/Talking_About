using System.ComponentModel.DataAnnotations;

namespace Talking_About.Domain.Requests.Comment;

public class CreateCommentRequest : Request
{
    [Required(ErrorMessage = "Conteúdo do comentário é necessário")]
    public required string CommentContent { get; set; }
}
