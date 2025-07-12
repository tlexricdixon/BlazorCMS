namespace CmsModels
{
    public class Comment : SyncEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Content { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsApproved { get; set; }
    }
}
