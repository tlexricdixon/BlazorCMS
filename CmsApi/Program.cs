
using CmsModels;
using DbContexts;


namespace CmsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5161", "https://localhost:7015") // Replace with your Blazor app's origin(s)
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            builder.Services.AddDbContext<LocalDbContext>();
            // Add services to the container.  
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseCors("MyAllowSpecificOrigins"); // Use the policy
            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
            //    db.Database.EnsureCreated();

            //    if (!db.Users.Any())
            //    {
            //        var user = new UserProfile { Username = "admin", Email = "admin@example.com", PasswordHash = "6" };
            //        db.Users.Add(user);

            //        var category = new Category { Name = "Tech", Slug = "tech" };
            //        db.Categories.Add(category);

            //        var tag = new Tag { Name = "C#", Slug = "csharp" }; // Fix: Use CmsModels.Tag  
            //        db.Tags.Add(tag);

            //        var post = new Post
            //        {
            //            Title = "Welcome to Your New CMS!",
            //            Content = "This is your first post. You can edit or delete it.",
            //            Category = category,
            //            Author = user.Username,
            //            Slug = "I am new here",
            //            Excerpt = "This is a brief introduction to your new CMS.",
            //            PostTags =
            //               [
            //                   new PostTag { Tag = tag }
            //               ]
            //        };
            //        db.Posts.Add(post);

            //        var page = new Page
            //        {
            //            Title = "About",
            //            Slug = "about",
            //            Content = "This site was built with love and code."
            //        };
            //        db.Pages.Add(page);

            //        db.SaveChanges();
            //    }
            //}

            // Configure the HTTP request pipeline.  
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
