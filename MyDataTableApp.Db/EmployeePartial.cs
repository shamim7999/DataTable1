using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDataTableApp.Db
{
    public partial class Employee
    {
        //[NotMapped]
        public int TotalCount { get; set; }
    }
}
