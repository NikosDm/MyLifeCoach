namespace Libraries.Common.Abstractions.Commands;

public interface ICommand<out TResponse> where TResponse : notnull;
