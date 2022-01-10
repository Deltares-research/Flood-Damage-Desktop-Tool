using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using Path = System.IO.Path;

namespace FDT.Backend.PersistenceLayer.FileObjectModel
{
    public class ExposureRowEntry : IRowEntry
    {
        private const string ExposureFileName = "exposure.csv";
        private readonly string _exposureRelativePath;
        private readonly string _exposureProjection;

        public ExposureRowEntry(IBasin selectedBasin, string exposurePath)
        {
            if (selectedBasin == null)
                throw new ArgumentNullException(nameof(selectedBasin));
            if(string.IsNullOrEmpty(selectedBasin.BasinName))
                throw new ArgumentNullException(nameof(IBasin.BasinName));
            if (string.IsNullOrEmpty(selectedBasin.Projection))
                throw new ArgumentNullException(nameof(IBasin.Projection));
            if (string.IsNullOrEmpty(exposurePath))
                throw new ArgumentNullException(nameof(exposurePath));
            _exposureRelativePath = Path.Combine(exposurePath, selectedBasin.BasinName, ExposureFileName);
            _exposureProjection = selectedBasin.Projection;
        }

        public IEnumerable<object> GetOrderedColumns(IXLRow defaultRow)
        {
            return new []{ _exposureRelativePath, _exposureProjection};
        }
    }
}