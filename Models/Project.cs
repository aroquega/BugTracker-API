using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required] [StringLength(130)] public string Name { get; set; }
        [StringLength(400)] public string Description { get; set; }
        [DataType(DataType.Date)] public DateTime Created { get; set; }
        public IEnumerable<Issue> Issues { get; set; }
    }
}