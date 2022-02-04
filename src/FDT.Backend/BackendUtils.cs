using System;
using System.Collections.Generic;
using System.IO;
using FIAT.Backend.DomainLayer.DataModel;
using FIAT.Backend.DomainLayer.IDataModel;

namespace FIAT.Backend
{
    public static class BackendUtils
    {
        public static IFloodDamageBasin ConvertBasin(this IBasin basinData, IEnumerable<IScenario> scenarios)
        {
            if (basinData is null)
                throw new ArgumentNullException(nameof(basinData));
            if (scenarios is null)
                throw new ArgumentNullException(nameof(scenarios));

            return new FloodDamageBasinData()
            {
                BasinName = basinData.BasinName,
                Projection = basinData.Projection,
                Scenarios = scenarios
            };
        }

        public static IEnumerable<string> GetSubDirectoryNames(string[] foundSubDirectories)
        {
            if (foundSubDirectories == null || foundSubDirectories.Length == 0)
                yield break;

            foreach (string directory in foundSubDirectories)
            {
                string trimmedDirectory = directory.TrimEnd(Path.DirectorySeparatorChar);
                yield return trimmedDirectory.Remove(0, trimmedDirectory.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            }
        }

    }
}