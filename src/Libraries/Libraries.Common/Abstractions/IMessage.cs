namespace Libraries.Common.Abstractions;

public interface IMessage
{
    string EntityType { get; }
    string Action { get; }
    string GetMessageName() => $"{EntityType}.{Action}";
}