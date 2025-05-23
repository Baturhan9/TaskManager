using AutoMapper;
using TaskManager.Models;
using TaskManager.Models.CreateModelObjects;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Api.Classes
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Attachment, AttachmentDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<TaskManager.Models.Task, TaskDTO>().ReverseMap();
            CreateMap<TaskStatusLog, TaskStatusLogDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserAccess, UserAccessDTO>().ReverseMap();

            CreateMap<Attachment, AttachmentCreateDTO>().ReverseMap();
            CreateMap<Project, ProjectCreateDTO>().ReverseMap();
            CreateMap<TaskManager.Models.Task, TaskCreateDTO>().ReverseMap();
            CreateMap<TaskStatusLog, TaskStatusLogCreateDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<UserAccess, UserAccessCreateDTO>().ReverseMap();
        }
    }
}
