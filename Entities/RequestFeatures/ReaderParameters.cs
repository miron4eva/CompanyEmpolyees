using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class ReaderParameters : RequestParameters
    {
        public ReaderParameters()
        {
            OrderBy = "name";
        }
        public string SearchTerm { get; set; }
    }
}
