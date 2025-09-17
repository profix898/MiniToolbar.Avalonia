MiniToolbar.Avalonia
==========
[![Nuget](https://img.shields.io/nuget/v/MiniToolbar.Avalonia?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/MiniToolbar.Avalonia)

**MiniToolbar.Avalonia** provides a minimalistic toolbar implementation for **[Avalonia](https://avaloniaui.net/)**.

[<img src="screenshot.png" width="800" />](screenshot.png)

Documentation
------------
There is currently only very limited documentation (incl. API docs) available. Please refer to the *DemoApp* for preliminary instructions and usage examples. This README adds a short API overview and basic usage examples to get started quickly.

Installation
------------
Install the package from NuGet:

    dotnet add package MiniToolbar.Avalonia

Basic concepts / API
--------------------
- Toolbar (MiniToolbar.Avalonia.Toolbar)
  - A templated control that hosts a sequence of toolbar items.
  - Properties:
    - Orientation (Horizontal / Vertical)
    - DisplayMode (Normal / Compact)
    - Items (ObservableCollection<IToolbarItem>) — content collection for direct child items
    - ItemsSource (IEnumerable<IToolbarItem>) — bindable source (the control mirrors the collection into Items)
  - Template part: PART_ToolbarPanel (StackPanel) where item controls are inserted.

- ToolbarButton (MiniToolbar.Avalonia.ToolbarButton)
  - A lightweight button intended for use inside a Toolbar.
  - Properties:
    - Icon (object) — can be a path geometry, control, or image; templates decide how to render it
    - Text (string) — label text shown next to or below the icon depending on template/styles
  - Notes: ToolbarButton does not support customizing the Content or ContentTemplate. Use Text and Icon instead.

Usage examples
--------------
XAML example (in an Avalonia window or user control):

    xmlns:mt="clr-namespace:MiniToolbar.Avalonia;assembly=MiniToolbar.Avalonia"

    <mt:Toolbar Orientation="Horizontal" DisplayMode="Normal">
      <mt:ToolbarButton Text="New" Click="New_Click" Icon="/Assets/new.png" />
      <mt:ToolbarButton Text="Open" Click="Open_Click" Icon="/Assets/open.png" />
      <mt:ToolbarButton Text="Save" Click="Save_Click" Icon="/Assets/save.png" />
    </mt:Toolbar>

You can also bind the ItemsSource to a view-model collection of IToolbarItem implementations (or create ToolbarButtons in code-behind):

C# (code-behind) example:

    using MiniToolbar.Avalonia;
    using Avalonia.Controls;
    using System.Collections.ObjectModel;

    public partial class MainWindow : Window
    {
        public ObservableCollection<IToolbarItem> ToolbarItems { get; } = new();

        public MainWindow()
        {
            InitializeComponent();

            ToolbarItems.Add(new ToolbarButton { Text = "New", Icon = "/Assets/new.png" });
            ToolbarItems.Add(new ToolbarButton { Text = "Open", Icon = "/Assets/open.png" });

            // Assuming your XAML binds ItemsSource to ToolbarItems
            // this.FindControl<Toolbar>("MyToolbar").ItemsSource = ToolbarItems;
        }
    }

Notes and tips
--------------
- Use DisplayMode="Compact" to enable the compact styling (styles control when text is shown/hidden).
- The control raises the usual Button Click events on ToolbarButton.
- For advanced visuals, provide DataTemplates or custom styles in your application theme files. See the Themes/ folder and DemoApp for example styles.

License
-------
MiniToolbar.Avalonia is licensed under the terms of the MIT license (<http://opensource.org/licenses/MIT>, see LICENSE.txt).
