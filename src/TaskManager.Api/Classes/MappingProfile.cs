using AutoMapper;
using TaskManager.Models;
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
        }
    }
}
