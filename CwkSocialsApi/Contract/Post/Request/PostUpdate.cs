using System.ComponentModel.DataAnnotations;

namespace CwkSocialsApi.Contract.Post.Request
{
    public class PostUpdate
    {
        [Required]
        public string Text { get; set; }
    }
}
