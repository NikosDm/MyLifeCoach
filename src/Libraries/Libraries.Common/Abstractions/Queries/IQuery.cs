namespace Libraries.Common.Abstractions.Queries;

public interface IQuery<out TResponse> where TResponse : notnull;
