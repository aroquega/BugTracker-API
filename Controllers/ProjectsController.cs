using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly BugTrackerContext _context;

        public ProjectsController(BugTrackerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the list of projects
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectGetRequest>>> GetProjects()
        {
            return await _context.Projects
                .Select(p => new ProjectGetRequest
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Created = p.Created,
                }).ToListAsync();
        }

        /// <summary>
        /// Gets detailed information of an specific project
        /// </summary>
        /// <param name="id">the project id</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDetailGetRequest>> GetProject([FromRoute] int id)
        {
            var project = await _context.Projects
                .Select(p => new ProjectDetailGetRequest
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Created = p.Created,
                    Issues = p.Issues.Select(issue => new IssueGetRequest
                    {
                        Id = issue.Id,
                        ProjectId = issue.ProjectId,
                        Title = issue.Title,
                        Type = issue.Type,
                        Description = issue.Description,
                        Labels = issue.IssueLabels.Select(il => il.Label.Name)
                    })
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject([FromBody] ProjectPostRequest project)
        {
            var newProject = new Project
            {
                Name = project.Name,
                Description = project.Description,
                Created = DateTime.UtcNow
            };
            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new {id = newProject.Id}, newProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject([FromRoute] int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}