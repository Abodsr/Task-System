using System.ComponentModel.DataAnnotations;

namespace MiniProject1.DTO
{
    public class UserDto
    {
        [StringLength(20,MinimumLength =3)]
        public string Name { get; set; }
        [Range(18,60)]
        public int? Age { get; set; }


    }
}
