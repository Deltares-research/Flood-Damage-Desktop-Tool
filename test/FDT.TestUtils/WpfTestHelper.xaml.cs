using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FDT.TestUtils
{
    /// <summary>
    /// Interaction logic for WpfTestHelper.xaml
    /// </summary>
    public partial class WpfTestHelper : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WpfTestHelper"/> class.
        /// </summary>
        /// <param name="testControl">The test control.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="testAction">The test action.</param>
        public WpfTestHelper(UserControl testControl, string actionName, Action testAction)
        {
            InitializeComponent();

            TestAction = testAction;
            TestActionButton.Content = actionName;
            Wrapper.Content = testControl;
        }

        private Action TestAction { get; }

        private void TestActionButton_Click(object sender, RoutedEventArgs e)
        {
            TestAction?.Invoke();
        }
    }
}
