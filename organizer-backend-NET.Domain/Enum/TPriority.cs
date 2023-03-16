
using System.ComponentModel.DataAnnotations;

namespace organizer_backend_NET.Domain.Enum
{
    public enum TPriority
    {
        [Display(Name = "Hight")]
        HIGHT = 0,

        [Display(Name = "Medium")]
        MEDIUM = 1,

        [Display(Name = "Low")]
        LOW = 2,
    }
}
