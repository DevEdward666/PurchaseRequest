using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryRoomWatcher.Models
{
    public class ListofItemDetails
    {
     
            public string prno { get; set; }
            public short lineno { get; set; }
            public string linestatus { get; set; }
            public string deptcode { get; set; }
            public string sectioncode { get; set; }
            public string stockcode { get; set; }
            public string stockdesc { get; set; }
            public int prqty { get; set; }
            public string unitdesc { get; set; }
            public float prprice { get; set; }
            public string itemtrantype { get; set; }
            public string itemremarks { get; set; }
        
    }
}
