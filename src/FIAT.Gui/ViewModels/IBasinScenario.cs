using System.Collections.ObjectModel;

namespace FIAT.Gui.ViewModels
{
    public interface IBasinScenario
    {
        string ScenarioType { get; set; }
        bool IsEnabled { get; set; }
        ObservableCollection<IScenario> Scenarios { get; }
        void AddExtraScenario();
    }
}