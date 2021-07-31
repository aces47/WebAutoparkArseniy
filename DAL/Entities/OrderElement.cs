using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class OrderElement
    {
        public int OrderElementId { get; set; }
        public int OrderId { get; set; }
        public int DetailId { get; set; }
        public Detail Part { get; set; }
        public int DetailQuantity { get; set; }
    }
}
