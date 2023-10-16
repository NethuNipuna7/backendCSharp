/*Developed:it2204648 Nethu nipuna m 
 * Function: Shedule Management
 * FileName:TrainScheduleController
 * Usage: BackEndApi
 */

using Ead_backend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Ead_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainScheduleController : ControllerBase
    {
        private readonly TrainScheduleRepository _repository;

        public TrainScheduleController(TrainScheduleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TrainScheduleItem>>> Get()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }
        //get details by id
        [HttpGet("{id}", Name = "GetTrainSchedule")]
        public async Task<ActionResult<TrainScheduleItem>> GetById(string id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        //create shedules
       
        [HttpPost]
        public async Task<IActionResult> Create(TrainScheduleItem item)
        {
           
            item.Id = null;

            await _repository.CreateAsync(item);
            return CreatedAtRoute("GetTrainSchedule", new { id = item.Id }, item);
        }
        //update train shedule
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, TrainScheduleItem item)
        {
            var existingItem = await _repository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            await _repository.UpdateAsync(id, item);
            return NoContent();
        }
        //Delete train shedule
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
