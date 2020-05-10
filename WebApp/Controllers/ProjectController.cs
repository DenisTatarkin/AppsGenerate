using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models.Entities;

namespace WebApp.Controllers
{
    public class ProjectController : BaseController<Project>
    {
        public ProjectController(ApplicationDbContext db) : base(db)
        {
            _db = _appDbContext.Project;
        }
    }
}