using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rentabike.model;


namespace rentabike.service
{
    /// <summary>
    /// Help with message queue processing
    /// </summary>
    public class RentalMessageService : IRentalMessageService
    {
        private readonly IRentalBuilder builder;

        public RentalMessageService(IRentalBuilder builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Async processing for init a rental
        /// </summary>
        /// <returns></returns>
        public async Task<CompositeRental> Init()
        {
            return await Task.Run(() => builder.Init());
        }

        /// <summary>
        /// Async processing for add a child to a rental
        /// </summary>
        /// <param name="rental"></param>
        /// <param name="rentalTypeId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<CompositeRental> AddTo(CompositeRental rental, int rentalTypeId, int quantity)
        {
            return await Task.Run(() => builder.AddTo(rental, rentalTypeId, quantity));
        }

        /// <summary>
        /// Async processing for get CompositeRental result
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        public async Task<CompositeRental> GetResult(CompositeRental rental)
        {
            return await Task.Run(() => builder.GetResult(rental));            
        }


    }
}
