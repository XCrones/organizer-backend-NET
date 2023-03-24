
using organizer_backend_NET.Domain.Enums;

namespace organizer_backend_NET.Domain.Helpers
{
    public static class ConvertEnum
    {
        public static EPriority PriorityIntToEnum(int value)
        {
            try
            {
                if (Enum.IsDefined(typeof(EPriority), value))
                {
                    return (EPriority)value;
                }

                return EPriority.low;
            }
            catch (Exception ex)
            {
                //! loggin error
                return EPriority.low;
            }
        }
    }
}
