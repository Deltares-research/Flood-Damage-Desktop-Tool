using System;
using System.Collections.Generic;
using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Gui.ViewModels
{
    public class SelectBasinHelper
    {
        private IList<IBasin> SelectedBasins { get; }

        public SelectBasinHelper()
        {
            SelectedBasins = new List<IBasin>();
        }

        public string GetSelectedBasinWarning(IBasin selectedBasin)
        {
            if (selectedBasin is null)
            {
                throw new ArgumentNullException(nameof(selectedBasin));
            }
            if (SelectedBasins.Contains(selectedBasin))
                return string.Empty;
            SelectedBasins.Add(selectedBasin);
            if (string.IsNullOrEmpty(selectedBasin.Projection))
                return $"The area of interest {selectedBasin.BasinName} does not have an associated projection file.";
            return $"Only use flood maps with coordinate system {GetCrsCode(selectedBasin.Projection)} for this area of interest.";
        }
        private string GetCrsCode(string selectedBasinProjection)
        {
            if (string.IsNullOrEmpty(selectedBasinProjection))
                throw new ArgumentNullException(nameof(IBasin.Projection));

            string normalizedProj = selectedBasinProjection.ToLowerInvariant();
            if (normalizedProj.StartsWith("projcs") || normalizedProj.StartsWith("geogcs"))
                return ExtractProjectionString(selectedBasinProjection);

            if (normalizedProj.StartsWith("epsg") || normalizedProj.StartsWith("esri"))
                return selectedBasinProjection;

            // CS2022: This should show a warning / logging that it's not recognized.
            // Default value;
            return selectedBasinProjection;
        }

        private static string ExtractProjectionString(string wkidString)
        {
            return wkidString.Split("\"")[1].Replace("_", " ");
        }
    }
}