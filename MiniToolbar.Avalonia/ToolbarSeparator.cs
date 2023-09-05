using System;
using Avalonia.Controls;

namespace MiniToolbar.Avalonia;

public class ToolbarSeparator : Panel, IToolbarItem
{
    public ToolbarSeparator()
    {
        Classes.Add("ToolbarSeparator");
    }

    #region Overrides of StyledElement

    protected override Type StyleKeyOverride => typeof(Panel);

    #endregion
}