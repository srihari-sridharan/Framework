using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Test.Infrastructure.Implementations
{
    internal class StubDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public StubDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public StubDbAsyncEnumerable(Expression expression)
            : base(expression)
        {
        }

        IQueryProvider IQueryable.Provider => new StubDbAsyncQueryProvider<T>(this);

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new StubDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }
    }
}