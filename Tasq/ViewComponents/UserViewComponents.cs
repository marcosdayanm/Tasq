using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tasq.Data; // Asegúrate de tener tus propios namespaces correctos
using System.Security.Claims;

public class UserViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public UserViewComponent(ApplicationDbContext c)
    {
        _context = c;
    }

    public async Task<IViewComponentResult>
    InvokeAsync()
    {
        var userId = HttpContext.User.Identity.IsAuthenticated ? HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value : null;
        var user = userId != null ? await _context.Users.FindAsync(userId) : null;
        return View(user);
    }
}
