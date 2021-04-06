using System;

namespace BugTracker.ViewModels
{
    public class ProjectGetRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}