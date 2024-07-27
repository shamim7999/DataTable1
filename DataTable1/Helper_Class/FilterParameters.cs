using DataTable1.Binder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTable1.Helper_Class
{
    public class FilterParameters
    {
        public int? draw { get; set; }
        public int? id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string office { get; set; }
        public int? age { get; set; }
        public int? salary { get; set; }

        public int start { get; set; }
        public int length { get; set; }
        
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }
}