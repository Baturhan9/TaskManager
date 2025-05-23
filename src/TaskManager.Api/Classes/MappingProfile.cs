using AutoMapper;
using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

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

            CreateMap<Attachment, AttachmentForManipulationDTO>().ReverseMap();
            CreateMap<Project, ProjectForManipulationDTO>().ReverseMap();
            CreateMap<TaskManager.Models.Task, TaskForManipulationDTO>().ReverseMap();
            CreateMap<TaskStatusLog, TaskStatusLogForManipulationDTO>().ReverseMap();
            CreateMap<User, UserForManipulationDTO>().ReverseMap();
            CreateMap<UserAccess, UserAccessForManipulationDTO>().ReverseMap();
        }
    }
}
