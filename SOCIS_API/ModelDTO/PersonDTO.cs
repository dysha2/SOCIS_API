using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class PersonDTO:Person
    {
        public PersonDTO(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Surname = person.Surname;
            Lastname = person.Lastname;
            PostId = person.PostId;
            DepartmentId = person.DepartmentId;
            Email = person.Email;
            Comment = person.Comment;
            UserName = person.UserName;
            RoleId = person.RoleId;
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
        public int RoleId { get; set; }
    }
}
