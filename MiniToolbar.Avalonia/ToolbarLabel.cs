using System;
using Avalonia.Controls;

namespace MiniToolbar.Avalonia;

public class ToolbarLabel : Label, IToolbarItem
{
    public ToolbarLabel()
    {
        Classes.Add("ToolbarLabel");
    }

    #region Overrides of StyledElement

    protected override Type StyleKeyOverride => typeof(Label);

    #endregion
}
