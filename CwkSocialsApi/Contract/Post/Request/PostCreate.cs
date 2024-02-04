using System.ComponentModel.DataAnnotations;

namespace CwkSocialsApi.Contract.Post.Request
{
    public class PostCreate
    {
        public string UserProfileId { get; set; }
        [Required]

        public string TextContent { get; set; }
    }
}
