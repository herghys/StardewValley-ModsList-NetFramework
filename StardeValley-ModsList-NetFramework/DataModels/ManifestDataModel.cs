using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewValleyModList.DataModels
{
    public class ManifestDataModel
    {
        public ManifestDataModel() { }

        public string Name { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string UniqueID { get; set; }
        public string EntryDll { get; set; }
        public string MinimumApiVersion { get; set; }
        public List<string> UpdateKeys { get; set; }

    }
}
