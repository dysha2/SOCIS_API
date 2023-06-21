using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("Request")]
    public partial class Request
    {
        public Request()
        {
            WorkOnRequests = new HashSet<WorkOnRequest>();
        }

        [Key]
        public int Id { get; set; }
        public int DeclarantId { get; set; }
        [Precision(0)]
        public DateTime DateTimeStart { get; set; }
        [StringLength(1000)]
        public string Description { get; set; } = null!;
        [Precision(0)]
        public DateTime? DateTimeEnd { get; set; }
        public int PlaceId { get; set; }
        public bool IsComplete { get; set; }
        public int RequestStatusId { get; set; }
        public int? Importance { get; set; }

        [ForeignKey("DeclarantId")]
        [InverseProperty("Requests")]
        public virtual Person Declarant { get; set; } = null!;
        [ForeignKey("PlaceId")]
        [InverseProperty("Requests")]
        public virtual Place Place { get; set; } = null!;
        [ForeignKey("RequestStatusId")]
        [InverseProperty("Requests")]
        public virtual RequestStatus RequestStatus { get; set; } = null!;
        [InverseProperty("Request")]
        public virtual ICollection<WorkOnRequest> WorkOnRequests { get; set; }
    }
}
