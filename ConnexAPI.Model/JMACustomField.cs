using System;

namespace ConnexForQuickBooks.Model
{
    [Serializable]
    public class JMACustomField
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string OrderNumber { get; set; }
        public bool Mapped { get; set; }
    }
}
