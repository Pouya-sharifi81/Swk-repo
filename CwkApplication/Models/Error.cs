using CwkApplication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Models
{
    public class Error
    {
        public ErrorCode  code { get; set; }
        public string Massage { get; set; }
    }
}
