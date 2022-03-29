using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core.DTO
{
    public class SolarPanelRecord
    {
        public string Section { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Year { get; set; }
        public MaterialMode Material { get; set; }
        public string IsTracking { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Panels in Section: {Section}");
            sb.AppendLine("Row Col Year Material Tracking");
            sb.AppendLine($"  {Row.ToString()}  {Column.ToString()}  {Year.ToString()}  {Material.ToString()}  {IsTracking.ToString()}");
            sb.AppendLine("----------------------");

            return sb.ToString();
        }
    }
}
