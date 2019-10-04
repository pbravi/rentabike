
using rentabike.model;
using rentabike.model.enumerations;
using System.Threading.Tasks;

namespace rentabike.service
{
    public interface IRentalBuilder
    {
        CompositeRental Init();
        CompositeRental AddTo(CompositeRental rental, int rentalTypeId, int quantity);
        CompositeRental GetResult(CompositeRental rental);
    }
}
