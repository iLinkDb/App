using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlinkDb.Data.PivotalApi
{
    public class PivotApiResponse
    {
        public string Xml { get; set; }
        public int NewId { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
