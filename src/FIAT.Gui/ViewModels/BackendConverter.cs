using System;
using System.Collections.Generic;
using System.Linq;
using FIAT.Backend.DomainLayer.DataModel;
using FIAT.Backend.DomainLayer.IDataModel;

namespace FIAT.Gui.ViewModels
{
    public static class BackendConverter
    {
        public static IEnumerable<FIAT.Backend.DomainLayer.IDataModel.IScenario> ConvertBasinScenarios(this IEnumerable<IBasinScenario> basinScenarios)
        {
            if (basinScenarios == null)
                throw new ArgumentNullException(nameof(basinScenarios));

            return basinScenarios
                .Where(bs => bs.IsEnabled)
                .SelectMany(bs => bs.Scenarios.ConvertScenarios());
        }

        public static IEnumerable<FIAT.Backend.DomainLayer.IDataModel.IScenario> ConvertScenarios(this IEnumerable<IScenario> scenarios)
        {
            if (scenarios == null)
                throw new ArgumentNullException(nameof(scenarios));
            foreach (IScenario scenario in scenarios)
            {
                yield return new ScenarioData()
                {
                    ScenarioName = scenario.ScenarioName,
                    FloodMaps = scenario.FloodMaps.ConvertFloodMaps(scenario.ScenarioFloodMapType)
                };
            }
        }

        public static IEnumerable<IFloodMapBase> ConvertFloodMaps(
            this IEnumerable<IFloodMap> floodMaps, FloodMapType mapType)
        {
            if (floodMaps == null)
                throw new ArgumentNullException(nameof(floodMaps));
            foreach (IFloodMap floodMap in floodMaps)
            {
                if (floodMap.HasReturnPeriod)
                {
                    yield return new FIAT.Backend.DomainLayer.DataModel.FloodMapWithReturnPeriod()
                    {
                        Path = floodMap.MapPath,
                        ReturnPeriod = floodMap.ReturnPeriod,
                        MapType = mapType
                    };
                }
                else
                {
                    yield return new FIAT.Backend.DomainLayer.DataModel.FloodMap()
                    {
                        Path = floodMap.MapPath,
                        MapType = mapType
                    };
                }
            }
        }
    }
}