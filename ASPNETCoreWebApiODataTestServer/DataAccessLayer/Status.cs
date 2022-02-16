using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETCoreWebApiODataTestServer.DataAccessLayer
{
    [DataContract]
    public partial class Status
    {
        public Status()
        {
            CrashRequestLog = new HashSet<CrashRequestLog>();
            Machine = new HashSet<Machine>();
            Site = new HashSet<Site>();
            Workshop = new HashSet<Workshop>();
        }

        [Key]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [IgnoreDataMember]
        public ICollection<CrashRequestLog> CrashRequestLog { get; set; }

        [IgnoreDataMember]
        public ICollection<Machine> Machine { get; set; }

        [IgnoreDataMember]
        public ICollection<Site> Site { get; set; }

        [IgnoreDataMember]
        public ICollection<Workshop> Workshop { get; set; }
    }
}
