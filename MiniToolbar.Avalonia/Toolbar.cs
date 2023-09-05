using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Metadata;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Layout;

namespace MiniToolbar.Avalonia;

[TemplatePart("PART_ToolbarPanel", typeof(StackPanel))]
public class Toolbar : TemplatedControl
{
    #region DisplayModeEnum

    public enum DisplayModeEnum
    {
        Compact,
        Normal
    }
    
    #endregion

    private StackPanel? toolbarPanel;

    public static readonly StyledProperty<Orientation> OrientationProperty = AvaloniaProperty.Register<Toolbar, Orientation>(nameof(Orientation), Orientation.Horizontal);

    public static readonly StyledProperty<DisplayModeEnum> DisplayModeProperty = AvaloniaProperty.Register<Toolbar, DisplayModeEnum>(nameof(DisplayMode), DisplayModeEnum.Normal);

    public static readonly DirectProperty<Toolbar, IEnumerable<IToolbarItem>> ToolbarItemsSourceProperty =
        AvaloniaProperty.RegisterDirect<Toolbar, IEnumerable<IToolbarItem>>(nameof(ToolbarItemsSource), s => s.ToolbarItems, (s, items) =>
        {
            s.ToolbarItems.Clear();
            s.ToolbarItems.AddRange(items);
        });

    public Orientation Orientation
    {
        get { return GetValue(OrientationProperty); }
        set { SetValue(OrientationProperty, value); }
    }

    public DisplayModeEnum DisplayMode
    {
        get { return GetValue(DisplayModeProperty); }
        set { SetValue(DisplayModeProperty, value); }
    }

    [Content]
    public List<IToolbarItem> ToolbarItems { get; } = new List<IToolbarItem>();

    public IEnumerable<IToolbarItem> ToolbarItemsSource
    {
        get { return GetValue(ToolbarItemsSourceProperty); }
        set { SetValue(ToolbarItemsSourceProperty, value); }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        toolbarPanel = e.NameScope.Find<StackPanel>("PART_ToolbarPanel");
        if (toolbarPanel != null)
        {
            toolbarPanel.Children.Clear();
            toolbarPanel.Children.AddRange(ToolbarItems.OfType<Control>());

            Classes.Set("Horizontal", Orientation == Orientation.Horizontal);
            Classes.Set("Vertical", Orientation == Orientation.Vertical);
            Classes.Set("CompactStyle", DisplayMode == DisplayModeEnum.Compact);
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == OrientationProperty)
            Classes.Set("Vertical", Orientation == Orientation.Vertical);
        else if (change.Property == DisplayModeProperty)
            Classes.Set("CompactStyle", DisplayMode == DisplayModeEnum.Compact);
        else if (change.Property == ToolbarItemsSourceProperty && toolbarPanel != null)
        {
            toolbarPanel.Children.Clear();
            toolbarPanel.Children.AddRange(ToolbarItems.OfType<Control>());
        }
    }
}