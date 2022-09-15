using DAL.Model;
using DemoApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class CarController : ApiController
    {
        CarDBContext dbContext;
        private CarRepository carRepo = null;
        public CarController()
        {
            dbContext = new CarDBContext();
            carRepo = new CarRepository(dbContext);
        }

        [Route("api/car/getCarById")]
        [HttpGet]
        public HttpResponseMessage getCarById(HttpRequestMessage request, int ID)
        {
            Car car = carRepo.GetWithoutTracking().FirstOrDefault(a => a.Active != true && a.CarId == ID);
            return request.CreateResponse<Car>(HttpStatusCode.OK, car);
        }


        [Route("api/car/upsert")]
        [HttpPost]
        public HttpResponseMessage upsert(HttpRequestMessage request, Car car)
        {
            Car UpdateEntity = null;

            try
            {
                if (car.CarId > 0)
                {
                    UpdateEntity = carRepo.UpdatewithObj(car);
                }
                else
                {
                    UpdateEntity = carRepo.AddWithGetObj(car);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
         
            return request.CreateResponse<Car>(HttpStatusCode.OK, UpdateEntity);
        }
        
        [Route("api/car/list")]
        [HttpGet]
        public HttpResponseMessage list(HttpRequestMessage request, string carname = null, int pagesize = 10, int page = 1)
        {
            Expression<Func<Car, bool>> namefilter;
            if (carname != null)
            {
                namefilter = l => l.ModelName.StartsWith(carname);
            }
            else
            {
                namefilter = l => l.Active != true;
            }
            var objs = carRepo.GetWithoutTracking().Where(a => a.Active != true).Where(namefilter).OrderByDescending(a => a.DateofManufacturing);
            var totalCount = objs.Count();
            var results = objs.Skip(pagesize * (page - 1)).Take(pagesize).ToList();
            var model = new PagedListServer<Car>(results, totalCount, pagesize);

            return request.CreateResponse<PagedListServer<Car>>(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("api/car/delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int ID)
        {
            Car UpdatedEntity = new Car();
            Car article = carRepo.Get().FirstOrDefault(a => a.CarId == ID);
            article.Active = true;

            UpdatedEntity = carRepo.UpdatewithObj(article);
            return request.CreateResponse<Car>(HttpStatusCode.OK, UpdatedEntity);
        }
    }
}
