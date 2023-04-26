using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("Service")]
    public partial class Service
    {
        public Service()
        {
            WorkOnRequests = new HashSet<WorkOnRequest>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(20)]
        public string Code { get; set; } = null!;

        [InverseProperty("Service")]
        public virtual ICollection<WorkOnRequest> WorkOnRequests { get; set; }
    }
}
