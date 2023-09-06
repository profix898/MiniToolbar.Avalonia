using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DemoApp
{
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void ButtonAClick(object? sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button A clicked.");
        }
    }
}