namespace Framework.Entities.Interfaces
{
    /// <summary>
    ///     Interface for base entity.
    /// </summary>
    public interface IBaseEntity : IAuditable<int>
    {
        // This class will inherit IJournallable<int> in future.
    }
}