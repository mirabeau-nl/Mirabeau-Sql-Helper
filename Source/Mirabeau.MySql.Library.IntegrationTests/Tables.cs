using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirabeau.MySql.Library.IntegrationTests
{
    public class Table
    {
        public string Catelog { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Engine { get; set; }
        public long Version { get; set; }
        public string RowFormat { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
