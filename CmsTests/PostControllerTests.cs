using CmsApi.Controllers;
using CmsModels;
using DbContexts;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CmsTests;

public class PostControllerTests
{
    private PostController CreateControllerWithMockedContext()
    {
        var mockContext = new Mock<LocalDbContext>();
        return new PostController(mockContext.Object);
    }

    [Fact]
    public void Get_ReturnsEmptyCollection_WhenNoPostsExist()
    {
        // Arrange
        var controller = CreateControllerWithMockedContext();

        // Act
        var result = controller.Get();

        // Assert
        var okResult = Assert.IsType<ActionResult<IEnumerable<Post>>>(result);
        var posts = Assert.IsAssignableFrom<IEnumerable<Post>>(okResult.Value);
        Assert.Empty(posts);
    }

    [Fact]
    public void Create_AssignsId_AndReturnsCreatedPost()
    {
        // Arrange
        var controller = CreateControllerWithMockedContext();
        var post = new Post
        {
            Title = "Test Title",
            Content = "Test Content",
            Author = "Test Author"
        };

        // Act
        var actionResult = controller.Create(post);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var returnedPost = Assert.IsType<Post>(okResult.Value);
        Assert.Equal(1, returnedPost.Id);
        Assert.Equal("Test Title", returnedPost.Title);
        Assert.Equal("Test Content", returnedPost.Content);
        Assert.Equal("Test Author", returnedPost.Author);
    }

    [Fact]
    public void Get_ReturnsAllPosts_AfterCreation()
    {
        // Arrange
        var controller = CreateControllerWithMockedContext();
        var post1 = new Post { Title = "Title 1", Content = "Content 1", Author = "Author 1" };
        var post2 = new Post { Title = "Title 2", Content = "Content 2", Author = "Author 2" };

        // Act
        controller.Create(post1);
        controller.Create(post2);
        var result = controller.Get();

        // Assert
        var okResult = Assert.IsType<ActionResult<IEnumerable<Post>>>(result);
        var posts = Assert.IsAssignableFrom<IEnumerable<Post>>(okResult.Value);
        Assert.Equal(2, posts.Count());
    }

    [Fact]
    public void Create_AssignsSequentialIds_ToMultiplePosts()
    {
        // Arrange
        var controller = CreateControllerWithMockedContext();
        var post1 = new Post { Title = "Title 1", Content = "Content 1", Author = "Author 1" };
        var post2 = new Post { Title = "Title 2", Content = "Content 2", Author = "Author 2" };

        // Act
        var result1 = controller.Create(post1);
        var result2 = controller.Create(post2);

        // Assert
        var okResult1 = Assert.IsType<OkObjectResult>(result1);
        var returnedPost1 = Assert.IsType<Post>(okResult1.Value);

        var okResult2 = Assert.IsType<OkObjectResult>(result2);
        var returnedPost2 = Assert.IsType<Post>(okResult2.Value);

        Assert.Equal(1, returnedPost1.Id);
        Assert.Equal(2, returnedPost2.Id);
    }
}
