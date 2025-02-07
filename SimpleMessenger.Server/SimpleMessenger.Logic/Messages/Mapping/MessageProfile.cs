using AutoMapper;
using SimpleMessenger.DataAccess.Models;

namespace SimpleMessenger.Logic.Messages.Mapping;

internal class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageDto>()
            .ForMember(dto => dto.Text,
            opt => opt.MapFrom(s => s.Text))
            .ForMember(dto => dto.SequenceNumber,
            opt => opt.MapFrom(s => s.SequenceNumber))
            .ForMember(dto => dto.Timestamp,
            opt => opt.MapFrom(s => s.Timestamp));
    }
}