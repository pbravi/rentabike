using rentabike.model.strategies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using rentabike.model;
using rentabike.model.enumerations;
using rentabike.data;

namespace rentabike.service
{
    /// <summary>
    /// Manage work related to determinate the price and help with composite building for a rental (based on a strategy)
    /// </summary>
    public class StrategyService : BaseService<Strategy>, IStrategyService
    {
        /// <summary>
        /// Strategy to determinate prices and composite groups
        /// </summary>
        private Strategy strategy;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="strategyRepository"></param>
        public StrategyService(IRepository<Strategy> strategyRepository):base(strategyRepository)
        {
            //If strategy is null get current strategy (Family Rental)
            if (strategy == null)
                strategy = strategyRepository.GetById((int)PriceStrategyEnum.Family);
        }

        /// <summary>
        /// Get composite rental tree from a List of leaf rentals
        /// </summary>
        /// <param name="rentals">Leaf rentals to process</param>
        /// <returns></returns>
        public CompositeRental GetRental(IList<Rental> rentals)
        {
            while (rentals.Any())
            {
                //If rentals count is lower than min of strategy range (3)
                if (rentals.Count < strategy.MinCompositeSize)
                {
                    //Is a normal group (without discount)
                    var normalRental = new CompositeRental { RentalTypeId = (int)RentalTypeEnum.NormalGroup };
                    normalRental.Add(rentals);
                    //Set price for composite rental
                    SetPrice(normalRental);
                    return normalRental;
                }
                else
                {
                    //Get min rentals of strategy range (3)
                    var minRentals = rentals.Take(strategy.MinCompositeSize).ToList();
                    //Is a strategy group (with discount)
                    var strategyRental = new CompositeRental { RentalTypeId = (int)RentalTypeEnum.StrategyGroup };
                    strategyRental.Add(minRentals);
                    //Get rest of rentals (total rentals - min rentals of strategy)
                    var restRentals = rentals.Except(minRentals).ToList();
                    //If rest of rentals fit into strategy size (3..5)
                    if (restRentals.Count() <= (strategy.MaxCompositeSize - strategy.MinCompositeSize))
                    {
                        //Add rest of rentals into strategy group rental
                        strategyRental.Add(restRentals);
                        //Set price for strategy group rental
                        SetPrice(strategyRental);
                        return strategyRental;
                    }
                    else
                    {
                        //Rest of rentals are greater than strategy size (3..5)
                        //Set price for strategy group rental
                        SetPrice(strategyRental);
                        //Create a container rental
                        var containerRental = new CompositeRental { RentalTypeId = (int)RentalTypeEnum.NormalGroup };
                        //Add strategy rental as a child
                        containerRental.Add(strategyRental);
                        //Recursive call with rest of rentals
                        containerRental.Add(GetRental(restRentals));
                        //Set price for container rental
                        SetPrice(containerRental);
                        return containerRental;
                    }
                }
            }
            //If list of rentals is empty return an empty container
            return new CompositeRental { RentalTypeId = (int)RentalTypeEnum.NormalGroup };
        }

        /// <summary>
        /// Set price for a rental using a strategy
        /// </summary>
        /// <param name="rental">Rental to set price</param>
        public void SetPrice(Rental rental)
        {
            //If strategy not found throw an exception
            if (strategy == null || strategy.StrategyRentalTypes == null)
                throw new Exception("Strategy not found");

            //Get strategy detail for rental type (by hour, by day, by week, strategy rental, normal group)
            var rentalType = strategy.StrategyRentalTypes.FirstOrDefault(f => f.RentalTypeId == rental.RentalTypeId);
            //If strategy detail not found throw an exception
            if (rentalType == null)
                throw new Exception($"Strategy {strategy.Description} not support this rental type ({rental.RentalTypeId})");
            //Set rental type
            rental.Type = rentalType.RentalType;
            //Determine price based on rental type (by hour, by day, by week, strategy rental, normal group)
            switch (rental.RentalTypeId)
            {
                case (int)RentalTypeEnum.ByHour:
                case (int)RentalTypeEnum.ByDay:
                case (int)RentalTypeEnum.ByWeek:
                    //Normal calculation (Quantity*Price)
                    rental.Price = Math.Round(rental.Quantity * rentalType.Price);
                    break;
                case (int)RentalTypeEnum.StrategyGroup:
                case (int)RentalTypeEnum.NormalGroup:
                    //Get group price
                    rental.Price = ((CompositeRental)rental).GetPrice();
                    //If it's a strategy group apply discount
                    if(rental.RentalTypeId == (int)RentalTypeEnum.StrategyGroup)
                        rental.Price = Math.Round(rental.Price * (1 - rentalType.Discount));
                    break;
                default:
                    break;
            }
        }
    }
}
