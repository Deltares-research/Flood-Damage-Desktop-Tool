using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FDT.Backend.DomainLayer.DataModel;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer;

namespace FDT.Gui.ViewModels
{
    public static class BackendConverter
    {
        public static IBasin ConvertBasin(this IEnumerable<IBasinScenario> basinScenarios, string selectedBasinPath)
        {
            if (basinScenarios == null)
                throw new ArgumentNullException(nameof(basinScenarios));

            return new BasinData
            {
                Projection = new WkidDataReader{BasinDir = selectedBasinPath}.GetProjectionValue(),
                BasinName = Path.GetFileName(selectedBasinPath),
                Scenarios = basinScenarios
                    .Where( bs => bs.IsEnabled )
                    .SelectMany( bs => bs.Scenarios.ConvertScenarios())
            };
        }

        public static IEnumerable<Backend.DomainLayer.IDataModel.IScenario> ConvertScenarios(this IEnumerable<IScenario> scenarios)
        {
            if (scenarios == null)
                throw new ArgumentNullException(nameof(scenarios));
            foreach (IScenario scenario in scenarios)
            {
                yield return new ScenarioData()
                {
                    ScenarioName = scenario.ScenarioName,
                    FloodMaps = scenario.FloodMaps.ConvertFloodMaps()
                };
            }
        }

        public static IEnumerable<IFloodMapBase> ConvertFloodMaps(
            this IEnumerable<IFloodMap> floodMaps)
        {
            if (floodMaps == null)
                throw new ArgumentNullException(nameof(floodMaps));
            foreach (IFloodMap floodMap in floodMaps)
            {
                if (floodMap.HasReturnPeriod)
                {
                    yield return new Backend.DomainLayer.DataModel.FloodMapWithReturnPeriod()
                    {
                        Path = floodMap.MapPath,
                        ReturnPeriod = floodMap.ReturnPeriod
                    };
                }
                else
                {
                    yield return new Backend.DomainLayer.DataModel.FloodMap()
                    {
                        Path = floodMap.MapPath
                    };
                }
            }
        }
    }
}