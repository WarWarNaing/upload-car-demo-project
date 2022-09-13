using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.Repository
{
    public class CarRepository : RepositoryBase<Car>
    {
        public CarRepository()
        {
            this.entityContext = new CarDBContext();
        }

        public CarRepository(CarDBContext context)
        {
            this.entityContext = context;
        }

        protected override Car AddEntity(CarDBContext entityContext, Car entity)
        {
            return entityContext.Cars.Add(entity);
        }

        protected override Car AddOrUpdateEntity(CarDBContext entityContext, Car entity)
        {
            if (entity.CarId == default(int))
            {
                return entityContext.Cars.Add(entity);
            }
            else
            {

                return entityContext.Cars.FirstOrDefault(e => e.CarId == entity.CarId);
            }
        }

        protected override Car UpdateEntity(CarDBContext entityContext, Car entity)
        {
            return entityContext.Cars.FirstOrDefault(e => e.CarId == entity.CarId);

        }

        protected override IQueryable<Car> GetEntities()
        {
            return entityContext.Cars.AsQueryable();
        }

        protected override IQueryable<Car> GetEntitiesWithoutTracking()
        {
            return entityContext.Cars.AsNoTracking().AsQueryable();
        }

        protected override Car GetEntity(CarDBContext entityContext, int id)
        {
            return entityContext.Cars.FirstOrDefault(e => e.CarId == id);
        }
    }
}