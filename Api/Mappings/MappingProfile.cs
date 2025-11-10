using Api.DTOs;
using Api.Entities;
using AutoMapper;

namespace Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClienteCreateDto, Cliente>()
                .ForMember(d => d.FotoCasa1Url, opt => opt.Ignore())
                .ForMember(d => d.FotoCasa2Url, opt => opt.Ignore())
                .ForMember(d => d.FotoCasa3Url, opt => opt.Ignore());
        }
    }
}
