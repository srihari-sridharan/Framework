// <copyright file="BaseEntity.cs" company="">
//
// </copyright>

namespace Framework.Entities.Implementation
{
    /// <summary>
    ///     Represents the base entity which has integer identity column and is auditable.
    /// </summary>
    public abstract class BaseEntity : AuditableEntity<int>
    {
    }
}