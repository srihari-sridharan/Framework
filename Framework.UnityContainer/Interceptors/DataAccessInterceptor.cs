using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using Framework.Base;
using Framework.Utils;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Framework.UnityContainer.Interceptors
{
    /// <summary>
    ///     Interceptor for data access objects. This is the one stop shop for common behavior required for any DAO.
    ///     This handles cross cutting concerns by intercepting calls.
    /// </summary>
    public class DataAccessInterceptor : IInterceptionBehavior
    {
        /// <summary>
        ///     Gets a value indicating whether this behavior will actually do anything when invoked.
        /// </summary>
        /// <remarks>
        ///     This is used to optimize interception. If the behaviors won't actually
        ///     do anything (for example, PIAB where no policies match) then the interception
        ///     mechanism can be skipped completely.
        /// </remarks>
        public bool WillExecute => true;

        /// <summary>
        ///     Returns the interfaces required by the behavior for the objects it intercepts.
        /// </summary>
        /// <returns>
        ///     The required interfaces.
        /// </returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        ///     Implement this method to execute your behavior processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the behavior chain.</param>
        /// <returns>
        ///     Return value from the target.
        /// </returns>
        /// <exception cref="Framework.Base.BaseException">Base Exception</exception>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn result;
            var target = input.Target as BaseObject;
            var methodName = input.MethodBase.Name;
            target.LogInfo(Constants.Start.FormatIt(methodName));
            ValidateArguments(input, target);
            try
            {
                result = getNext()(input, getNext);
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                target.LogError(Constants.ExecutionError.FormatIt(methodName), ex);
                throw new BaseException(Constants.Error, ex);
            }
            catch (Exception ex)
            {
                throw new BaseException(Constants.Error, ex);
            }
        }

        /// <summary>
        ///     Validates the arguments.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="target">The target.</param>
        /// <exception cref="Framework.Base.BaseException">Base Exception</exception>
        private static void ValidateArguments(IMethodInvocation input, BaseObject target)
        {
            for (var i = 0; i < input.Arguments.Count; i++)
            {
                var paramName = input.Arguments.ParameterName(i);
                var param = input.Arguments[paramName];
                if (null == param)
                {
                    target.LogError(Constants.Null.FormatIt(paramName), null);
                    throw new BaseException(Constants.ArgumentIsNull, null, paramName);
                }
            }
        }
    }
}