using I8SSYF_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace I8SSYF_HFT_2021221.Data
{
    public class CarDbContext : DbContext
    {
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Engine> Engines { get; set; }

        public CarDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                //string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
                builder.UseLazyLoadingProxies().UseInMemoryDatabase("cardatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity
                .HasOne(car => car.Engine)
                .WithMany(engine => engine.Cars)
                .HasForeignKey(car => car.EngineId)
                .OnDelete(DeleteBehavior.Cascade);

                entity
                .HasOne(car => car.Model)
                .WithMany(model => model.Cars)
                .HasForeignKey(car => car.ModelId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity
                .HasMany(model => model.Cars)
                .WithOne(car => car.Model)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Engine>(entity =>
            {
                entity
                .HasMany(engine => engine.Cars)
                .WithOne(car => car.Engine)
                .OnDelete(DeleteBehavior.Restrict);
            });

            Model model1 = new Model() { ModelId = 1, Shape = "Sedan" };
            Model model2 = new Model() { ModelId = 2, Shape = "Coupe" };
            Model model3 = new Model() { ModelId = 3, Shape = "Sedan" };
            Model model4 = new Model() { ModelId = 4, Shape = "Touring" };
            Model model5 = new Model() { ModelId = 5, Shape = "Sedan" };

            Engine engine1 = new Engine() { EngineId = 1, Fuel = "Petrol", NumOfCylinders = 6 };
            Engine engine2 = new Engine() { EngineId = 2, Fuel = "Diesel", NumOfCylinders = 6 };
            Engine engine3 = new Engine() { EngineId = 3, Fuel = "Petrol", NumOfCylinders = 12 };
            Engine engine4 = new Engine() { EngineId = 4, Fuel = "Diesel", NumOfCylinders = 6 };
            Engine engine5 = new Engine() { EngineId = 5, Fuel = "Petrol", NumOfCylinders = 8 };

            Car car1 = new Car() { Id = 1, EngineId = engine1.EngineId, ModelId = model1.ModelId, Name = "BMW 530i", Price = 3000000 };
            Car car2 = new Car() { Id = 2, EngineId = engine2.EngineId, ModelId = model2.ModelId, Name = "BMW 330d", Price = 2500000 };
            Car car3 = new Car() { Id = 3, EngineId = engine3.EngineId, ModelId = model3.ModelId, Name = "BMW 750i", Price = 4000000 };
            Car car4 = new Car() { Id = 4, EngineId = engine4.EngineId, ModelId = model4.ModelId, Name = "BMW 525d", Price = 2000000 };
            Car car5 = new Car() { Id = 5, EngineId = engine5.EngineId, ModelId = model5.ModelId, Name = "BMW 740i", Price = 5000000 };


            modelBuilder.Entity<Car>().HasData(car1, car2, car3, car4, car5);
            modelBuilder.Entity<Model>().HasData(model1, model2, model3, model4, model5);
            modelBuilder.Entity<Engine>().HasData(engine1, engine2, engine3, engine4, engine5);
        }
    }
}
