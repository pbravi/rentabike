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
    public class RentalBuilderTests
    {
        [TestMethod]
        public void AddRental()
        {
            //preparation
            var rentalBuilder = new RentalBuilder(GetStrategyServiceMock(), GetRentalServiceMock());
            var leafRental = new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByHour };
            var expectedPrice = 5;
            var compRental = new CompositeRental();
            //test
            rentalBuilder.AddTo(compRental, leafRental.RentalTypeId, leafRental.Quantity);
            //validation
            Assert.AreEqual(expectedPrice, compRental.Childrens.ElementAt(0).Price);

        }
        [TestMethod]
        public void Finish()
        {
            //preparation
            var rentalBuilder = new RentalBuilder(GetStrategyServiceMock(), GetRentalServiceMock());
            var childrens = new List<Rental>();
            childrens.Add(new LeafRental { Quantity = 5, RentalTypeId = (int)RentalTypeEnum.ByHour, Price=25 });
            childrens.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByDay, Price=20 });
            childrens.Add(new LeafRental { Quantity = 1, RentalTypeId = (int)RentalTypeEnum.ByWeek, Price=60 });           
            var expectedPrice = Math.Round(childrens.Sum(c => c.Price) * 0.7);
            var expectedRental = new CompositeRental { RentalTypeId = (int)RentalTypeEnum.StrategyGroup, Childrens = childrens, Price = expectedPrice };

            var currentRental = new CompositeRental();
            rentalBuilder.AddTo(currentRental, childrens.ElementAt(0).RentalTypeId, childrens.ElementAt(0).Quantity);
            rentalBuilder.AddTo(currentRental, childrens.ElementAt(1).RentalTypeId, childrens.ElementAt(1).Quantity);
            rentalBuilder.AddTo(currentRental, childrens.ElementAt(2).RentalTypeId, childrens.ElementAt(2).Quantity);

            //test
            currentRental = rentalBuilder.GetResult(currentRental);

            //validation
            Assert.IsInstanceOfType(currentRental, typeof(CompositeRental));
            Assert.AreEqual(expectedRental.RentalTypeId, currentRental.RentalTypeId);
            Assert.AreEqual(expectedRental.Price, currentRental.Price);
            Assert.AreEqual(expectedRental.Childrens.Count, currentRental.Childrens.Count);
        }

        #region Mock data
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
