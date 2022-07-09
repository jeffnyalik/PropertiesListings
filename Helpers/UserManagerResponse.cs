namespace PropertiesListings.Helpers
{
    public class UserManagerResponse
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public string? Status { get; set; }
        //public IEnumerable<string>? errors { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string?  Token {get;set;}
    }
}
