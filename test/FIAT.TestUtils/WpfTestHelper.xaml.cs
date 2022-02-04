using System;
using System.Windows;
using System.Windows.Controls;

namespace FIAT.TestUtils
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
