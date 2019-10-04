using rentabike.model.enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rentabike.model.strategies
{
    public class Strategy
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int MinCompositeSize { get; set; }
        public int MaxCompositeSize { get; set; }

        public List<StrategyRentalType> StrategyRentalTypes { get; set; }


    }
}
