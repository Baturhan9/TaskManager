using AutoMapper;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Web.Interfaces;
using TaskManager.Web.Models.Tasks;

namespace TaskManager.Web.Classes;

public class MapperProfile : Profile
{
    private readonly ITaskManagerClient _client;
    public MapperProfile()
    {
        CreateMap<TaskDTO, ListTasksViewModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.LastStatus))
            .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId));
    }

}