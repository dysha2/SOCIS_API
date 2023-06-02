using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("RequestStatus")]
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            Requests = new HashSet<Request>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(20)]
        public string Code { get; set; } = null!;

        [InverseProperty("RequestStatus")]
        public virtual ICollection<Request> Requests { get; set; }
    }
}
