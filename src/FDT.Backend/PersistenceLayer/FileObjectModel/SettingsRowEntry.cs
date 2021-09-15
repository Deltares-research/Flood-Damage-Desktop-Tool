using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;

namespace FDT.Backend.PersistenceLayer.FileObjectModel
{
    public class SettingsRowEntry : IRowEntry
    {
        private readonly IBasin _basin;
        private string SiteName => _basin.BasinName;
        private string ScenarioName { get; }

        private string OutputCrs => _basin.Projection;
        private string VerticalUnit => "feet";

        public SettingsRowEntry(IBasin basin, string scenarioName)
        {
            _basin = basin ?? throw new ArgumentNullException(nameof(basin));
            if (string.IsNullOrEmpty(scenarioName))
                throw new ArgumentNullException(nameof(scenarioName));
            ScenarioName = scenarioName;
        }

        public IEnumerable<object> GetOrderedColumns(IXLRow defaultRow)
        {
            return new[]
            {
                SiteName,
                ScenarioName,
                OutputCrs,
                VerticalUnit
            };
        }
    }
}