namespace Dr.ActionHero.Views.Extensions;

public static class PanelExtensions
{
    public static Panel Height(this Panel panel, int height)
    {
        panel.Height = height;
        return panel;
    }
}
