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
        public static void ResetID(int @base = 0) { NextID = @base; }

        public int ID { get; set; }

        protected Entity() { ID = ++NextID; }
    }

    public abstract class ColorfulEntity : Entity
    {
        public double Color { get; set; }
        public double FontColor { get; set; }

        protected ColorfulEntity() : base() { }
    }
}
