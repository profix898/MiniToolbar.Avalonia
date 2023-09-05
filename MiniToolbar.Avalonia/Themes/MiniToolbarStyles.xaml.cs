using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace MiniToolbar.Avalonia.Themes
{
    public class MiniToolbarStyles : Styles
    {
        public MiniToolbarStyles(IServiceProvider? sp = null)
        {
            AvaloniaXamlLoader.Load(sp, this);
        }
    }
}
