using DAL.Helper;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> _CarForm(string FormType, int ID)
        {
            Car car = new Car();
            if (FormType == "Add")
            {
                return PartialView("_carForm", car);
            }
            else
            {
                Car result = await CarApiRequestHelper.GetCarById(ID);
                return PartialView("_carForm", result);
            }
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Save(Car car, string feature, string desc)
        {
            car.Description = (desc != null) ? desc : "";
            car.Features = (feature != null) ? feature : "";

            Car result = await CarApiRequestHelper.UpsertCar(car);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> _carList(string carname = null, int pagesize = 10, int page = 1)
        {
            PagedListClient<Car> result = await CarApiRequestHelper.GetCarListWithPaging(carname, pagesize, page);
            return PartialView("_carList", result);
        }

        public async Task<ActionResult> Delete(int ID = 0)
        {
            Car result = await CarApiRequestHelper.delete(ID);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }
    }
}