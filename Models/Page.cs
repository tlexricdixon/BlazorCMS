namespace CmsModels
{
    public class Page : SyncEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
