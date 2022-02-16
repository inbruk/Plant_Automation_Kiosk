using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebApiODataTestServer.DataAccessLayer
{
    [DataContract]
    public partial class CrashRequestLog
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string MachineInventoryNumber { get; set; }

        [DataMember]
        public int NewStatusId { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public int KioskNumber { get; set; }

        [DataMember]
        public int? ConfirmationId { get; set; }

        [IgnoreDataMember]
        public Machine MachineInventoryNumberNavigation { get; set; }

        [IgnoreDataMember]
        public Status NewStatus { get; set; }

        public PassLeanLog Confirmation { get; set; }
    }
}
