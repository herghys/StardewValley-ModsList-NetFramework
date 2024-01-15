using Newtonsoft.Json;
using StardewValleyModList.DataModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StardewValleyModList.Helper
{
    public static class ModGetter
    {
        public static async Task<List<ManifestDataModel>> SearchModsAsync(string path)
        {
            var manifestJsonFile = "manifest.json";
            var modsListPath = Directory.GetDirectories(path);
            if (modsListPath.Length == 0)
                return default;

            var manifestList = await GetModsManifestAsync(modsListPath, manifestJsonFile);

            if (manifestList.Count == 0) return default;

            return manifestList;
        }

        public static  List<ManifestDataModel> SearchMods(string path)
        {
            var manifestJsonFile = "manifest.json";
            var modsListPath = Directory.GetDirectories(path);
            if (modsListPath.Length == 0)
                return default;

            var manifestList = GetModsManifest(modsListPath, manifestJsonFile);

            if (manifestList.Count == 0) return default;

            return manifestList;
        }

        private static async Task<List<ManifestDataModel>> GetModsManifestAsync(string[] modsListPaths, string manifestFilename)
        {
            List<ManifestDataModel> manifestData = new List<ManifestDataModel>();

            foreach (var path in modsListPaths)
            {
                var insideDir = Directory.GetDirectories(path);
                string baseFile = Path.Combine(path, manifestFilename);

                //bool containsDependencies = insideDir.Any(x => x.Equals("[FTM]") || x.Equals("[CP]") || x.Equals("[JA]") || x.Equals("[SAAT]") || x.Equals("[STF]"));

                if (File.Exists(baseFile))
                {
                    string file = Path.Combine(path, manifestFilename);
                    if (File.Exists(file))
                    {
                        var json = File.ReadAllText(file);
                        var data = JsonConvert.DeserializeObject<ManifestDataModel>(json);
                        manifestData.Add(data);
                        await Task.Yield();
                    }
                    else
                    {
                        var lastFolder = path.Split(Path.DirectorySeparatorChar).Last();
                        ManifestDataModel data = new ManifestDataModel()
                        {
                            Name = lastFolder
                        };
                        manifestData.Add(data);
                    }
                }
                else if (insideDir.Length != 0 && !File.Exists(baseFile))
                {
                    var lastFolder = path.Split(Path.DirectorySeparatorChar).Last();
                    var data = new ManifestDataModel()
                    {
                        Name = lastFolder
                    };

                    foreach (var insidePath in insideDir)
                    {
                        string file = Path.Combine(insidePath, manifestFilename);
                        if (File.Exists(file))
                        {
                            var json = File.ReadAllText(file);
                            var d = JsonConvert.DeserializeObject<ManifestDataModel>(json);
                            if (d.UpdateKeys != null && d.UpdateKeys.Count > 0)
                            data.UpdateKeys.AddRange(d.UpdateKeys);
                        }
                    }
                    data.UpdateKeys.Distinct();
                    data.UpdateKeys.RemoveAll(x => x == string.Empty || x.Equals("Nexus:???") || x.Equals("Nexus:"));
                    
                    manifestData.Add(data);
                }
            }

            return manifestData;
        }

        private static List<ManifestDataModel> GetModsManifest(string[] modsListPaths, string manifestFilename)
        {
            List<ManifestDataModel> manifestData = new List<ManifestDataModel>();

            foreach (var path in modsListPaths)
            {
                var insideDir = Directory.GetDirectories(path);
                string baseFile = Path.Combine(path, manifestFilename);

                //bool containsDependencies = insideDir.Any(x => x.Equals("[FTM]") || x.Equals("[CP]") || x.Equals("[JA]") || x.Equals("[SAAT]") || x.Equals("[STF]"));

                if (File.Exists(baseFile))
                {
                    string file = Path.Combine(path, manifestFilename);
                    if (File.Exists(file))
                    {
                        var json = File.ReadAllText(file);
                        var data = JsonConvert.DeserializeObject<ManifestDataModel>(json);
                        manifestData.Add(data);
                    }
                    else
                    {
                        var lastFolder = path.Split(Path.DirectorySeparatorChar).Last();
                        var data = new ManifestDataModel()
                        {
                            Name = lastFolder
                        };
                        manifestData.Add(data);
                    }
                }
                else if (insideDir.Length != 0 && !File.Exists(baseFile))
                {
                    var lastFolder = path.Split(Path.DirectorySeparatorChar).Last();
                    var data = new ManifestDataModel()
                    {
                        Name = lastFolder
                    };

                    foreach (var insidePath in insideDir)
                    {
                        string file = Path.Combine(insidePath, manifestFilename);
                        if (File.Exists(file))
                        {
                            var json = File.ReadAllText(file);
                            var d = JsonConvert.DeserializeObject<ManifestDataModel>(json);
                            if (d.UpdateKeys != null && d.UpdateKeys.Count > 0)
                                data.UpdateKeys.AddRange(d.UpdateKeys);
                        }
                    }
                    data.UpdateKeys.Distinct();
                    data.UpdateKeys.RemoveAll(x => x == string.Empty || x.Equals("Nexus:???") || x.Equals("Nexus:"));

                    manifestData.Add(data);
                }
            }

            return manifestData;
        }
    }
}
