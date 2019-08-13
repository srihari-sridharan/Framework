using System;
using System.Collections.Generic;
using System.Transactions;
using Framework.Base;
using Framework.Interfaces.Services;
using Framework.Utils;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Framework.UnityContainer.Interceptors
{
    /// <summary>
    ///     This interceptor shall intercept all service methods and ensure that
    ///     1. The calls are authorized
    ///     2. Transaction is initiated if required
    ///     3. Handle unhandled exceptions
    ///     4. Log all service calls
    ///     5. Check for null arguments unless argument name starts with optional
    /// </summary>
    public class ServiceInterceptor : IInterceptionBehavior
    {
        /// <summary>
        ///     Gets a value indicating whether this behavior will actually do anything when invoked.
        /// </summary>
        /// <remarks>
        ///     This is used to optimize interception. If the behaviors won't actually
        ///     do anything then the interception mechanism can be skipped completely.
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
            if (target == null)
            {
                return null;
            }

            target.LogInfo(Constants.Start.FormatIt(methodName));
            ValidateArguments(input, target);
            var requiresTransaction = ((IService) target).RequiresTransaction.Contains(methodName);
            try
            {
                if (requiresTransaction)
                {
                    target.LogDebug(Constants.InitiateTransaction.FormatIt(input.MethodBase.Name));
                    using (var transaction = new TransactionScope(TransactionScopeOption.Required))
                    {
                        result = Run(input, getNext);
                        transaction.Complete();
                        target.LogInfo(Constants.CompleteTransaction.FormatIt(input.MethodBase.Name));
                    }
                }
                else
                {
                    result = Run(input, getNext);
                }
            }
            catch (BaseException)
            {
                if (requiresTransaction)
                    target.LogDebug(Constants.RollbackTransaction.FormatIt(input.MethodBase.Name));

                throw;
            }
            catch (Exception ex)
            {
                throw new BaseException(Constants.Error, ex);
            }

            return result;
        }

        /// <summary>
        ///     Invokes the specified method.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="getNext">The get next.</param>
        /// <returns>Execution result.</returns>
        private static IMethodReturn Run(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            return getNext()(input, getNext);
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