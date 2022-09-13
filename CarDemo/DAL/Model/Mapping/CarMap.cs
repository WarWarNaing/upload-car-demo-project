using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model.Mapping
{
    public class CarMap : EntityTypeConfiguration<Car>
    {
        public CarMap()
        {
            // Primary Key
            this.HasKey(t => t.CarId);

            // Properties


            // Table & Column Mappings
            this.ToTable("Car");
            this.Property(t => t.CarId).HasColumnName("CarId");
            this.Property(t => t.Brand).HasColumnName("Brand");
            this.Property(t => t.Class).HasColumnName("Class");
            this.Property(t => t.ModelName).HasColumnName("ModelName");
            this.Property(t => t.ModelCode).HasColumnName("ModelCode");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Features).HasColumnName("Features");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.DateofManufacturing).HasColumnName("DateofManufacturing");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Image).HasColumnName("Image");
        }
    }
}