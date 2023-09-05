using System;
using Avalonia.Controls;

namespace MiniToolbar.Avalonia;

public class ToolbarComboBox : ComboBox, IToolbarItem
{
    public ToolbarComboBox()
    {
        Classes.Add("ToolbarComboBox");
    }

    #region Overrides of StyledElement

    protected override Type StyleKeyOverride => typeof(ComboBox);

    #endregion
}
