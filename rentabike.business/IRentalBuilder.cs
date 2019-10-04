using rentabike.model;
using System;

namespace rentabike.helpers
{
    public interface IRentalBuilder
    {
        void Add(Rental rental);
    }
}
