using System.Collections.ObjectModel;

namespace FDT.Gui.ViewModels
{
    public interface IBasinScenario
    {
        bool IsEnabled { get; set; }
        IScenario Scenario { get; set; }
    }
}