using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using rentabike.data;
using rentabike.model;
using rentabike.model.enumerations;
using rentabike.model.strategies;
using rentabike.service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace rentabike.test
{
    [TestClass]
    public class RentalMessageServiceTests
    {
        [TestMethod]
        public void Init()
        {
            //preparation
            var rentalMessageService = new RentalMessageService(GetBuilderMock());
            //test
            var rental = rentalMessageService.Init().Result;
            //validation
            Assert.IsNotNull(rental);
            Assert.AreEqual((int)RentalTypeEnum.NormalGroup, rental.RentalTypeId);
        }

        [TestMethod]
        public void AddTo()
        {
            //preparation
            var rentalMessageService = new RentalMessageService(GetBuilderMock());
            var compRental = new CompositeRental();
            //test
            compRental = rentalMessageService.AddTo(compRental, (int)RentalTypeEnum.ByHour, 2).Result;
            compRental = rentalMessageService.AddTo(compRental, (int)RentalTypeEnum.ByDay, 1).Result;
            //validation
            Assert.IsNotNull(compRental.Childrens);
            Assert.AreEqual(2, compRental.Childrens.Count);
        }
        [TestMethod]
        public void GetResultNormal()
        {
            //preparation
            var rentalMessageService = new RentalMessageService(GetBuilderMock());
            var compRental = rentalMessageService.Init().Result;
            compRental = rentalMessageService.AddTo(compRental, (int)RentalTypeEnum.ByHour, 2).Result; 
            compRental = rentalMessageService.AddTo(compRental, (int)RentalTypeEnum.ByDay, 1).Result;
            var expectedPrice = Math.Round(compRental.Childrens.Sum(c => c.Price));
            //test
            compRental = rentalMessageService.GetResult(compRental).Result;
            //validation
            Assert.AreEqual(compRental.RentalTypeId, (int)RentalTypeEnum.NormalGroup);
            Assert.AreEqual(compRental.Price, expectedPrice);
        }
        [TestMethod]
        public void GetResultFamiliar()
        {
            //preparation
            var rentalMessageService = new RentalMessageService(GetBuilderMock());
            var compRental = rentalMessageService.Init().Result;
            compRental = rentalMessageService.AddTo(compRental, (int)RentalTypeEnum.ByHour, 2).Result;
            compRental = rentalMessageService.AddTo(compRental, (int)RentalTypeEnum.ByDay, 1).Result;
            compRental = rentalMessageService.AddTo(compRental, (int)RentalTypeEnum.ByHour, 5).Result;
            compRental = rentalMessageService.AddTo(compRental, (int)RentalTypeEnum.ByWeek, 1).Result;
            var expectedPrice = Math.Round(compRental.Childrens.Sum(c => c.Price) * 0.7);
            //test
            compRental = rentalMessageService.GetResult(compRental).Result;
            //validation
            Assert.AreEqual(compRental.RentalTypeId, (int)RentalTypeEnum.StrategyGroup);
            Assert.AreEqual(compRental.Price, expectedPrice);            
        }
        [TestMethod]
        public void GetResultMultiple()
        {
            //preparation
            var rentalMessageService = new RentalMessageService(GetBuilderMock());
            var compRental = rentalMessageService.Init().Result;
            var group1 = new List<Rental>();
            group1.Add(new LeafRental { RentalTypeId = (int)RentalTypeEnum.ByHour, Quantity = 2, Price = 10 });
            group1.Add(new LeafRental { RentalTypeId = (int)RentalTypeEnum.ByDay, Quantity = 1, Price = 20 });
            group1.Add(new LeafRental { RentalTypeId = (int)RentalTypeEnum.ByWeek, Quantity = 1, Price = 60 });
            
            var group2 = new List<Rental>();
            group2.Add(new LeafRental { RentalTypeId = (int)RentalTypeEnum.ByHour, Quantity = 5, Price = 25 });
            group2.Add(new LeafRental { RentalTypeId = (int)RentalTypeEnum.ByDay, Quantity = 1, Price = 20 });
            group2.Add(new LeafRental { RentalTypeId = (int)RentalTypeEnum.ByWeek, Quantity = 1, Price = 60 });
            var expectedPrice = Math.Round(group1.Sum(c => c.Price) * 0.7) + Math.Round(group2.Sum(c => c.Price) * 0.7);

            foreach (var item in group1)
            {
                compRental = rentalMessageService.AddTo(compRental, item.RentalTypeId, item.Quantity).Result;
            }
            foreach (var item in group2)
            {
                compRental = rentalMessageService.AddTo(compRental, item.RentalTypeId, item.Quantity).Result;
            }
            //test
            compRental = rentalMessageService.GetResult(compRental).Result;
            //validation
            Assert.AreEqual(compRental.RentalTypeId, (int)RentalTypeEnum.NormalGroup);
            Assert.AreEqual(compRental.Childrens.Count, 2);
            Assert.AreEqual(compRental.Childrens.ElementAt(0).RentalTypeId, (int)RentalTypeEnum.StrategyGroup);
            Assert.AreEqual(compRental.Childrens.ElementAt(1).RentalTypeId, (int)RentalTypeEnum.StrategyGroup);
            Assert.AreEqual(compRental.Price, expectedPrice);
        }

        #region Mock data
        private IRentalBuilder GetBuilderMock()
        {
            return new RentalBuilder(GetStrategyServiceMock(), GetRentalServiceMock());
        }
        private IStrategyService GetStrategyServiceMock()
        {
            return new StrategyService(GetRepositoryMock());
        }
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
        private BaseService<Rental> GetRentalServiceMock()
        {
            return new RentalService(GetRentalRepositoryMock());
        }
        private IRepository<Rental> GetRentalRepositoryMock()
        {
            var rentalRepositoryMock = new Mock<IRepository<Rental>>();
            rentalRepositoryMock.Setup(r => r.Insert(It.IsAny<Rental>()));
            return rentalRepositoryMock.Object;
        }
        #endregion
    }
}
