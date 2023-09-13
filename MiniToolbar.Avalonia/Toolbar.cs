using System;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Metadata;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

    public static readonly StyledProperty<IEnumerable<IToolbarItem>?> ItemsSourceProperty = AvaloniaProperty.Register<Toolbar, IEnumerable<IToolbarItem>?>(nameof(ItemsSource));

    public Toolbar()
    {
        Items.CollectionChanged += OnItemsCollectionChanged;
    }

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
    public ObservableCollection<IToolbarItem> Items { get; } = new ObservableCollection<IToolbarItem>();

    public IEnumerable<IToolbarItem>? ItemsSource
    {
        internal get { return GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        toolbarPanel = e.NameScope.Find<StackPanel>("PART_ToolbarPanel");
        if (toolbarPanel != null)
        {
            toolbarPanel.Children.Clear();
            toolbarPanel.Children.AddRange(Items.OfType<Control>());

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
        else if (change.Property == ItemsSourceProperty)
        {
            if (ItemsSource is INotifyCollectionChanged notifyCollectionChanged)
                notifyCollectionChanged.CollectionChanged += (_, _) => { UpdateItemsFromItemsSource(); };

            UpdateItemsFromItemsSource();
        }
    }

    #region Private

    private void UpdateItemsFromItemsSource()
    {
        Items.Clear();

        if (ItemsSource != null)
        {
            foreach (var item in ItemsSource)
                Items.Add(item);
        }
    }

    private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (toolbarPanel == null)
            return;

        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewStartingIndex != -1)
                    toolbarPanel.Children.InsertRange(e.NewStartingIndex, e.NewItems.OfType<Control>());
                else
                    toolbarPanel.Children.AddRange(e.NewItems.OfType<Control>());
                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.NewStartingIndex != -1)
                    toolbarPanel.Children.RemoveRange(e.NewStartingIndex, e.OldItems.OfType<Control>().Count());
                else
                    toolbarPanel.Children.RemoveAll(e.OldItems.OfType<Control>());
                break;
            case NotifyCollectionChangedAction.Replace:
                if (e.NewStartingIndex != -1)
                {
                    toolbarPanel.Children.RemoveRange(e.NewStartingIndex, e.OldItems.OfType<Control>().Count());
                    toolbarPanel.Children.InsertRange(e.OldStartingIndex, e.NewItems.OfType<Control>());
                }
                else
                {
                    toolbarPanel.Children.RemoveAll(e.OldItems.OfType<Control>());
                    toolbarPanel.Children.AddRange(e.NewItems.OfType<Control>());
                }
                break;
            case NotifyCollectionChangedAction.Move:
                OnItemsCollectionChanged(sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, e.OldStartingIndex, e.OldItems));
                OnItemsCollectionChanged(sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, e.NewStartingIndex, e.NewItems));
                break;
            case NotifyCollectionChangedAction.Reset:
                toolbarPanel.Children.Clear();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion
}