namespace CwkSocialsApi.Contract.UserProfile.Responses
{
    public record UserProfileResponse
    {
        public Guid UserProfileId { get; set; }
        public BasicInformation? BasicInfo { get; set; }
    }
}
