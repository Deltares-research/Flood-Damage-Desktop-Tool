using System.Collections.Generic;
using FDT.Backend.IDataModel;

namespace FDT.Backend.OutputLayer.FileObjectModel
{
    public class SettingsTabXlsx : ITabXlsx
    {
        private static readonly string _tabName = "Settings";
        public SettingsRowEntry SettingsRowSingleEntry { get; }

        public SettingsTabXlsx(IBasin basin, string scenarioName)
        {
            SettingsRowSingleEntry = new SettingsRowEntry(basin, scenarioName);
            RowEntries = new[] { SettingsRowSingleEntry };
        }

        public string TabName => _tabName;
        public IEnumerable<IRowEntry> RowEntries { get; }
    }
}