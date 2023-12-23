using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public class Person : Entity
    {
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Company { get; set; }

        public readonly List<DateTimeInterval> Inactive;
        public readonly List<DateTimeInterval> Sector;
        public readonly List<DateTimeInterval> Normalized;

        public Person() : base()
        {
            Inactive = new List<DateTimeInterval>();
            Sector = new List<DateTimeInterval>();
            Normalized = new List<DateTimeInterval>();
        }

        public void Normalize(DateTimeInterval interval)
        {
            if (Normalized.Count < 1)
            {
                NormalizeAddNew(interval);
            }
            else
            {
                NormalizeAddNext(interval);
            }
        }
        protected void NormalizeAddNew(DateTimeInterval interval)
        {
            Normalized.Add(
                new DateTimeInterval {
                    ID = interval.ID,
                    Order = interval.Order,
                    Location = interval.Location,
                    Zone = interval.Zone,
                    Note = interval.Note,
                    Description = interval.Description,
                    IsInactive = interval.IsInactive,
                    StartDate = interval.StartDate,
                    EndDate = interval.EndDate,
            });
        }
        protected void NormalizeAddNext(DateTimeInterval interval)
        {
            var current = Normalized.Last();
            if (current.ID != interval.ID)
            {
                if (interval.StartDate < current.EndDate)
                {
                    current.EndDate = interval.StartDate;
                }
                NormalizeAddNew(interval);
            }
        }

        public override string ToString()
        {
            return $"Person [Name: {Name}][Rank: {Rank}][Company: {Company}]";
        }
    }
}
