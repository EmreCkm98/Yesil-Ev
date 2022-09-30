using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using YesilEvCF.Core.Interfaces;

namespace YesilEvCF.Core.Repos
{
    public abstract class EFRepoBase<TContext, TEntity> : ISelectableRepo<TEntity>, IUpdatableRepo<TEntity>, IInsertableRepo<TEntity>, IDeletableRepo<TEntity> where TEntity : class where TContext : DbContext, new()
    {
        private readonly TContext _context;
        public EFRepoBase(TContext context)
        {
            _context = context;
        }
        public EFRepoBase()
        {
            _context = new TContext();
        }
        public DbSet<TEntity> GetEntity()
        {
            return _context.Set<TEntity>();
        }
        public TEntity Add(TEntity item)
        {
            return _context.Set<TEntity>().Add(item);
        }
        public bool Any(Func<TEntity, bool> filter)
        {
            return _context.Set<TEntity>().Any(filter);
        }

        public List<TEntity> AddRange(List<TEntity> items)
        {
            return _context.Set<TEntity>().AddRange(items).ToList();
        }

        public TEntity Delete(TEntity item)
        {
            return _context.Set<TEntity>().Remove(item);
        }

        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public List<TEntity> GetByFilter(Func<TEntity, bool> filter)
        {
            return _context.Set<TEntity>().Where(filter).ToList();
        }

        public TEntity GetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Set<TEntity>().AddOrUpdate(item);

        }
    }
}
