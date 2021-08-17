using System.Collections.Generic;
using System.Linq;
using FDT.Backend.DataModel;
using FDT.Backend.IDataModel;

namespace FDT.Gui.ViewModels
{
    public static class BackendConverter
    {
        public static IBasin ConvertBasin(this IEnumerable<IBasinScenario> basinScenarios, string selectedBasin)
        {
            return new BasinData()
            {
                Projection = "EPSG:42",
                BasinName = selectedBasin,
                Scenarios = basinScenarios
                    .Where( bs => bs.IsEnabled )
                    .SelectMany( bs => bs.Scenarios.ConvertScenarios())
            };
        }

        public static IEnumerable<Backend.IDataModel.IScenario> ConvertScenarios(this IEnumerable<IScenario> scenarios)
        {
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
            foreach (IFloodMap floodMap in floodMaps)
            {
                if (floodMap.HasReturnPeriod)
                {
                    yield return new FloodMapBaseWithReturnPeriod()
                    {
                        Path = floodMap.MapPath,
                        ReturnPeriod = floodMap.ReturnPeriod
                    };
                }
                else
                {
                    yield return new Backend.DataModel.FloodMap()
                    {
                        Path = floodMap.MapPath
                    };
                }
            }
        }
    }
}