namespace BlikkBasicReplica.Models.Auth
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public string UserId { get; set; }
        public string Token{ get; set; }
        public bool Success { get; set; }
    }
}
