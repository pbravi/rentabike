using Microsoft.EntityFrameworkCore;
using rentabike.model;
using rentabike.model.strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rentabike.data
{
    public class RentalRepository : BaseRepository<Rental>
    {
        public RentalRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
