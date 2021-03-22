using genics.Data;
using genics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _repository;


        public ProjectsController(IProjectRepository repository)
        {
            _repository = repository;

        }

        [HttpPost]
        public async Task<ActionResult<Project>> AddNewProject(Project project) 
        {
            try
            {
                if (project == null)
                    return BadRequest();

                var newProject = await _repository.AddNewProject(project);

                return CreatedAtAction(nameof(AddNewProject), new { id = newProject.Data.Id }, newProject);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Unable to add project to database");
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetAllProjects()
        {
            return Ok(await _repository.GetAllProjects());
        }
        
        // GET api/projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await  _repository.GetProjectById(id);
            if(project.Data != null)
            {
                return Ok(project);
            }
            return NotFound(new RequestResponse<Project> { Data = null, Message = "Project not found", Success = false });
            
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> UpdateProject (int id, Project project)
        {
            try
            {
                var projectToBeUpdated = await _repository.GetProjectById(id);

                if (id != project.Id)
                {
                    return BadRequest("Wrong ID");
                }

                if( projectToBeUpdated.Data == null)
                {
                    return NotFound(new RequestResponse<Project> { Data = null, Message= "Project not found", Success = false}) ;
                }

                var result = await _repository.UpdateProject(project);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating project");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var result = await _repository.DeleteProject(id);

            if (result.Data == null)
            {
                return NotFound(new RequestResponse<Project> { Data = null, Message = "Project not found", Success = false });
            }
            return Ok(result);
        }

    }
}
