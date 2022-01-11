using System;
using System.Collections.Generic;
using System.Linq;
using FDT.Backend.DomainLayer.DataModel;
using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Gui.ViewModels
{
    public static class BackendConverter
    {
        public static IFloodDamageBasin ConvertBasin(this IBasin basinData, IEnumerable<IBasinScenario> basinScenarios)
        {
            if (basinScenarios == null)
                throw new ArgumentNullException(nameof(basinScenarios));

            return new FloodDamageBasinData
            {
                Projection = basinData.Projection,
                BasinName = basinData.BasinName,
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