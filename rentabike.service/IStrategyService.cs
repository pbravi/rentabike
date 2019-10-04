using rentabike.model.strategies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using rentabike.model;
using rentabike.model.enumerations;

namespace rentabike.service
{
    public interface IStrategyService
    {
        void SetPrice(Rental rental);
        CompositeRental GetRental(IList<Rental> pRentals);
    }
}
