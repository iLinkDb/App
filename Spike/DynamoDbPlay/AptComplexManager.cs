using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoDbPlay
{

    [DynamoDBTable("AptComplex")]
    public class AptComplex
    {
        [DynamoDBHashKey]
        public string State_City
        {
            get { return State + "_" + City; }
            set { }
        }

        [DynamoDBRangeKey]
        public string ComplexName { get; set; }

        public string State { get; set; }
        public string City { get; set; }
    }

    public class AptComplexManager
    {
        private DynamoDBContext _context;

        public AptComplexManager(DynamoDBContext context)
        {
            _context = context;
        }
 
    }
}

