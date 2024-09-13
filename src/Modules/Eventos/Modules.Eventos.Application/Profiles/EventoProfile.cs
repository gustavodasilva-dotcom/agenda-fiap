using Agenda.Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.Domain.Entities;
using AutoMapper;

namespace Agenda.Modules.Eventos.Application.Profiles;

public class EventoProfile : Profile
{
    public EventoProfile()
    {
        CreateMap<Evento, EventoResponse>()
            .ForMember(
                dest => dest.ContatosIds,
                opt => opt.MapFrom(dest => dest.Contatos.Select(c => c.ContatoId).ToArray()));
    }
}
