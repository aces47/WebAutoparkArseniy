using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Vehicle
    {
        private const int _taxBasicShift = 5;
        private const int _taxMultiplier = 30;
        private const double _weightCoefficient = 0.0013;

        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public string ModelName { get; set; }
        public string StateNumber { get; set; }
        public int ManufactureYear { get; set; }
        public double Mileage { get; set; }
        public double Weight { get; set; }
        public string EngineType { get; set; }
        public double EngineCapacity { get; set; }
        public double EngineConsumption { get; set; }
        public double TankCapacity { get; set; }
        public double TaxPerMonth => Weight * _weightCoefficient
                                   + VehicleType.TaxCoefficient * _taxMultiplier
                                   + _taxBasicShift;
        public double MaxKm => TankCapacity / EngineConsumption;
        public Color Color { get; set; }
    }
}
