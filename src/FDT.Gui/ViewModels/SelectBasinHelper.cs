using System;
using System.Collections.Generic;
using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Gui.ViewModels
{
    public class SelectBasinHelper
    {
        private IList<IBasin> SelectedBasins { get; }

        public Action<string> ShowWarningMessage { private get; set; }

        public SelectBasinHelper()
        {
            SelectedBasins = new List<IBasin>();
        }

        public void ChangeBasin(IBasin selectedBasin)
        {
            if (selectedBasin is null)
            {
                throw new ArgumentNullException(nameof(selectedBasin));
            }
            if (SelectedBasins.Contains(selectedBasin))
                return;
            SelectedBasins.Add(selectedBasin);
            string warningMessage =
                $"Only use flood maps with coordinate system {GetCrsCode(selectedBasin.Projection)} for this area of interest.";
            ShowWarningMessage?.Invoke(warningMessage);
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

        private string ExtractProjectionString(string wkidString)
        {
            string crsProjection = wkidString.Split("\"")[1];
            return crsProjection.Replace("_", " ");
        }
    }
}