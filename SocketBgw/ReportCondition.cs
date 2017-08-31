using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysLogs
{
    public class ReportCondition
    {
        private DateTime _fromDate;
        public DateTime FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }

        private DateTime _toDate;
        public DateTime ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }

        private string _locations;
        public string Location
        {
            get { return _locations; }
            set { _locations = value; }
        }

        private string _locationPath;
        public string LocationPath
        {
            get { return _locationPath; }
            set { _locationPath = value; }
        }

        private int _groupId;
        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        private string _groupName;
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }

        private int _resourceId;
        public int ResourceId
        {
            get { return _resourceId; }
            set { _resourceId = value; }
        }

        private string _resourceName;
        public string ResourceName
        {
            get { return _resourceName; }
            set { _resourceName = value; }
        }

        private string _method;
        public string Method
        {
            get { return _method; }
            set { _method = value; }
        }

        private bool _includeSaturday;
        public bool IncludeSaturday
        {
            get { return _includeSaturday; }
            set { _includeSaturday = value; }
        }

        private bool _includeSunday;
        public bool IncludeSunday
        {
            get { return _includeSunday; }
            set { _includeSunday = value; }
        }

        private float _upperThreshold;
        public float UpperThreshold
        {
            get { return _upperThreshold; }
            set { _upperThreshold = value; }
        }

        private float _lowerThreshold;
        public float LowerThreshold
        {
            get { return _lowerThreshold; }
            set { _lowerThreshold = value; }
        }

        /// <summary>
        /// Return total days from FromDate to ToDate, exclude weekend days if needed
        /// </summary>
        public int NumberOfAvailableDayInTimeRange
        {
            get
            {
                int totalday = 0;
                for (DateTime d = FromDate; d < ToDate; d = d.AddDays(1))
                {
                    if (d.DayOfWeek == DayOfWeek.Saturday)
                    {
                        if (_includeSaturday)
                        {
                            totalday = totalday + 1;
                        }
                    }
                    else
                    {
                        if (d.DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (_includeSunday)
                            {
                                totalday = totalday + 1;
                            }
                        }
                        else
                        {
                            totalday = totalday + 1;
                        }
                    }
                }
                return totalday;
            }
        }

        /// <summary>
        /// Option to run report, default is opening hours.
        /// </summary>
        private Options _option = Options.OpeningHours;
        /// <summary>
        /// Option to run report, default is opening hours.
        /// </summary>
        public Options Option
        {
            get { return _option; }
            set { _option = value; }
        }

        private DateTime _startTime = DateTime.Now;
        /// <summary>
        /// The start time specified by user.
        /// </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        private DateTime _endTime = DateTime.Now;

        /// <summary>
        /// The end time specified by user.
        /// </summary>
        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                if (_endTime.Hour == 0 && EndTime.Minute == 0)
                {
                    //It is a special value
                    _isMaxEndTime = true;
                }
                else
                {
                    _isMaxEndTime = false;
                }
            }
        }

        private bool _isMaxEndTime = false;

        public bool IsMaxEndTime
        {
            get { return _isMaxEndTime; }
        }

    }
    public enum Options
    {
        OpeningHours = 0,
        SpecifiedHours = 1
    }
}
