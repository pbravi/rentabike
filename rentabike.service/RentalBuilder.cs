using System;
using System.Collections.Generic;
using rentabike.model;
using rentabike.model.enumerations;
using rentabike.model.strategies;
using System.Linq;
using System.Threading.Tasks;

namespace rentabike.service
{
    /// <summary>
    /// Help with the rental creation
    /// </summary>
    public class RentalBuilder : IRentalBuilder
    {
        private readonly IStrategyService strategyService;
        private readonly BaseService<Rental> rentalService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="strategyService"></param>
        /// <param name="rentalService"></param>
        public RentalBuilder(IStrategyService strategyService, BaseService<Rental> rentalService)
        {
            this.strategyService = strategyService;
            this.rentalService = rentalService;
        }

        /// <summary>
        /// Initialize a new CompositeRental
        /// </summary>
        /// <returns></returns>
        public CompositeRental Init()
        {
            var rental = new CompositeRental { RentalTypeId = (int)RentalTypeEnum.NormalGroup };
            rentalService.Insert(rental);
            return rental;

        }

        /// <summary>
        /// Add a new LeafRental (child) to a CompositeRental container
        /// </summary>
        /// <param name="rental">Rental container</param>
        /// <param name="rentalTypeId">Type of LeafRental to be added</param>
        /// <param name="quantity">Quantity of LeafRental to be added</param>
        /// <returns></returns>
        public CompositeRental AddTo(CompositeRental rental, int rentalTypeId, int quantity)
        {
            var children = new LeafRental() { RentalTypeId = rentalTypeId, Quantity = quantity };
            //Set price of child before to be added
            strategyService.SetPrice(children);
            rental.Childrens.Add(children);
            return rental;
        }

        /// <summary>
        /// Get composite rental tree from a List of leaf rentals
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        public CompositeRental GetResult(CompositeRental rental)
        {
            rental = strategyService.GetRental(rental.Childrens.ToList());
            return rental;
        }
    }
}
