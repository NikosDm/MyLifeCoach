namespace Libraries.Common.Entities;

public interface IEntity<T>
{
    T Id { get; }

    void SetEntityId(T value);
}
