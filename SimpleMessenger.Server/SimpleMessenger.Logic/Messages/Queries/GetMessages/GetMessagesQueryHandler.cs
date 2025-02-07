using AutoMapper;
using FluentResults;
using MediatR;
using SimpleMessenger.DataAccess.Storage.Abstractions;
using SimpleMessenger.Logic.Messages.Mapping;

namespace SimpleMessenger.Logic.Messages.Queries.GetMessages;

internal class GetMessagesQueryHandler(IMessageRepository repos, IMapper mapper) 
    : IRequestHandler<GetMessagesQuery, Result<MessagesListDto>>
{
    public async Task<Result<MessagesListDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await repos.GetAll(request.Specification, cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(new MessagesListDto { Messages = messages.Select(mapper.Map<MessageDto>).ToList() });
    }
}