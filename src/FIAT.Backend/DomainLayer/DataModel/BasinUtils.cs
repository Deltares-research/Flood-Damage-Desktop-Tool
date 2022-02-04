using System;
using System.IO;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Backend.PersistenceLayer;

namespace FIAT.Backend.DomainLayer.DataModel
{
    public static class BasinUtils
    {
        public static IBasin GetBasin(string selectedBasinPath)
        {
            if (string.IsNullOrEmpty(selectedBasinPath))
                throw new ArgumentNullException(nameof(selectedBasinPath));
            return new BasinData
            {
                Projection = GetWkidValue(selectedBasinPath),
                BasinName = Path.GetFileName(selectedBasinPath),
            };
        }

        public static string GetWkidValue(string selectedBasinPath)
        {
            if (string.IsNullOrEmpty(selectedBasinPath))
                throw new ArgumentNullException(nameof(selectedBasinPath));
            try
            {
                return new WkidDataReader {BasinDir = selectedBasinPath}.GetProjectionValue();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}