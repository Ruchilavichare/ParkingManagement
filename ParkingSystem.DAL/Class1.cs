using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem.DAL
{
    public class ParkingRepository
    {
        private readonly ParkingDBEntities _dbContext; // ParkingDBEntities is auto-generated from EF.

        public ParkingRepository()
        {
            _dbContext = new ParkingDBEntities();
        }

        public IEnumerable<Gate> GetGates()
        {
            return _dbContext.Gates.ToList();
        }

        public IEnumerable<ParkingSpace> GetAvailableSpaces(int gateId)
        {
            return _dbContext.ParkingSpaces
                .Where(space => space.GateId == gateId && (!space.IsOccupied.HasValue || !space.IsOccupied.Value)) // Safely check nullable bool
                .ToList();
        }

        public ParkingRate GetParkingRates()
        {
            return _dbContext.ParkingRates.FirstOrDefault();
        }

        public int ParkVehicle(string vehicleNumber, int SpaceId)
        {

            // Find the first available space for the given gate
            var availableSpace = _dbContext.ParkingSpaces
                .FirstOrDefault(space => space.SpaceId == SpaceId && !(space.IsOccupied ?? false));

            if (availableSpace == null)
                throw new Exception("No available parking spaces.");

            // Create a new ParkedVehicle object
            var parkedVehicle = new ParkedVehicle
            {
                VehicleNumber = vehicleNumber,
                SpaceId = SpaceId,
                StartTime = DateTime.Now
            };

            // Mark the parking space as occupied
            availableSpace.IsOccupied = true;

            // Add the new parked vehicle to the database
            _dbContext.ParkedVehicles.Add(parkedVehicle);

            // Save changes to the database
            _dbContext.SaveChanges();

            // Return the ID of the newly created ParkedVehicle
            return parkedVehicle.ParkingId;
        }

        public void UnparkVehicle(int parkingId)
        {
            var parkedVehicle = _dbContext.ParkedVehicles.Find(parkingId);

            if (parkedVehicle == null)
                throw new Exception("Vehicle not found.");

            var parkingSpaces = _dbContext.ParkingSpaces
                .FirstOrDefault(parkingSpace => parkingSpace.SpaceId == parkedVehicle.SpaceId && (parkingSpace.IsOccupied ?? false) == true);

        
            if (parkingSpaces != null)
            {
                parkingSpaces.IsOccupied = false;
            }

            parkedVehicle.EndTime = DateTime.Now;
            _dbContext.SaveChanges();
        }

        public int GetSpaceId(string vehicleNumber)
        {
            var SpaceId = _dbContext.ParkedVehicles
            .Where(v => v.VehicleNumber == vehicleNumber && v.EndTime == null)  // Ensure vehicle has not exited
            .Select(v => (int)v.SpaceId)
            .FirstOrDefault();
            
            return SpaceId;
        }

        public decimal CalculateCharges(int parkingId)
        {
            var parkedVehicle = _dbContext.ParkedVehicles.Find(parkingId);
            var rates = GetParkingRates();

            if (parkedVehicle == null || rates == null)
                throw new Exception("Invalid data.");

            var endTime = parkedVehicle.EndTime ?? DateTime.Now; // Use DateTime.Now if EndTime is null
            var hoursParked = endTime.Subtract(parkedVehicle.StartTime ?? DateTime.Now).TotalHours;

            var totalCharge = (decimal)hoursParked * rates.HourlyRate;

            return totalCharge ?? 0;
        }
        public DateTime? GetEstimatedFreeTime(int spaceID)
        {
            var nextAvailable = _dbContext.ParkedVehicles
                                .Where(v => v.SpaceId == spaceID && v.EndTime != null)
                                .OrderBy(v => v.EndTime)
                                .Select(v => v.EndTime)
                                .FirstOrDefault();
            return nextAvailable;
        }
    }
}
