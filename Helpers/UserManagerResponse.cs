namespace PropertiesListings.Helpers
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> errors { get; set; }
    }
}
