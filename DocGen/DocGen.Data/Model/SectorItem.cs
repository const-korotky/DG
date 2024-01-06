using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace DocGen.Data.Model
{
    public class SectorItem : Entity, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void FirePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _orderName;
        public string OrderName
        {
            get { return _orderName; }
            set
            {
                if (_orderName != value)
                {
                    _orderName = value;
                    FirePropertyChanged("OrderName");
                }
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    FirePropertyChanged("StartDate");
                }
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    FirePropertyChanged("EndDate");
                }
            }
        }

        private string _persons;
        public string Persons
        {
            get { return _persons; }
            set
            {
                if (_persons != value)
                {
                    _persons = value;
                    FirePropertyChanged("Persons");
                }
            }
        }

        private string _locationName;
        public string LocationName
        {
            get { return _locationName; }
            set
            {
                if (_locationName != value)
                {
                    _locationName = value;
                    FirePropertyChanged("LocationName");
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    FirePropertyChanged("Description");
                }
            }
        }

        private string _note;
        public string Note
        {
            get { return _note; }
            set
            {
                if (_note != value)
                {
                    _note = value;
                    FirePropertyChanged("Note");
                }
            }
        }

        private string _zoneName;
        public string ZoneName
        {
            get { return _zoneName; }
            set
            {
                if (_zoneName != value)
                {
                    _zoneName = value;
                    FirePropertyChanged("ZoneName");
                }
            }
        }
    }
}
