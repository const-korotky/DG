﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public class DateTimeInterval : Entity
    {
        public bool IsGeneral { get; set; }

        public Order Order { get; set; }
        public Location Location { get; set; }
        public Zone Zone { get; set; }

        public string Note { get; set; }
        public string Description { get; set; }

        public bool IsInactive { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days => (EndDate - StartDate).Days;

        public DateTimeInterval() : base() { }

        public override string ToString()
        {
            return $"DateTimeInterval [Start: {StartDate}][End: {EndDate}][{Order}][{Location}][{Zone}][Note: {Note}][Description: {Description}]";
        }
    }
}
