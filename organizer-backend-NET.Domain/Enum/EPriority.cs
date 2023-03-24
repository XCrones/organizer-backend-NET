
using System.ComponentModel;
using System.Runtime.Serialization;

namespace organizer_backend_NET.Domain.Enum
{
    public enum EPriority
    {
        [EnumMember(Value = "hight")]
        [Description("hight")]
        hight,

        [EnumMember(Value = "medium")]
        [Description("medium")]
        medium,

        [EnumMember(Value = "low")]
        [Description("low")]
        low,
    }
}
