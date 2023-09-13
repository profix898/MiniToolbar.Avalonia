using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MiniToolbar.Avalonia;

namespace DemoApp;

public partial class TestWindow : Window
{
    private readonly ObservableCollection<IToolbarItem> itemsList = new ObservableCollection<IToolbarItem>
    {
        new ToolbarButton { Text = "Button A" },
        new ToolbarButton { Text = "Button B" }
    };

    public TestWindow()
    {
        InitializeComponent();
    }

    private void ButtonAClick(object? sender, RoutedEventArgs e)
    {
        Debug.WriteLine("Button A clicked.");
    }

    private void AddToToolbar(object? sender, RoutedEventArgs e)
    {
        HorizontalCompactToolbar.Items.Add(new ToolbarButton { Text = "Button X" });
    }

    private void BindToolbarItemSource(object? sender, RoutedEventArgs e)
    {
        HorizontalCompactToolbar.ItemsSource = itemsList;
    }

    private void UpdateToolbarItemSource(object? sender, RoutedEventArgs e)
    {
        itemsList.Add(new ToolbarButton { Text = "Button M" });
    }
}
