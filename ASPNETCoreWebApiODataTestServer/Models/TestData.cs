using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebApiODataTestServer.Models
{
    public class TestData
    {
        [Key]
        [DataMember]
        public int Id { set; get; }
    }
}
