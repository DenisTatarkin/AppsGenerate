using WebApp.Controllers;

namespace WebApp.Models.Entities
{
    public class UserProject : IHaveId
    {
        public AppUser Participant { get; set; }
        
        public Project Project { get; set; }
        
        public long Id { get; set; }
    }
}