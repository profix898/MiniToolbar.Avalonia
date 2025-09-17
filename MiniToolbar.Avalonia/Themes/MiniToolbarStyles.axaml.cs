using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace MiniToolbar.Avalonia.Themes
{
    public partial class MiniToolbarStyles : Styles
    {
        public MiniToolbarStyles()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}