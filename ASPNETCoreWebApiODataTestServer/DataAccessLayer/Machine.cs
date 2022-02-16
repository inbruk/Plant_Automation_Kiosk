using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebApiODataTestServer.DataAccessLayer
{
    [DataContract]
    public partial class Machine
    {
        public Machine()
        {
            CrashRequestLog = new HashSet<CrashRequestLog>();
            Place = new HashSet<Place>();
        }

        [Key]
        [DataMember]
        public string InventoryNumber { get; set; }

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

        [IgnoreDataMember]
        public Status Status { get; set; }

        [IgnoreDataMember]
        public ICollection<CrashRequestLog> CrashRequestLog { get; set; }

        [IgnoreDataMember]
        public ICollection<Place> Place { get; set; }
    }
}
