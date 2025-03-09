using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingSystem.DAL;

namespace ParkingSystem.BLL
{
    public class ParkingService
    {
        private readonly ParkingRepository _repository;

        public ParkingService()
        {
            _repository = new ParkingRepository();
        }

        public IEnumerable<Gate> GetAllGates()
        {
            return _repository.GetGates();
        }
        
        public IEnumerable<ParkingSpace> GetAvailableSpacesForGate(int gateId)
        {
            return _repository.GetAvailableSpaces(gateId);
        }

        public int ParkVehicle(string vehicleNumber, int SpaceId)
        {
            return _repository.ParkVehicle(vehicleNumber, SpaceId);
        }

        public void UnparkVehicle(int parkingId)
        {
            _repository.UnparkVehicle(parkingId);
        }

        public int GetSpaceId(string vehicleNumber)
        {
            return _repository.GetSpaceId(vehicleNumber);
        }

        public decimal CalculateCharges(int parkingId)
        {
            return _repository.CalculateCharges(parkingId);
        }

        public DateTime? GetEstimatedFreeTime(int spaceID)
        {
            return _repository.GetEstimatedFreeTime(spaceID);
        }
    }
}
