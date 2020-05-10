
namespace WebApp.Models.Entities
{
    public class UserRole  
    {
        public int Id { get; set; }     
        public string IdentityId { get; set; }   
        
        public AppUser Identity { get; set; }
        public string Location {get;set;}
    }
}