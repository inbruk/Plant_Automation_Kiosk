using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETCoreWebApiODataTestServer.DataAccessLayer
{
    [DataContract]
    public partial class Workshop
    {
        [Key]
        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int StatusId { get; set; }

        [IgnoreDataMember]
        public Status Status { get; set; }
    }
}
