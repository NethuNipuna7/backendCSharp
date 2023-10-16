/*Developed:it2204648 Nethu nipuna m 
 * Function: Shedule Management
 * FileName:TrainScheduleRepository
 * Usage: BackEndApi
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Ead_backend.Data; // Make sure to import the correct namespace for TrainScheduleItem

namespace Ead_backend.Data
{
    public class TrainScheduleRepository
    {
        private readonly IMongoCollection<TrainScheduleItem> _collection;

        // Constructor for initializing the repository with a MongoDB collection
        public TrainScheduleRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<TrainScheduleItem>("TrainSchedule");
        }

        // Retrieve all train schedule items asynchronously
        public async Task<List<TrainScheduleItem>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        // Retrieve a train schedule item by its unique identifier asynchronously
        public async Task<TrainScheduleItem> GetByIdAsync(string id)
        {
            // Use the provided id to find a matching item
            return await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();
        }

        // Create a new train schedule item asynchronously
        public async Task CreateAsync(TrainScheduleItem item)
        {
            await _collection.InsertOneAsync(item);
        }

        // Update an existing train schedule item asynchronously
        public async Task UpdateAsync(string id, TrainScheduleItem item)
        {
            // Replace the existing item with the new item using its unique identifier
            await _collection.ReplaceOneAsync(i => i.Id == id, item);
        }

        // Delete a train schedule item by its unique identifier asynchronously
        public async Task DeleteAsync(string id)
        {
            // Delete the item with the provided id
            await _collection.DeleteOneAsync(i => i.Id == id);
        }
    }
}
