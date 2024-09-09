namespace Agenda.Common.DependencyInjection.Options;

public sealed class MessageBrokerOptions
{
    public const string Position = "MessageBroker";

    public string Host { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
}
