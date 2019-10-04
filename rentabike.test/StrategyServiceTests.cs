using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using rentabike.data;
using rentabike.model;
using rentabike.model.enumerations;
using rentabike.model.strategies;
using rentabike.service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace rentabike.test
{
    [TestClass]
    public class StrategyServiceTests
    {
        [TestMethod]
        public void GetNormalRental()
        {
            //preparation
            var strategyService = new StrategyService(GetRepositoryMock());
            var strategy = GetStrategyMock();
            var rentals = new List<Rental>();
            rentals.Add(new LeafRental { Quantity = 2, RentalTypeId = (int)RentalTypeEnum.ByHour, Price = 10 });
            rentals.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByDay, Price = 20 });
            var price = Math.Round(rentals.Sum(r => r.Price));

            //test
            var compRental = strategyService.GetRental(rentals);

            //verification
            Assert.IsInstanceOfType(compRental, typeof(CompositeRental));
            Assert.AreEqual((int)RentalTypeEnum.NormalGroup, compRental.RentalTypeId);
            Assert.AreEqual((int)RentalTypeEnum.NormalGroup, compRental.Type.Id);
            Assert.AreEqual("Normal Group", compRental.Type.Description);
            Assert.AreEqual(true, compRental.IsComposite());
            Assert.AreEqual(price, compRental.Price);
        }
        [TestMethod]
        public void GetNormalRentalEmpty()
        {
            //preparation
            var strategyService = new StrategyService(GetRepositoryMock());
            var strategy = GetStrategyMock();
            var rentals = new List<Rental>();

            //test
            var compRental = strategyService.GetRental(rentals);

            //verification
            Assert.IsInstanceOfType(compRental, typeof(CompositeRental));
            Assert.AreEqual((int)RentalTypeEnum.NormalGroup, compRental.RentalTypeId);
        }

        [TestMethod]
        public void GetFamilyRental()
        {
            //preparation
            var strategyService = new StrategyService(GetRepositoryMock());

            var rentals = new List<Rental>();
            rentals.Add(new LeafRental { Quantity = 2, RentalTypeId = (int)RentalTypeEnum.ByHour, Price = 10 });
            rentals.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByDay, Price = 20 });
            rentals.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByWeek, Price = 60 });
            var price = Math.Round(rentals.Sum(r => r.Price) * 0.7);

            //test
            var compRental = strategyService.GetRental(rentals);

            //verification
            Assert.IsInstanceOfType(compRental, typeof(CompositeRental));
            Assert.AreEqual((int)RentalTypeEnum.StrategyGroup, compRental.RentalTypeId);
            Assert.AreEqual(price, compRental.Price);
        }

        [TestMethod]
        public void GetFamilyRentalMultiple()
        {
            //preparation
            var strategyService = new StrategyService(GetRepositoryMock());

            var rentals = new List<Rental>();
            //Familiar Group
            rentals.Add(new LeafRental { Quantity = 2, RentalTypeId = (int)RentalTypeEnum.ByHour, Price = 10 });
            rentals.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByDay, Price = 20 });
            rentals.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByWeek, Price = 60 });
            rentals.Add(new LeafRental { Quantity = 2, RentalTypeId = (int)RentalTypeEnum.ByHour, Price = 10 });
            rentals.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByDay, Price = 20 });
            //Familiar Group
            rentals.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByWeek, Price = 60 });
            rentals.Add(new LeafRental { Quantity = 2, RentalTypeId = (int)RentalTypeEnum.ByHour, Price = 10 });
            rentals.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByDay, Price = 20 });
            rentals.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByWeek, Price = 60 });
            var price = Math.Round(rentals.Sum(r => r.Price) * 0.7);

            //test
            var compRental = strategyService.GetRental(rentals);

            //verification
            Assert.IsInstanceOfType(compRental, typeof(CompositeRental));
            Assert.AreEqual((int)RentalTypeEnum.NormalGroup, compRental.RentalTypeId);
            Assert.AreEqual(2, compRental.Childrens.Count);
            Assert.AreEqual(price, compRental.Price);
        }

        [TestMethod]
        public void CalculatePriceNotSupported()
        {
            //preparation
            var strategyService = new StrategyService(GetRepositoryMock());
            var strategy = GetStrategyMock();
            var rental = new LeafRental { Quantity = 1, RentalTypeId = 6 };
            var expectedException = new Exception($"Strategy {strategy.Description} not support this rental type ({rental.RentalTypeId})");
            var currentException = new Exception();
            //test
            try
            {
                strategyService.SetPrice(rental);
            }
            catch (Exception ex)
            {
                currentException = ex;
            }
            Assert.AreEqual(currentException.GetType(), expectedException.GetType());
            Assert.AreEqual(currentException.Message, expectedException.Message);
        }

        [TestMethod]
        public void CalculatePriceStrategyNotFound()
        {
            //preparation
            var strategyRepositoryMock = new Mock<IRepository<Strategy>>();
            strategyRepositoryMock.Setup(s => s.GetById((int)PriceStrategyEnum.Family))
                .Returns(default(Strategy));

            var strategyService = new StrategyService(strategyRepositoryMock.Object);
            var strategy = GetStrategyMock();
            var rental = new LeafRental { Quantity = 2, RentalTypeId = (int)RentalTypeEnum.ByHour, Price = 10 };
            var expectedException = new Exception("Strategy not found");
            var currentException = new Exception();
            //test
            try
            {
                strategyService.SetPrice(rental);
            }
            catch (Exception ex)
            {
                currentException = ex;
            }


            Assert.AreEqual(currentException.GetType(), expectedException.GetType());
            Assert.AreEqual(currentException.Message, expectedException.Message);
        }

        [TestMethod]
        public void CalculatePriceStrategyDetailsNotFound()
        {
            //preparation
            var strategyRepositoryMock = new Mock<IRepository<Strategy>>();
            var strategyMock = GetStrategyMock();
            strategyMock.StrategyRentalTypes = null;
            strategyRepositoryMock.Setup(s => s.GetById((int)PriceStrategyEnum.Family))
                .Returns(strategyMock);

            var strategyService = new StrategyService(strategyRepositoryMock.Object);
            var strategy = GetStrategyMock();
            var rental = new LeafRental { Quantity = 2, RentalTypeId = (int)RentalTypeEnum.ByHour, Price = 10 };
            var expectedException = new Exception("Strategy not found");
            var currentException = new Exception();
            //test
            try
            {
                strategyService.SetPrice(rental);
            }
            catch (Exception ex)
            {
                currentException = ex;
            }


            Assert.AreEqual(currentException.GetType(), expectedException.GetType());
            Assert.AreEqual(currentException.Message, expectedException.Message);
        }
        [TestMethod]
        public void CalculatePriceStrategyDetailsEmpty()
        {
            //preparation
            var strategyRepositoryMock = new Mock<IRepository<Strategy>>();
            var strategyMock = GetStrategyMock();
            strategyMock.StrategyRentalTypes = new List<StrategyRentalType>();
            strategyRepositoryMock.Setup(s => s.GetById((int)PriceStrategyEnum.Family))
                .Returns(strategyMock);

            var strategyService = new StrategyService(strategyRepositoryMock.Object);
            var strategy = GetStrategyMock();
            var rental = new LeafRental { Quantity = 2, RentalTypeId = (int)RentalTypeEnum.ByHour, Price = 10 };
            var expectedException = new Exception($"Strategy {strategy.Description} not support this rental type ({rental.RentalTypeId})");
            var currentException = new Exception();
            //test
            try
            {
                strategyService.SetPrice(rental);
            }
            catch (Exception ex)
            {
                currentException = ex;
            }


            Assert.AreEqual(currentException.GetType(), expectedException.GetType());
            Assert.AreEqual(currentException.Message, expectedException.Message);
        }

        #region Mock data
        private IRepository<Strategy> GetRepositoryMock()
        {
            var strategyRepositoryMock = new Mock<IRepository<Strategy>>();
            strategyRepositoryMock.Setup(s => s.GetById((int)PriceStrategyEnum.Family))
                .Returns(GetStrategyMock());
            return strategyRepositoryMock.Object;
        }
        private Strategy GetStrategyMock()
        {
            return new Strategy
            {
                Id = 1,
                Description = "Family Strategy",
                MinCompositeSize = 3,
                MaxCompositeSize = 5,
                StrategyRentalTypes = new List<StrategyRentalType>()
                {
                    new StrategyRentalType { Id = 1, RentalTypeId = (int)RentalTypeEnum.ByHour, Price=5, RentalType=new RentalType{ Id=1, Description="ByHour", IsComposite=false}, PriceStrategyId=1, PriceStrategy=new Strategy() },
                    new StrategyRentalType { Id = 2, RentalTypeId = (int)RentalTypeEnum.ByDay, Price=20, RentalType=new RentalType{ Id=2, Description="ByDay", IsComposite=false}, PriceStrategyId=1, PriceStrategy=new Strategy()},
                    new StrategyRentalType { Id = 3, RentalTypeId = (int)RentalTypeEnum.ByWeek, Price=60, RentalType=new RentalType{ Id=3, Description="ByWeek", IsComposite=false}, PriceStrategyId=1, PriceStrategy=new Strategy()},
                    new StrategyRentalType { Id = 4, RentalTypeId = (int)RentalTypeEnum.StrategyGroup, Discount=0.3, RentalType=new RentalType{ Id=4, Description="Familiar Group", IsComposite=true}, PriceStrategyId=1, PriceStrategy=new Strategy()},
                    new StrategyRentalType { Id = 5, RentalTypeId = (int)RentalTypeEnum.NormalGroup, RentalType=new RentalType{ Id=5, Description="Normal Group", IsComposite=true}, PriceStrategyId=1, PriceStrategy=new Strategy()},

                }
            };
        }
        #endregion
    }
}
