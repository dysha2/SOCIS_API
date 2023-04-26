using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            People = new HashSet<Person>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(75)]
        public string Name { get; set; } = null!;

        [InverseProperty("Department")]
        public virtual ICollection<Person> People { get; set; }
    }
}
