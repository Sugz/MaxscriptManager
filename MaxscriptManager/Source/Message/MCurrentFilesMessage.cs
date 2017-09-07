using MaxscriptManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxscriptManager.Source.Message
{
    public class MCurrentFilesMessage
    {
        public ObservableCollection<MCodeItem> CurrentFiles { get; protected set; }
        public MCurrentFilesMessage(ObservableCollection<MCodeItem> currentFiles)
        {
            CurrentFiles = currentFiles;
        }
    }
}
