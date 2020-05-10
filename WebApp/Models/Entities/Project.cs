using System;
using WebApp.Controllers;

namespace WebApp.Models.Entities
{
    public class Project : IHaveId
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime LastChangeDateTime { get; set; }
        
        public string DbConnectionString { get; set; }
        
        public string DisplayName { get; set; }
        
        public bool IsRunning { get; set; }
        
        public string RepositoryLink { get; set; }
        
        public string RepositoryUserName { get; set; }
        
        public string RepositoryPassword { get; set; }
        
        public AppUser Creator { get; set; }

        public string MetaFileName { get; set; }
        
    }
}