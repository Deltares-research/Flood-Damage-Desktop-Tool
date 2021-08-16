﻿namespace FDT.Backend.IDataModel
{
    public interface IFloodMapBase
    {
        string Path { get; set; }
    }

    public interface IFloodMap: IFloodMapBase
    {
        string ReturnPeriod { get; }
    }

    public interface IFloodMapBaseWithReturnPeriod : IFloodMapBase
    {
        int ReturnPeriod { get; set; }
    }
}