using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class DepartmentDTO : Department
    {
        public DepartmentDTO(Department dep)
        {
            Id = dep.Id;
            Name = dep.Name;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(75)]
        public string Name { get; set; } = null!; 
    }
}
