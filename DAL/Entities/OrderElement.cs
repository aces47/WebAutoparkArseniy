using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class OrderElement
    {
        public int OrderElementId { get; set; }
        [DisplayName("Order number")]
        public int OrderId { get; set; }
        [DisplayName("Part")]
        public int DetailId { get; set; }
        public Detail Part { get; set; }
        [DisplayName("Count")]
        public int DetailCount { get; set; }
    }
}
