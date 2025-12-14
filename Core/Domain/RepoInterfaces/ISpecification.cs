using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepoInterfaces
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity,bool>>? Criteria { get; }
        List<Expression<Func<TEntity,object>>> IncludeExpressions { get; }

        Expression<Func<TEntity,object>> OrderBy { get; }
        Expression<Func<TEntity,object>> OrderByDescending { get; }

        public int Skip { get; }
        public int Take { get; }

        public bool IsPaginated { get; } // to mark if pagination is applied or not
    }
}
