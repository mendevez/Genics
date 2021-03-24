using AutoMapper;
using genics.Dtos.Projects;
using genics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Profiles
{
    public class ProjectProfile : Profile
    {

        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadDto>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();
        }
    }
}
