﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public class Person
    {
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Company { get; set; }

        public readonly List<DateTimeInterval> Sector;
        public readonly List<DateTimeInterval> Absent;

        public Person()
        {
            Sector = new List<DateTimeInterval>();
            Absent = new List<DateTimeInterval>();
        }

        public override string ToString()
        {
            return $"Person [Name: {Name}][Rank: {Rank}][Company: {Company}]";
        }
    }
}