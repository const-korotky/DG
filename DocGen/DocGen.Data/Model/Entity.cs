using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public abstract class Entity
    {
        private static int NextID = 0;
        public int ID { get; set; }

        protected Entity() { ID = ++NextID; }
    }
}
