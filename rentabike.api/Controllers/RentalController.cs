using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using rentabike.model;
using rentabike.model.enumerations;
using rentabike.service;

namespace rentabike.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalBuilder rentalBuilder;
        private readonly BaseService<Rental> rentalService;

        public RentalController(IRentalBuilder rentalBuilder, BaseService<Rental> rentalService)
        {
            this.rentalBuilder = rentalBuilder;
            this.rentalService = rentalService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var rental = rentalBuilder.Init();
            rentalBuilder.AddTo(rental, (int)RentalTypeEnum.ByHour, 2);
            rentalBuilder.AddTo(rental, (int)RentalTypeEnum.ByDay,1);
            rentalBuilder.AddTo(rental, (int)RentalTypeEnum.ByWeek, 1);
            rentalBuilder.AddTo(rental, (int)RentalTypeEnum.ByHour, 8);
            rentalBuilder.AddTo(rental, (int)RentalTypeEnum.ByHour, 2);
            rentalBuilder.AddTo(rental, (int)RentalTypeEnum.ByDay, 1);
            rentalBuilder.AddTo(rental, (int)RentalTypeEnum.ByWeek, 1);
            rentalBuilder.AddTo(rental, (int)RentalTypeEnum.ByHour, 8);
            rentalBuilder.GetResult(rental);
            return new string[] { rental.Price.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
