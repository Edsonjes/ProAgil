using AutoMapper;
using ProAgil.Domain;
using ProAgil.WebAPI.Dtos;
using System.Linq;

namespace ProAgil.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento,EventosDtos>().ForMember(dest => dest.Palestrante, opt => {
                opt.MapFrom(src => src.PalestranteEventos.Select(x =>  x.Palestrante).ToList());
            }).ReverseMap();
            CreateMap<Palestrante,PalestranteDtos>()
            .ForMember(dest => dest.Evento, opt => {opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Eventos).ToList());
            }).ReverseMap();
            CreateMap<Lote,LotesDtos>().ReverseMap();
            CreateMap<RedeSocial,RedeSociaisDtos>().ReverseMap();
        }
    }
}