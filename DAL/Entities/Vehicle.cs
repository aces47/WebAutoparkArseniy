using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Type")]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        [Display(Name = "Model")]
        public string ModelName { get; set; }
        [Display(Name = "State Number")]
        public string StateNumber { get; set; }
        [Display(Name = "Manufacture Year")]
        public int ManufactureYear { get; set; }
        public double Mileage { get; set; }
        public double Weight { get; set; }
        [Display(Name = "Engine Type")]
        public string EngineType { get; set; }
        [Display(Name = "Engine Capacity")]
        public double EngineCapacity { get; set; }
        [Display(Name = "Engine Consumption")]
        public double EngineConsumption { get; set; }
        [Display(Name = "Tank Capacity")]
        public double TankCapacity { get; set; }
        [Display(Name = "Tax")]
        public double TaxPerMonth => Weight * _weightCoefficient
                                   + VehicleType.TaxCoefficient * _taxMultiplier
                                   + _taxBasicShift;
        [Display(Name = "Max km")]
        public double MaxKm => TankCapacity / EngineConsumption;
        public Color Color { get; set; }
    }
}
