using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Metadata;

namespace MiniToolbar.Avalonia;

public class ToolbarButton : Button, IToolbarItem
{
    public static readonly StyledProperty<object?> IconProperty = AvaloniaProperty.Register<ToolbarButton, object?>(nameof(Icon));

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
    
    public object? Icon
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
            Control iconControl = Icon switch
            {
                TemplatedControl control => control,
                IImage iimage => new Image { Source = iimage, IsVisible = Icon != null },
                null => new Image { Source = null, IsVisible = false }, // Placeholder
                _ => throw new NotSupportedException($"Unsupported icon type ({Icon.GetType()}).")
            };

            base.Content = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Children =
                {
                    iconControl,
                    new TextBlock { Text = Text, IsVisible = !String.IsNullOrEmpty(Text) }
                }
            };

            Classes.Set("NoIcon", Icon == null);
        }
    }
}
