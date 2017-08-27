using MaxscriptManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxscriptManager.Src
{
    public class MMSelectedItemMessage
    {
        public MMDataItem NewItem { get; protected set; }
        public MMSelectedItemMessage(MMDataItem item)
        {
            NewItem = item;
        }
    }
}
