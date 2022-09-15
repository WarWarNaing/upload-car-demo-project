using DAL.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
   public class CarApiRequestHelper
    {
        public static async Task<Car> GetCarById(int ID)
        {
            string url = string.Format("api/car/getCarById?ID={0}", ID);
            Car result = await ApiRequest<Car>.GetRequest(url);
            return result;
        }

        public static async Task<Car> UpsertCar(Car car)
        {
            string url = string.Format("api/car/upsert");
            Car result = await ApiRequest<Car>.PostRequest(url, car);
            return result;
        }

        public static async Task<PagedListClient<Car>> GetCarListWithPaging(string carname = null, int pagesize = 10, int page = 1)
        {
            string url = $"api/car/list?carname={carname}&pagesize={pagesize}&page={page}";
            var data = await ApiRequest<PagedListServer<Car>>.GetRequest(url);
            var model = new PagedListClient<Car>();
            var pagedList = new StaticPagedList<Car>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }

        public static async Task<Car> delete(int ID)
        {
            var url = string.Format("api/car/delete?ID={0}", ID);
            Car result = await ApiRequest<Car>.GetRequest(url);
            return result;
        }
    }
}
