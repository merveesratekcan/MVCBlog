using HS14MVC.Applicationn.Services.ArticleServices;
using HS14MVC.Applicationn.Services.AuthorServices;
using HS14MVC.UI.Areas.Author.Models.ArticleVMs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HS14MVC.UI.Areas.Author.Controllers
{
    public class HomeController : AuthorBaseController
    {
        private readonly IAuthorService _authorService;
        private readonly IArticleService _articleService;

        public HomeController(IAuthorService authorService, IArticleService articleService)
        {
            _authorService = authorService;
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var authorId = await _authorService.GetAuthorIdByIdentityId(userId);
            var result = await _articleService.GetAllAsync(authorId);

            if (!result.IsSuccess)
            {NotifiyError(result.Messages);
             
                return View(new List<ArticleListVM>());
            }

            NotifiySuccess(result.Messages);
            return View(result.Data.Adapt<List<ArticleListVM>>());

        }
     
    }
}
