using CwkDomain.Aggragrate.UserProfileAggragrate;
using CwkDomain.Exeption;
using CwkDomain.Validator.PostValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkDomain.Aggragrate.PostAggragrate
{
    public class PostModel
    {


        private readonly List<PostComment> _comments = new List<PostComment>();
        private readonly List<PostInteraction> _interactions = new List<PostInteraction>();
        private PostModel()
        {
        }
        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public string TextContent { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }
        public IEnumerable<PostComment> Comments { get { return _comments; } }
        public IEnumerable<PostInteraction> Interactions { get { return _interactions; } }
        public static PostModel CreatePost(Guid userProfileId, string textContent)
        {
            var validator = new PostValidator();
            var objectValid= new PostModel
            {
                UserProfileId = userProfileId,
                TextContent = textContent,
                CreatedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
            };
            var validationResult = validator.Validate(objectValid);
            if (validationResult.IsValid) return objectValid;
            var exception = new PostCommentNotValidException("Post comment is not valid");

            validationResult.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));
            throw exception;


        }
        public void UpdatePostText(string newText)
        {
            if (string.IsNullOrWhiteSpace(newText))
            {
                var exeption = new PostNotValidException("can not update post" + "post text is not valid");
                exeption.ValidationErrors.Add("this post is emty or have space ");
                throw exeption;
            }
            TextContent = newText;
            LastModified = DateTime.UtcNow;
        }

        public void AddPostComment(PostComment newComment)
        {
            _comments.Add(newComment);
        }

        public void RemoveComment(PostComment toRemove)
        {
            _comments.Remove(toRemove);
        }

        public void UpdatePostComment(Guid postCommentId, string updatedComment)
        {
            var comment = _comments.FirstOrDefault(c => c.CommentId == postCommentId);
            if (comment != null && !string.IsNullOrWhiteSpace(updatedComment))
                comment.UpdateCommentText(updatedComment);
        }

        public void AddInteraction(PostInteraction newInteraction)
        {
            _interactions.Add(newInteraction);
        }

        public void RemoveInteraction(PostInteraction toRemove)
        {
            _interactions.Remove(toRemove);
        }

    }
}

