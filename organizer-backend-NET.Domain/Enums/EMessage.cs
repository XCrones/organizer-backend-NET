using System.Runtime.Serialization;

namespace organizer_backend_NET.Domain.Enums
{
    public enum EMessage
    {
        [EnumMember(Value = "Not found")]
        not_found,

        [EnumMember(Value = "Create succes")]
        create_succes,

        [EnumMember(Value = "Restore succes")]
        restore_succes,

        [EnumMember(Value = "Delete succes")]
        delete_succes,

        [EnumMember(Value = "Update succes")]
        update_succes,

        [EnumMember(Value = "email is busy")]
        email_busy,

        [EnumMember(Value = "user not found")]
        user_not_found,
    }
}