using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepoInterfaces
{
    public interface IGenaricRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(TKey id);


        #region Specification overload for [GetAll,GetById]
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification);

        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specification); 
        #endregion

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);

        void Remove(TEntity entity);

        Task<int> CountAsync(ISpecification<TEntity,TKey> specification);
    }
}
