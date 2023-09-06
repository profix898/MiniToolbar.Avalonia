using System;
using Avalonia.Controls;

namespace MiniToolbar.Avalonia;

public class ToolbarCheckBox : CheckBox, IToolbarItem
{
    public ToolbarCheckBox()
    {
        Classes.Add("ToolbarCheckBox");
    }

    #region Overrides of StyledElement

    protected override Type StyleKeyOverride => typeof(CheckBox);

    #endregion
}
