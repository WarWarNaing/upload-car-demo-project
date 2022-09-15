using DAL.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class CarDBContext : DbContext
    {
        static CarDBContext()
        {
            Database.SetInitializer<CarDBContext>(null);
        }

        public CarDBContext()
            : base("Name=CarDBContext")
        {
        }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CarMap());
        }
    }
}
