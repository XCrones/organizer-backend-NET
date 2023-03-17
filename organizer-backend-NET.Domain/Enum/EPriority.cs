
using System.ComponentModel;

namespace organizer_backend_NET.Domain.Enum
{
    public enum EPriority : byte
    {
        [Description("Hight")]
        HIGHT = 0,

        [Description("Medium")]
        MEDIUM = 1,

        [Description("Low")]
        LOW = 2,
    }
}
