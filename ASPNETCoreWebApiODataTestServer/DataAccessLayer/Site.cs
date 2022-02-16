using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETCoreWebApiODataTestServer.DataAccessLayer
{
    [DataContract]
    public partial class Site
    {
        [Key, Column(Order = 0)]
        public int WorkshopNumber { get; set; }

        [Key, Column(Order = 1)]
        public int Number { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int StatusId { get; set; }

        [IgnoreDataMember]
        public Status Status { get; set; }
    }
}
