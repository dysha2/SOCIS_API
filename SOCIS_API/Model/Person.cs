using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("Person")]
    [Index("Name", "Surname", "Lastname", Name = "UQ__Responsi__27D4E7DA61339BC2", IsUnique = true)]
    public partial class Person
    {
        public Person()
        {
            Requests = new HashSet<Request>();
            UnitRespPeople = new HashSet<UnitRespPerson>();
            WorkOnRequests = new HashSet<WorkOnRequest>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = null!;
        [StringLength(30)]
        public string Surname { get; set; } = null!;
        [StringLength(30)]
        public string? Lastname { get; set; }
        public int? PostId { get; set; }
        public int? DepartmentId { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(300)]
        public string? Comment { get; set; }
        [StringLength(30)]
        public string? UserName { get; set; }
        public int? RoleId { get; set; }
        [StringLength(2048)]
        public string? Password { get; set; }
        [StringLength(1024)]
        public string? PasswordSalt { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("People")]
        public virtual Department? Department { get; set; }
        [ForeignKey("PostId")]
        [InverseProperty("People")]
        public virtual Post? Post { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("People")]
        public virtual Role? Role { get; set; }
        [InverseProperty("Declarant")]
        public virtual ICollection<Request> Requests { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<UnitRespPerson> UnitRespPeople { get; set; }
        [InverseProperty("Implementer")]
        public virtual ICollection<WorkOnRequest> WorkOnRequests { get; set; }
    }
}
