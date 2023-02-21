using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraApiV2.Models
{
    public class ProductoVenta
    {
        public long Id {get; set; }
        public int Stock { get; set; }
        public long IdProducto { get; set; }
        public long IdVenta {get; set; }
    }
}
