using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models.Entities;

namespace WebApp.Controllers
{
    public class UserProjectController : BaseController<UserProject>
    {
        public UserProjectController(ApplicationDbContext db) : base(db)
        {
            _db = _appDbContext.UserProject;
        }
    }
}