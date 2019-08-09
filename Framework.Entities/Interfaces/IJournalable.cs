// <copyright file="IJournalable.cs" company="">
//
// </copyright>

namespace Framework.Entities.Interfaces
{
    /// <summary>
    ///     Indicates if this BaseObject is a Entity with journal.
    ///     When a BaseObject is marked as entity with journal, the framework, data source implementation in particular will
    ///     automatically look for journaling
    ///     all updates, deletes.The data source implementation shall look for a table going by name MAINTABLENAME_JOURNAL and
    ///     persist all operations
    ///     in the journal table.
    ///     The Journal table shall be constraints free except for a unique Id column and other audit columns. The audit
    ///     columns will carry the same values
    ///     as the main entity. Since no amendments will be made to journal entry, the journal table does not require its own
    ///     audit columns.
    ///     Ideally, each version of main table updates should result in a new row in journal table. So if a table has 10
    ///     updates, the journal table shall feature
    ///     10 rows depicting each version of the main table entry leading to its tenth revision.
    /// </summary>
    /// <typeparam name="T">Type of identity column.</typeparam>
    public interface IJournalable<T> : IAuditable<T>
    {
        /// <summary>
        ///     Gets or sets the main entity identifier.
        ///     Get or sets a value referring to the main table row whose journal entry this represents.
        ///     This serves as the foreign key between the main table and journal table.
        ///     Apart from this the version # will be used.
        /// </summary>
        /// <value>
        ///     The main entity identifier.
        /// </value>
        T MainEntityId { get; set; }
    }
}