using System;
using System.Collections.Generic;
using System.Text;

namespace JobSearch.APP.Models
{
    public class SearchParams
    {
        public string Word { get; set; }
        public string CityState { get; set; }
        public int NumberOfPage { get; set; }
    }
}
