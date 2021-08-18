using System;
using FDT.Backend.IDataModel;
using FDT.Backend.InputOutpulLayer.IFileObjectModel;

namespace FDT.Backend.InputOutpulLayer.FileObjectModel
{
    public class SettingsRowEntry : IRowEntry
    {
        private readonly IBasin _basin;
        public string SiteName => _basin.BasinName;
        public string ScenarioName { get; }

        public string OutputCrs => _basin.Projection;
        public string VerticalUnit => "feet";

        public SettingsRowEntry(IBasin basin, string scenarioName)
        {
            _basin = basin ?? throw new ArgumentNullException(nameof(basin));
            if (string.IsNullOrEmpty(scenarioName))
                throw new ArgumentNullException(nameof(scenarioName));
            ScenarioName = scenarioName;
        }
    }
}