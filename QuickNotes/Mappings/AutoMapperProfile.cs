using AutoMapper;
using QuickNotes.DTOs;
using QuickNotes.Models;

namespace QuickNotes.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
        }
    }
}
