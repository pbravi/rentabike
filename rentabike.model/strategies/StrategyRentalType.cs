using System;
using System.Collections.Generic;
using System.Text;

namespace rentabike.model.strategies
{
    public class StrategyRentalType
    {
        public int Id { get; set; }
        public int PriceStrategyId { get; set; }
        public virtual Strategy PriceStrategy { get; set; }
        public int RentalTypeId { get; set; }
        public RentalType RentalType { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
