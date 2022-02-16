using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETCoreWebApiODataTestServer.DataAccessLayer
{
    [DataContract]
    public class vwMachinePlace
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
        public string CurrMachineInvNum { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public string Model { get; set; }

        [DataMember]
        public bool IsCritical { get; set; }

        [DataMember]
        public DateTime? ProductionDate { get; set; }

        [DataMember]
        public int StatusId { get; set; }
    }
}
