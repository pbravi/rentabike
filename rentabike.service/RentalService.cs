using rentabike.data;
using rentabike.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace rentabike.service
{
    public class RentalService : BaseService<Rental>
    {

        public RentalService(IRepository<Rental> rentalRepository) : base(rentalRepository) { }
        
    }
}
