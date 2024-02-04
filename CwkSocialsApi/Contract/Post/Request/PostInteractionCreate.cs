using static CwkDomain.Aggragrate.PostAggragrate.InteractionTypee;
using System.ComponentModel.DataAnnotations;

namespace CwkSocialsApi.Contract.Post.Request
{
    public class PostInteractionCreate
    {
        [Required]
        public InteractionType Type { get; set; }
    }
}
