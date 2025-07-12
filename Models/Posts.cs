namespace CmsModels;
public class Post : SyncEntity
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Content { get; set; }
    public string Excerpt { get; set; }
    public DateTime PublishedAt { get; set; }
    public bool IsPublished { get; set; }
    public string Author { get; set; }

    public int? CategoryId { get; set; }
    public Category Category { get; set; }

    public List<PostTag> PostTags { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}

