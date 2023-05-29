using I8SSYF_HFT_2021221.Models;
using I8SSYF_HFT_2021221.Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using I8SSYF_HFT_2021221.Repository;

namespace I8SSYF_HFT_2021221.Test
{
    [TestFixture]
    public class LogicTest
    {
        CarLogic carLogic;
        //ModelLogic modelLogic;
        //EngineLogic engineLogic;

        Mock<ICarRepository> mockCarRepo = new Mock<ICarRepository>();
        //Mock<IModelRepository> mockModelRepo = new Mock<IModelRepository>();
        //Mock<IEngineRepository> mockEngineRepo = new Mock<IEngineRepository>();

        [SetUp]
        public void Setup()
        {
            Model model1 = new Model() { Shape = "Sedan" };
            Model model2 = new Model() { Shape = "Touring" };
            Model model3 = new Model() { Shape = "Coupe" };

            Engine engine1 = new Engine() { Fuel = "Petrol", NumOfCylinders = 6 };
            Engine engine2 = new Engine() { Fuel = "Diesel", NumOfCylinders = 6 };
            Engine engine3 = new Engine() { Fuel = "Petrol", NumOfCylinders = 8 };

            Car car1 = new Car() { Id = 1, Name = "BMW 530i", Price = 3000000 };
            Car car2 = new Car() { Id = 2, Name = "BMW 525d", Price = 2500000 };
            Car car3 = new Car() { Id = 3, Name = "BMW 545i", Price = 4000000 };



            mockCarRepo.Setup(x => x.ReadAll()).Returns(new List<Car>
            {
                new Car()
                {
                    Price = 3000000,
                    Model = model1,
                    Engine = engine1
                },
                new Car()
                {
                    Price = 2500000,
                    Model = model2,
                    Engine = engine2
                },
                new Car()
                {
                    Price = 4000000,
                    Model = model1,
                    Engine = engine1
                },
                new Car()
                {
                    Price = 2000000,
                    Model = model3,
                    Engine = engine3
                },
            }.AsQueryable());

            carLogic = new CarLogic(mockCarRepo.Object);
        }

        [Test]
        public void TestAveragePrice()
        {
            double avg = carLogic.AveragePrice();
            Assert.That(avg, Is.EqualTo(2875000));
        }

        [Test]
        public void TestAverageNumberOfCylindersByModels()
        {
            var result = carLogic.AverageNumberOfCylindersByModels();

            var expected = new List<KeyValuePair<string, double>>()
            {
                new KeyValuePair<string, double>("Sedan", 6),
                new KeyValuePair<string, double>("Touring", 6),
                new KeyValuePair<string, double>("Coupe", 8)
            };
            Assert.That(result, Is.EqualTo(expected));
        }

        //[Test]
        //public void TestPetrolCars()
        //{
        //    var result = carLogic.PetrolCars();
        //    Assert.That(result, Is.EqualTo(3));
        //}

        [Test]
        public void TestAveragePriceByModels()
        {
            var result = carLogic.AveragePriceByModels().ToList();

            var expected = new List<KeyValuePair<string, double>>()
            {
                new KeyValuePair<string, double>("Sedan", 3500000),
                new KeyValuePair<string, double>("Touring", 2500000),
                new KeyValuePair<string, double>("Coupe", 2000000)
            };
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestCylindersByDescending()
        {
            var result = carLogic.CylindersByDescending();

            var expected = new List<KeyValuePair<string, double>>()
            {
                new KeyValuePair<string, double>("BMW 750i", 12),
                new KeyValuePair<string, double>("BMW 740i", 8),
                new KeyValuePair<string, double>("BMW 530i", 6),
                new KeyValuePair<string, double>("BMW 330d", 6),
                new KeyValuePair<string, double>("BMW 525d", 6)
            };
        }

        //[Test]
        //public void TestSedanCount()
        //{
        //    var result = carLogic.SedanCount();

        //    Assert.That(result, Is.EqualTo(2));
        //}

        [Test]
        public void TestCarCountByModels()
        {
            var result = carLogic.CarCountByModels();

            var expected = new List<KeyValuePair<string, double>>()
            {
                new KeyValuePair<string, double>("Sedan", 2),
                new KeyValuePair<string, double>("Touring", 1),
                new KeyValuePair<string, double>("Coupe", 1)
            };
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestSumPriceByModels()
        {
            var result = carLogic.SumPriceByModels();

            var expected = new List<KeyValuePair<string, double>>()
            {
                new KeyValuePair<string, double>("Sedan", 7000000),
                new KeyValuePair<string, double>("Touring", 2500000),
                new KeyValuePair<string, double>("Coupe", 2000000)
            };
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestDelete()
        {
            carLogic.Delete(1);

            mockCarRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void TestCreate()
        {
            var car = new Car() { Name = "BMW 530i" };
            carLogic.Create(car);

            mockCarRepo.Verify(x => x.Create(car), Times.Once);
        }

        [Test]
        public void TestUpdate()
        {
            var car = new Car() {Id = 1, Name = "BMW 530i Individual Edition", Price = 9000000 };
            carLogic.Update(car);

            mockCarRepo.Verify(x => x.Update(car), Times.Once);
        }

        [Test]
        public void TestReadWithInvalidIdThrowsException()
        {
            mockCarRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(value: null);

            Assert.Throws<ArgumentException>(() => carLogic.Read(1));
        }

        [Test]
        public void TestReadWithValidIdReturnsExpectedObject()
        {
            Car expected = new Car()
            {
                Id = 1,
                Name = "BMW 530i",
                Price = 3000000
            };

            mockCarRepo.Setup(x => x.Read(1)).Returns(expected);

            var investigated = carLogic.Read(1);

            Assert.That(investigated, Is.EqualTo(expected));
        }
    }
}
