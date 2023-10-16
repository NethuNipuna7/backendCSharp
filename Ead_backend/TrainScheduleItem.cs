/*Developed:it2204648 Nethu nipuna m 
 * Function: Shedule Management
 * FileName:TrainScheduleItem
 * Usage: BackEndApi
 */

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ead_backend
{
    // TrainScheduleItem class represents an item in the train schedule.
    public class TrainScheduleItem
    {
        // The [BsonId] attribute specifies that this property is the document's primary key.
        // The [BsonRepresentation] attribute indicates the BSON type of the ObjectId.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Train_id { get; set; }
        // TrainName property stores the name of the train.
        public string TrainName { get; set; }

        // DepartureStation property stores the name of the departure station.
        public string DepartureStation { get; set; }

        // ArrivalStation property stores the name of the arrival station.
        public string ArrivalStation { get; set; }

        // DepartureTime property stores the time of departure.
        public string DepartureTime { get; set; }

        // ArrivalTime property stores the time of arrival.
        public string ArrivalTime { get; set; }
    }
}
