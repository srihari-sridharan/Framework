using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Test.Infrastructure.Implementations
{
    internal class StubDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider queryProvider;

        internal StubDbAsyncQueryProvider(IQueryProvider queryProviderInstance)
        {
            queryProvider = queryProviderInstance;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new StubDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new StubDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return queryProvider.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return queryProvider.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }
}