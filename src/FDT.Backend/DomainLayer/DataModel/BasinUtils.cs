using System;
using System.IO;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer;

namespace FDT.Backend.DomainLayer.DataModel
{
    public static class BasinUtils
    {
        public static IBasin GetBasin(string selectedBasinPath)
        {
            if (selectedBasinPath == null)
                throw new ArgumentNullException(nameof(selectedBasinPath));
            return new BasinData
            {
                Projection = GetWkidValue(selectedBasinPath),
                BasinName = Path.GetFileName(selectedBasinPath),
            };
        }

        public static string GetWkidValue(string selectedBasinPath)
        {
            if (selectedBasinPath == null)
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