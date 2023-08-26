using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ProductDataModel
    {
        public int Id { get; set; } = 0;
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
    }
}
