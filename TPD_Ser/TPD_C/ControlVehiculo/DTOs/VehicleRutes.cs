using System;

namespace TPD_C.ControlVehiculo.DTOs
{
    public class VehicleRutes
    {
        public string Id { get; set; }
        public DateTime StartUtcTimestamp { get; set; }
        public DateTime EndUtcTimestamp { get; set; }
        public decimal Distance { get; set; }
        public decimal Duration { get; set; }
        public decimal StartLatitude { get; set; }
        public decimal StartLongitude { get; set; }
        public string StartLocationDescription { get; set; }
        public decimal EndLatitude { get; set; }
        public decimal EndLongitude { get; set; }
        public string EndLocationDescription { get; set; }
        public decimal NumberOfExceptions { get; set; }
        public decimal NumberOfDriverBehaviourExceptions { get; set; }
        public decimal NumberOfSpeedingExceptions { get; set; }
        public decimal NumberOfExcessiveRPMExceptions { get; set; }
        public decimal NumberOfHarshBrakingExceptions { get; set; }
        public decimal NumberOfExcessiveIdleExceptions { get; set; }
        public decimal NumberOfExcessiveFreewheelingExceptions { get; set; }
        public decimal NumberOfRecklessDrivingExceptions { get; set; }
        public decimal NumberOfExcessiveAccelerationExceptions { get; set; }
        public decimal NumberOfDriverFatiqueExceptions { get; set; }
        public decimal NumberOfAccidentExceptions { get; set; }
        public decimal NumberOfTemperatureExceptions { get; set; }
        public decimal MaxSpeed { get; set; }
        public decimal AvgSpeed { get; set; }
        public decimal MaxRPM { get; set; }
        public DateTime UtcLastModified { get; set; }
        public string Unit_Id { get; set; }
        public string Unit_Description { get; set; }
        public string Vehicle_Id { get; set; }
        public string Vehicle_Description { get; set; }//chofer
        public string Vehicle_Registration { get; set; }//placas

    }
}
