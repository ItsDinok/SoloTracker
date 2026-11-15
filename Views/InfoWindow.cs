using Terminal.Gui.App;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

// TODO: Fill out the info window
// TODO: Figure out what this section is for

namespace SoloTracker;

internal sealed class InfoWindow : Window
{
    public InfoWindow()
    {
        Button back = new()
        {
            Text = "_Back",
            X = 1,
            Y = Pos.AnchorEnd()
        };
        back.Accepted += (_, _) => App!.RequestStop(this);

        Add(back);
    }
}
