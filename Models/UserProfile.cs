namespace CmsModels
{
    public class UserProfile : SyncEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}