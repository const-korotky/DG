using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace DocGen.Data.Model
{
    public class SectorItem : Entity
    {
        public bool IsNew { get; set; } = false;
        public bool IsDirty { get; set; } = false;

        public string OrderName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Persons { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string ZoneName { get; set; }
    }
}
