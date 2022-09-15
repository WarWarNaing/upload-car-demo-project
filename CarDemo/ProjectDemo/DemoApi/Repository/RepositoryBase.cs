using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAL.Model;

namespace DemoApi.Repository
{
    public abstract class RepositoryBase<T> where T : class, new()
    {
        public CarDBContext entityContext;
        protected abstract T AddEntity(CarDBContext entityContext, T entity);
        protected abstract T AddOrUpdateEntity(CarDBContext entityContext, T entity);
        protected abstract IQueryable<T> GetEntities();
        protected abstract IQueryable<T> GetEntitiesWithoutTracking();
        protected abstract T GetEntity(CarDBContext entityContext, int id);
        protected abstract T UpdateEntity(CarDBContext entityContext, T entity);


        public int Add(T entity)
        {
            int result = 0;
            using (CarDBContext entityContext = new CarDBContext())
            {
                var obj = AddEntity(entityContext, entity);
                result = entityContext.SaveChanges();

            }
            return result;
        }
        public T AddWithGetObj(T entity)
        {

            using (CarDBContext entityContext = new CarDBContext())
            {
                var obj = AddEntity(entityContext, entity);
                if (entityContext.SaveChanges() > 0)
                {
                    return obj;
                }
            }
            return null;
        }
        public int Remove(T entity)
        {
            using (CarDBContext entityContext = new CarDBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                return entityContext.SaveChanges();
            }
        }

        public int Remove(int id)
        {
            using (CarDBContext entityContext = new CarDBContext())
            {
                T entity = GetEntity(entityContext, id);
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                return entityContext.SaveChanges();
            }
        }

        public int Update(T entity)
        {
            using (CarDBContext entityContext = new CarDBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Modified;
                return entityContext.SaveChanges();
            }
        }
        public T UpdatewithObj(T entity)
        {
            using (CarDBContext entityContext = new CarDBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Modified;
                if (entityContext.SaveChanges() > 0)
                {
                    return entity;
                }
                return null;
            }
        }

        public IQueryable<T> Get()
        {
            return GetEntities();
        }
        public IQueryable<T> GetWithoutTracking()
        {
            return GetEntitiesWithoutTracking();
        }

        public T Get(int id)
        {
            using (CarDBContext entityContext = new CarDBContext())
            {
                return GetEntity(entityContext, id);
            }
        }


    }
}