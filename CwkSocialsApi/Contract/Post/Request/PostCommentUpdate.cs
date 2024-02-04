using System.ComponentModel.DataAnnotations;

namespace CwkSocialsApi.Contract.Post.Request
{
    public class PostCommentUpdate
    {
        [Required]
        public string Text { get; set; }
    }
}
