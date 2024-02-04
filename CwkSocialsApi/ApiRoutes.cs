namespace CwkSocialsApi
{
    public class ApiRoutes
    {
        public const string BaseRoutes = "api/v{version:apiVersion}/[controller]";
        public static class UserProfiles
        {
            public const string IdRoute = "{id}";
        }
        public static class Post
        {
            public const string GetById = "{id}";
            public const string PostComments = "{postId}/comments";
            public const string CommentById = "{postId}/comments/{commentId}";
            public const string InteractionById = "{postId}/interactions/{interactionId}";
            public const string PostInteractions = "{postId}/interactions";
        }
        public static class Identity
        {
            public const string Login = "login";
            public const string Registration = "registration";
            public const string IdentityById = "{identityUserId}";
            public const string CurrentUser = "currentuser";
        }
    }
}