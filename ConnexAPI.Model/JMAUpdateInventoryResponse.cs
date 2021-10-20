using ConnexForQuickBooks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnexAPI.Model
{
    public class JMAUpdateInventoryResponse
    {
        public JMAUpdateInventoryResponse()
        {
            ProductSkusErrors = new Dictionary<string, string>();
            ProductSyncSuccess = new List<JMAProduct>();
        }

        public Dictionary<string, string> ProductSkusErrors { get; set; }
        public List<JMAProduct> ProductSyncSuccess { get; set; }
    }
}
