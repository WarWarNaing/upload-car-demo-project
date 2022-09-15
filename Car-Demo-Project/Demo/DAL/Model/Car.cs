using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Model
{
    public class Car
    {
        public int CarId { get; set; }

        public string Brand { get; set; }

        public string Class { get; set; }

        public string ModelName { get; set; }

        public string ModelCode { get; set; }
       
        public string Description { get; set; }

        public string Features { get; set; }

        public decimal Price { get; set; }

        public DateTime DateofManufacturing { get; set; }

        public bool Active { get; set; }

        public String Image { get; set; }
    }
}
