using System.ComponentModel.DataAnnotations;

namespace MiniProject1.DTO
{
    public class UserTask
    {
        public int UserID {  get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(300)]

        public string ?Description { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
