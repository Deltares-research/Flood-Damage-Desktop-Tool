using System.Windows;
using System.Windows.Controls;
using FIAT.Gui.ViewModels;

namespace FIAT.Gui.UserControls
{
    public class BasinTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EventTemplate { get; set; }

        public DataTemplate RiskTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return item is RiskBasedScenario ? RiskTemplate : EventTemplate;
        }
    }
}