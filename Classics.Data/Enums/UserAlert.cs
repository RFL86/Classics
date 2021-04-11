using System.ComponentModel;

namespace Enums
{
    public class UserAlert
    {
        public enum ReadingStatus
        {
            [Description("Lido")]
            Read = 1,
            [Description("Não Lido")]
            NotRead = 0
        }
    }
}
