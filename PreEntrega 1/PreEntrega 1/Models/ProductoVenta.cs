using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreEntrega_1.Models
{
    internal class ProductoVenta
    {
        public long Id {get; set; }
        public int Stock { get; set; }
        public long IdProducto { get; set; }
        public long IdVenta {get; set; }
    }
}
