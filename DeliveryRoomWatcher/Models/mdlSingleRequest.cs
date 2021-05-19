using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryRoomWatcher.Models
{
    public class mdlSingleRequest
    {
        public short reqno { get; set; }
        public class SingleRequestApprove
        {
            public short reqno { get; set; }
            public string apprbycode { get; set; }
            public string apprbyname { get; set; }

        }
        public class SingleRequestCancelled
        {
            public short reqno { get; set; }
            public string cancelledbycode { get; set; }
            public string cancelledbyname { get; set; }

        }
    }
}
