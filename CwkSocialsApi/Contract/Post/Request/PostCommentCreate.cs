using System.ComponentModel.DataAnnotations;

namespace CwkSocialsApi.Contract.Post.Request
{
    public class PostCommentCreate
    {
        [Required]
        public string Text { get; set; }
        public string userProfileId { get; set; }
    }
}
