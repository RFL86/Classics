using System.ComponentModel;

namespace Enums
{
    public class Profile
    {
        public enum ProfileType
        {
            [Description("Client")]
            Client = 0,
            [Description("Operator")]
            Operator = 1,
            [Description("Manager")]
            Manager = 2,
        }
    }
}
