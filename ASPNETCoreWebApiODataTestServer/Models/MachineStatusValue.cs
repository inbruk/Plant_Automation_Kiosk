using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETCoreWebApiODataTestServer.Models
{
    public class MachineStatusValue
    {
        [Key, Column(Order = 0)]
        [DataMember]
        public int WorkshopNumber { get; set; }

        [Key, Column(Order = 1)]
        [DataMember]
        public int SiteNumber { get; set; }

        [Key, Column(Order = 2)]
        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public int StatusId { set; get; }

        [DataMember]
        public bool IsCritical { get; set; }
    }
}