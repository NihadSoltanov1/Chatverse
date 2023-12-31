﻿namespace Chatverse.Application.Features.Command.Message.CreateMessage;

public class SendMessageCommandRequest : IRequest<SendMessageCommandResponse>
{
    public string? Content { get; set; }
    public string? Image { get; set; }
    public string? ToUserId { get; set; }
    public string? FromUserId { get; set; }
}
