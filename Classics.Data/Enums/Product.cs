
using System.ComponentModel;

namespace Enums
{
    public class Product
    {
        public enum ProductStatus
        {
            [Description("Ativo")]
            Enable = 1,
            [Description("Pendente Aprovação")]
            PendingApproval = 2,
            [Description("Inativo")]
            Disable = 3,
        }
    }
}
