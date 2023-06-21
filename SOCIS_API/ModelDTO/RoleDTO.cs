using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class RoleDTO:Role
    {
        public RoleDTO(Role role)
        {
            Id = role.Id;
            Name = role.Name;
            Comment = role.Comment;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = null!;
        [StringLength(100)]
        public string? Comment { get; set; }
    }
}
