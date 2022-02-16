using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebApiODataTestServer.DataAccessLayer
{
    [DataContract]
    public partial class PassLeanLog
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int KioskNumber { get; set; }

        [DataMember]
        public string PassNumber { get; set; }

        [DataMember]
        public int  IDPERNR { get; set; }

        [DataMember]
        public bool IsConfirmed { get; set; }

        [DataMember]
        public bool IsUsed { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [IgnoreDataMember]
        public ICollection<CrashRequestLog> CrashRequestLog { get; set; }
    }
}
