using CwkDomain.Aggragrate.UserProfileAggragrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CwkDomain.Aggragrate.PostAggragrate.InteractionTypee;

namespace CwkDomain.Aggragrate.PostAggragrate
{
    public class PostInteraction
    {
        private PostInteraction()
        {

        }
        public Guid InteractionId { get; private set; }
        public Guid PostId { get; private set; }
        public Guid? UserProfileId { get; private set; }
        public UserProfile UserProfile { get; set; }
        public InteractionType InteractionType { get; private set; }

        //Factories
        public static PostInteraction CreatePostInteraction(Guid postId, Guid userProfileId,
            InteractionType type)
        {
            return new PostInteraction
            {
                PostId = postId,
                UserProfileId = userProfileId,
                InteractionType = type
            };
        }
    }
}
