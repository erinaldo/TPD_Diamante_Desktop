using System.Collections.Generic;

namespace TPD_C.ControlVehiculo.DTOs
{
    public class ResponseCollection
    {
        public string OdataContext { get; set; }

        public List<VehicleRutes> Value { get; set; }
    }
}
