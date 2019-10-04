using System;
using System.Collections.Generic;
using System.Text;

namespace rentabike.model
{
    public class CompositeRental : Rental
    {
        public virtual ICollection<Rental> Childrens { get; set; }
        public CompositeRental()
        {
            Childrens = new List<Rental>();
        }

        public void Add(Rental children)
        {
            Childrens.Add(children);
        }
        public void Add(IList<Rental> childrens)
        {
            ((List<Rental>)Childrens).AddRange(childrens);
        }
        public double GetPrice()
        {
            var totalPrice = 0.0;
            foreach (var children in Childrens)
            {
                totalPrice += children.Price;
            }
            return totalPrice;
        }
    }
}
