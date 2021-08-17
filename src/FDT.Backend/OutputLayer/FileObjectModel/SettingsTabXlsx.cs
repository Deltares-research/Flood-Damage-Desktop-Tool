using System;
using System.Collections.Generic;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer.IFileObjectModel;

namespace FDT.Backend.OutputLayer.FileObjectModel
{
    public class SettingsTabXlsx : ITabXlsx
    {
        private static readonly string _tabName = "Settings";
        public SettingsRowEntry SettingsRowSingleEntry { get; }

        public string TabName => _tabName;
        public IEnumerable<IRowEntry> RowEntries { get; }
        public SettingsTabXlsx(IBasin basin, string scenarioName)
        {
            if (basin == null)
                throw new ArgumentNullException(nameof(basin));
            if (string.IsNullOrEmpty(scenarioName))
                throw new ArgumentNullException(nameof(scenarioName));
            SettingsRowSingleEntry = new SettingsRowEntry(basin, scenarioName);
            RowEntries = new[] { SettingsRowSingleEntry };
        }
    }
}