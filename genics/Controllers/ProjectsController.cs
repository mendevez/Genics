using AutoMapper;
using genics.Data;
using genics.Dtos.Projects;
using genics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectRepository projectRepository, IMapper mapper, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // POST api/projects/
        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectCreateDto project) 
        {
            if (project == null)
                return BadRequest();
              
            var newProject = await _projectRepository.CreateProject(_mapper.Map<Project>(project));
            var projectReadDto = _mapper.Map<ProjectReadDto>(newProject);

            RequestResponse<ProjectReadDto> response = new RequestResponse<ProjectReadDto>
            { 
                Data = projectReadDto, 
                Message = "Project created successfully", 
                Success = true 
            };

            return CreatedAtAction(nameof(CreateProject), new { id = response.Data.Id }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjects();
            RequestResponse<IEnumerable<ProjectReadDto>> response = new RequestResponse<IEnumerable<ProjectReadDto>>
            {
                Data = _mapper.Map<IEnumerable<ProjectReadDto>>(projects),
                Message = "Projects fetched successfully",
                Success = true
            };
          
            return Ok(response);
        }
        
        // GET api/projects/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await  _projectRepository.GetProjectById(id);

            if(project != null)
            {
                RequestResponse<ProjectReadDto> response = new RequestResponse<ProjectReadDto>
                {
                    Data = _mapper.Map<ProjectReadDto>(project),
                    Message = "Project retrieved successfully",
                    Success = true
                };

                return Ok(response);
            }
            return NotFound(new RequestResponse<ProjectReadDto> { Data = null, Message = "Project not found", Success = false });
            
        }
        // PUT api/projects/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectUpdateDto project)
        {
            var projectToBeUpdated = await _projectRepository.GetProjectById(id);

            if( projectToBeUpdated == null)
            {
                return NotFound(new RequestResponse<ProjectReadDto> { Data = null, Message= "Project not found", Success = false}) ;
            }

            _mapper.Map(project, projectToBeUpdated);

            var result = await _projectRepository.UpdateProject(projectToBeUpdated);

            RequestResponse<ProjectReadDto> response = new RequestResponse<ProjectReadDto>
            {
                Data = _mapper.Map<ProjectReadDto>(result),
                Message = "Project updated successfully",
                Success = true
            };

            return Ok(response);
        }

        // DELETE api/projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.GetProjectById(id);

            if (project == null)
            {
                return NotFound(new RequestResponse<ProjectReadDto> { Data = null, Message = "Project not found", Success = false });
            }

            await _projectRepository.DeleteProject(project);

            return Ok(new RequestResponse<ProjectReadDto> { Data = null, Message = "Project successfully deleted", Success = true });
        }

    }
}
