using System.Collections.Generic;

namespace BugTracker.Models
{
    public class IssueGetRequest
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; }
        public IEnumerable<string> Labels { get; set; }
    }
}