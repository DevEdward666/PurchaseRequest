﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryRoomWatcher.Models
{
    public class ListOfItems
    {
        public string prno { get; set; }
        public string deptcode { get; set; }
        public string deptname { get; set; }
        public string sectioncode { get; set; }
        public string reqdate { get; set; }
        public string reqby { get; set; }
        public string apprbycode { get; set; }
        public string apprbyname { get; set; }
        public string apprdate { get; set; }
        public string todept { get; set; }
        public string tosection { get; set; }
        public string issueno { get; set; }
        public string reqtype { get; set; }
        public string reqstatus { get; set; }
        public string trantype { get; set; }
        public string headapprovebycode { get; set; }
        public string headapprovebyname { get; set; }
        public string headdateapprove { get; set; }
        public string cancelledbycode { get; set; }
        public string cancelledbyname { get; set; }
        public string datecancelled { get; set; }
        public string reqremarks { get; set; }
        public string encodedby { get; set; }
        public float total_price { get; set; }

      public List<ListofItemDetails> pritems { get; set; }

    }
}
