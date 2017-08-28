using MaxscriptManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxscriptManager.Src
{
    public class MSelectedItemMessage
    {
        public MDataItem NewItem { get; protected set; }
        public MSelectedItemMessage(MDataItem item)
        {
            NewItem = item;
        }
    }
}
