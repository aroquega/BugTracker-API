using System;
using System.Collections.Generic;
using BugTracker.Models;

namespace BugTracker.ViewModels
{
    public class ProjectDetailGetRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<IssueGetRequest> Issues { get; set; }
    }
}