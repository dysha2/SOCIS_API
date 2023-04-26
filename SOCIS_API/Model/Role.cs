using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            People = new HashSet<Person>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Role")]
        [StringLength(30)]
        public string Role1 { get; set; } = null!;
        [StringLength(100)]
        public string? Comment { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<Person> People { get; set; }
    }
}
