using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Metadata;

namespace MiniToolbar.Avalonia;

public class ToolbarButton : Button, IToolbarItem
{
    public static readonly StyledProperty<IImage?> IconProperty = AvaloniaProperty.Register<ToolbarButton, IImage?>(nameof(Icon));

    public static readonly StyledProperty<string?> TextProperty = AvaloniaProperty.Register<ToolbarButton, string?>(nameof(Text));

    public ToolbarButton()
    {
        Classes.Add("ToolbarButton");
    }

    [Content]
    [DependsOn(nameof(ContentTemplate))]
    public object? Content
    {
        get { return GetValue(ContentProperty); }
        set { throw new NotSupportedException($"{nameof(ToolbarButton)} does not support custom {nameof(Content)} and {nameof(ContentTemplate)}. Use .{nameof(Text)} and .{nameof(Icon)} properties."); }
    }

    public IDataTemplate? ContentTemplate
    {
        get { return null; }
        set { throw new NotSupportedException($"{nameof(ToolbarButton)} does not support custom {nameof(Content)} and {nameof(ContentTemplate)}. Use .{nameof(Text)} and .{nameof(Icon)} properties."); }
    } 
    
    public IImage? Icon
    {
        get { return GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    public string? Text
    {
        get { return GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    #region Overrides of StyledElement

    protected override Type StyleKeyOverride => typeof(Button);

    #endregion

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == IconProperty || change.Property == TextProperty)
        {
            base.Content = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Children =
                {
                    new Image { Source = Icon, IsVisible = (Icon != null) },
                    new TextBlock { Text = Text, IsVisible = (!String.IsNullOrEmpty(Text)) }
                }
            };

            Classes.Set("NoIcon", Icon == null);
        }
    }
}
