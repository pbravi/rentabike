using rentabike.model.strategies;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace rentabike.model
{
    public abstract class Rental
    {
        #region Properties

        public int Id { get; set; }
        public int RentalTypeId { get; set; }
        public RentalType Type { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        #endregion

        #region Virtual Methods

        public virtual bool IsComposite()
        {
            return Type.IsComposite;
        }

        #endregion
    }
}
