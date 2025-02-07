using FluentResults;
using MediatR;
using SimpleMessenger.DataAccess.Storage.Abstractions;
using SimpleMessenger.Logic.Messages.Mapping;

namespace SimpleMessenger.Logic.Messages.Queries.GetMessages;

public class GetMessagesQuery : IRequest<Result<MessagesListDto>>
{
    public ISpecification? Specification { get; set; }
}
