using Microsoft.EntityFrameworkCore;
using rentabike.model.strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rentabike.data
{
    public class StrategyRepository : BaseRepository<Strategy>
    {
        public StrategyRepository(AppDbContext dbContext) : base(dbContext) { }

        public override Strategy GetById(int id)
        {
            return base.DbSet.Include(s => s.StrategyRentalTypes).FirstOrDefault(s => s.Id == id);
        }
    }
}
