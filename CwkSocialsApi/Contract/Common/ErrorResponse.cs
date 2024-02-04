namespace CwkSocialsApi.Contract.Common
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Statusphrase { get; set; }
        public List<string> Errors { get; } = new List<string>();
        public DateTime TimeStamp { get; set; }

    }
}
