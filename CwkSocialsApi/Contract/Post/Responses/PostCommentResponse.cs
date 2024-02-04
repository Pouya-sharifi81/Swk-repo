namespace CwkSocialsApi.Contract.Post.Responses
{
    public class PostCommentResponse
    {
        public Guid CommentId { get; set; }
        public string Text { get; set; }
        public string UserProfileId { get; set; }
    }
}
