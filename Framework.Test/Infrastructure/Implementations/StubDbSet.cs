using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Framework.Entities.Implementation;

namespace Framework.Test.Infrastructure.Implementations
{
    public class StubDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
        where TEntity : class
    {
        private readonly ObservableCollection<TEntity> data;
        private readonly IQueryable query;

        public StubDbSet()
        {
            data = new ObservableCollection<TEntity>();
            query = data.AsQueryable();
        }

        Type IQueryable.ElementType => query.ElementType;
        Expression IQueryable.Expression => query.Expression;
        public override ObservableCollection<TEntity> Local => data;

        IQueryProvider IQueryable.Provider => new StubDbAsyncQueryProvider<TEntity>(query.Provider);

        public override TEntity Add(TEntity item)
        {
            data.Add(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            if (item is BaseEntity)
            {
                var entity = item as BaseEntity;
                var collection = data.ToArray() as BaseEntity[];
                item = collection.FirstOrDefault(e => e.Id.Equals(entity.Id)) as TEntity;
            }

            return item;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            return new StubDbAsyncEnumerator<TEntity>(data.GetEnumerator());
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public override TEntity Remove(TEntity item)
        {
            data.Remove(item);
            return item;
        }
    }
}