using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class PostDTO:Post
    {
        public PostDTO(Post post)
        {
            this.Id=post.Id;
            this.Name=post.Name;
        }
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
    }
}
