using CmsModels;
using CmsMvc.Areas.Admin.Models;
using DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CmsMvc.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin/pages")]
public sealed class PagesController(LocalDbContext db) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var pages = await db.Pages
            .AsNoTracking()
            .OrderBy(page => page.Title)
            .ToListAsync(cancellationToken);

        return View(pages);
    }
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new PageCreateViewModel());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        PageCreateViewModel model,
        CancellationToken cancellationToken)
    {
        model.Slug = model.Slug.Trim().ToLowerInvariant();

        if (await db.Pages.AnyAsync(
                page => page.Slug == model.Slug,
                cancellationToken))
        {
            ModelState.AddModelError(
                nameof(model.Slug),
                "A page with this slug already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var page = new Page
        {
            Title = model.Title.Trim(),
            Slug = model.Slug,
            IsPublished = false,
            PublishedAt = null
        };

        db.Pages.Add(page);
        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction(
            nameof(Edit),
            new { id = page.Id });
    }
    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(
    int id,
    CancellationToken cancellationToken)
    {
        var page = await db.Pages
            .AsNoTracking()
            .Include(page => page.PageBlocks
                .OrderBy(block => block.SortOrder))
            .SingleOrDefaultAsync(
                page => page.Id == id,
                cancellationToken);

        return page is null
            ? NotFound()
            : View(page);
    }
}
