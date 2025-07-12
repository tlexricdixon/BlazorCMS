using CmsApi.Controllers;
using CmsModels;
using Microsoft.AspNetCore.Mvc;

namespace CmsTests;

public class PostControllerTests
{
    [Fact]
    public void Get_ReturnsEmptyCollection_WhenNoPostsExist()
    {
        // Arrange
        var controller = new PostController();

        // Act
        var result = controller.Get();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Create_AssignsId_AndReturnsCreatedPost()
    {
        // Arrange
        var controller = new PostController();
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
        var controller = new PostController();
        var post1 = new Post { Title = "Title 1", Content = "Content 1", Author = "Author 1" };
        var post2 = new Post { Title = "Title 2", Content = "Content 2", Author = "Author 2" };

        // Act
        controller.Create(post1);
        controller.Create(post2);
        var result = controller.Get();

        // Assert
        //post.create creates one in the controller, so we expect 2 + 1 = 3 posts
        var posts = Assert.IsAssignableFrom<IEnumerable<Post>>(result);
        Assert.Equal(2, posts.Count());
    }

    [Fact]
    public void Create_AssignsSequentialIds_ToMultiplePosts()
    {
        // Arrange
        var controller = new PostController();
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
