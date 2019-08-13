namespace Framework.Base
{
    /// <summary>
    ///     Represents the constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///     The argument is null message.
        /// </summary>
        public const string ArgumentIsNull = "ARGUMENT_IS_NULL";

        /// <summary>
        ///     The complete transaction message format.
        /// </summary>
        public const string CompleteTransaction = "COMPLETING TRANSACTION:{0}";

        /// <summary>
        ///     The context
        /// </summary>
        public const string Context = "CONTEXT";

        /// <summary>
        ///     The data source library required error message.
        /// </summary>
        public const string DataSourceLibraryRequired = "DS_LIBRARY_IS_REQUIRED";

        /// <summary>
        ///     The data source name required error message.
        /// </summary>
        public const string DataSourceNameRequired = "DS_NAME_IS_REQUIRED";

        /// <summary>
        ///     The default container name
        /// </summary>
        public const string DefaultContainerName = null;

        /// <summary>
        ///     The error message.
        /// </summary>
        public const string Error = "ERROR";

        /// <summary>
        ///     The execution error message format.
        /// </summary>
        public const string ExecutionError = "EXECUTIONERROR:{0}";

        /// <summary>
        ///     The fully qualified container for dependency injection and inversion of control.
        /// </summary>
        public const string FullyQualifiedContainer
            = "Framework.UnityContainer.dll,Framework.UnityContainer.Wrappers.UnityContainerWrapper";

        /// <summary>
        ///     The initiate transaction message format.
        /// </summary>
        public const string InitiateTransaction = "INITIATING TRANSACTION:{0}";

        /// <summary>
        ///     The null message format.
        /// </summary>
        public const string Null = "NULL:{0}";

        /// <summary>
        ///     The rollback transaction message format.
        /// </summary>
        public const string RollbackTransaction = "ROLLBACK TRANSACTION:{0}";

        /// <summary>
        ///     The service context key
        /// </summary>
        public const string ServiceContextKey = "SERVICE_CONTEXT";

        /// <summary>
        ///     The start message format.
        /// </summary>
        public const string Start = "START:{0}";
    }
}