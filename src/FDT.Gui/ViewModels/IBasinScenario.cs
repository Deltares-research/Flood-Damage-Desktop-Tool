using System.Collections.ObjectModel;

namespace FDT.Gui.ViewModels
{
    public interface IBasinScenario
    {
        bool IsEnabled { get; set; }
        ObservableCollection<IScenario> Scenarios { get; set; }
        void AddExtraScenario();
    }
}