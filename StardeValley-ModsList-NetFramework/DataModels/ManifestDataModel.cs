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
        public List<string> UpdateKeys { get; set; } = new List<string>();

    }

    public class ModsDataModel
    {
        public bool Exist { get; set; } = false;
        public string Name { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string SMAPILink { get; set; }
        public string NexusLink { get; set; }
        public string GithubLink { get; set; }
        public string UniqueID { get; set; }
        public string EntryDll { get; set; }
        public string MinimumApiVersion { get; set; }

        public ModsDataModel() { }

        public ModsDataModel(string name, string author, string version, string description, string uniqueID, string entryDll, string minimumApiVersion, string smapiLink, string nexusLink, string githubLink)
        {
            this.Name = name;
            this.Author = author;
            this.Version = version;
            this.Description = description;
            this.UniqueID = uniqueID;
            this.EntryDll = entryDll;
            this.MinimumApiVersion = minimumApiVersion;
            this.SMAPILink = smapiLink;
            this.NexusLink = nexusLink;
            this.GithubLink = githubLink;
        }

        public static explicit operator ModsDataModel(ManifestDataModel manifestData)
        {
            string name = manifestData.Name;
            string author = manifestData.Author;
            string version = manifestData.Version;
            string description = manifestData.Description;
            string uniqueID = manifestData.UniqueID;
            string entryDll = manifestData.EntryDll;
            string minimumAPI = manifestData.MinimumApiVersion;

            //Set SMAPI URI
            var smapiUriPath = name.Replace(' ', '_').Replace("\'",".27s").Replace("&", ".26");
            string smapiLink = $"https://smapi.io/mods#{smapiUriPath}";

            string nexusLink = string.Empty;
            string githubLink = string.Empty;
            if (manifestData.UpdateKeys != null && manifestData.UpdateKeys.Count > 0)
            {
                foreach (var item in manifestData.UpdateKeys)
                {
                    if (item.Contains("Nexus") || item.Contains("nexus"))
                    {
                        var nexusModsId = item.Split(':').Last();
                        if (!string.IsNullOrEmpty(nexusModsId) && (!nexusModsId.Equals("?") || !nexusModsId.Equals("??") || !nexusModsId.Equals("???")))
                        {
                            nexusLink = $"https://www.nexusmods.com/stardewvalley/mods/{nexusModsId}";
                        }
                    }
                    if (item.Contains("github") || item.Contains("Github") || item.Contains("GitHub"))
                    {
                        var githubID = item.Split(':').Last();
                        githubLink = $"https://github.com/{githubID}";
                    }
                }
            }


            return new ModsDataModel(name, author, version, description, uniqueID, entryDll, minimumAPI, smapiLink, nexusLink, githubLink);
        }
    }
}
