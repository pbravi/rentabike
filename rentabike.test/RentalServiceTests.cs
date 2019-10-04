using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using rentabike.data;
using rentabike.model;
using rentabike.model.enumerations;
using rentabike.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace rentabike.test
{
    [TestClass]
    public class RentalServiceTests
    {
        [TestMethod]
        public void Constructor()
        {
            //test
            var rentalService = new RentalService(GetRepositoryMock());
            //validation
            Assert.IsInstanceOfType(rentalService, typeof(BaseService<Rental>));
        }
        [TestMethod]
        public void GetById()
        {
            //preparation
            var rental = new CompositeRental { Id = 1, RentalTypeId = (int)RentalTypeEnum.StrategyGroup };
            var rentalService = new RentalService(GetRepositoryMock());
            //test
            var currentRental = rentalService.GetById(rental.Id);
            //validation
            Assert.AreEqual(rental.Id, currentRental.Id);
            Assert.AreEqual(rental.RentalTypeId, currentRental.RentalTypeId);
        }
        [TestMethod]
        public void Insert()
        {
            //preparation
            var rental = new CompositeRental { Id = 1, RentalTypeId = (int)RentalTypeEnum.StrategyGroup };
            var rentalService = new RentalService(GetRepositoryMock());
            //test
            rentalService.Insert(rental);
            
        }
        [TestMethod]
        public void Update()
        {
            //preparation
            var rental = new CompositeRental { Id = 1, RentalTypeId = (int)RentalTypeEnum.StrategyGroup };
            var rentalService = new RentalService(GetRepositoryMock());
            //test
            rentalService.Update(rental);
        }
        [TestMethod]
        public void Delete()
        {
            //preparation
            var rental = new CompositeRental { Id = 1, RentalTypeId = (int)RentalTypeEnum.StrategyGroup };
            var rentalService = new RentalService(GetRepositoryMock());
            //test
            rentalService.Delete(rental.Id);

        }

        #region Mock data
        private IRepository<Rental> GetRepositoryMock()
        {
            var rentalRepositoryMock = new Mock<IRepository<Rental>>();
            rentalRepositoryMock.Setup(r => r.GetById(1))
                .Returns(new CompositeRental { Id = 1, RentalTypeId = (int)RentalTypeEnum.StrategyGroup });
            rentalRepositoryMock.Setup(r => r.Insert(It.IsAny<Rental>()));
            rentalRepositoryMock.Setup(r => r.Update(It.IsAny<Rental>()));
            rentalRepositoryMock.Setup(r => r.Delete(It.IsAny<int>()));
            return rentalRepositoryMock.Object;
        }
        #endregion
    }
}
