using rentabike.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace rentabike.service
{
    public interface IRentalMessageService
    {
        Task<CompositeRental> Init();
        Task<CompositeRental> AddTo(CompositeRental rental, int rentalTypeId, int quantity);
        Task<CompositeRental> GetResult(CompositeRental rental);

    }
}
