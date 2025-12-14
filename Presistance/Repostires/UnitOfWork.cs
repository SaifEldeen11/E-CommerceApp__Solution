using Domain.Models;
using Domain.RepoInterfaces;
using Presistance.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repostires
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();
        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // check if i have ref of the repository in dictionary

            var TypeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(TypeName))
            {
                return (IGenaricRepository<TEntity, TKey>) _repositories[TypeName];
            }
            // create repository

            var repo=new GenaricRepository<TEntity, TKey>(_dbContext);

            // Store ref in dictionary

            _repositories[TypeName] = repo;

            // return object
            return repo;
        }

        public async Task<int> SvaeChangesAsync()
        {
          return await _dbContext.SaveChangesAsync(); 
        }
    }
}
