namespace Framework.Entities.Implementation
{
    /// <summary>
    ///     Represents a base entity with journal and integer identity column.
    /// </summary>
    public abstract class JournalableBaseEntity : JournalableEntity<int>
    {
    }
}