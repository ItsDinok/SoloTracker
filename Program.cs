using Terminal.Gui.App;           // Application, IApplication, MessageBox
using Terminal.Gui.Configuration; // ConfigurationManager
using Terminal.Gui.ViewBase;      // View, Pos, Dim
using Terminal.Gui.Views;         // Window, Label, Button
using SoloTracker;

ConfigurationManager.Enable (ConfigLocations.All);

Application
    .Create ()
    .Run<MainWindow> ()
    .Dispose ();

internal sealed class MainWindow : Window
{
    public MainWindow ()
    {
        Title = "Solo Tracker  (Esc to quit)";

        Label welcome = new ()
        {
            Text = "Solo Raid Encounter Tracker!",
            X = Pos.Center (),
            Y = 1
        };


        // Buttons: View data, statistics, info, quit
        Button view_data = new()
        {
            Text = "_View Spreadsheet",
            X = Pos.Center (),
            Y = Pos.Center (),
        };

        Button statistics = new()
        {
            Text = "_Statistics",
            X = Pos.Center (),
            Y = Pos.Bottom(view_data) + 1,
        };

        Button info = new()
        {
            Text = "_Info",
            X = Pos.Center (),
            Y = Pos.Bottom(statistics) + 1,
        };

        Button quit = new ()
        {
            Text = "_Quit",        // the leading _ defines the hotkey (Alt+Q)
            X = Pos.Center (),
            Y = Pos.Bottom (info) + 1
        };

        quit.Accepted += (_, _) => App!.RequestStop ();
        view_data.Accepted += (_, _) => {
            RowsWindow rowswindow = new(JsonManager.Read_Json());
            App!.Run(rowswindow);
            rowswindow.Dispose();
        };

        info.Accepted += (_, _) => {
            InfoWindow info_window = new();
            App!.Run(info_window);
            info_window.Dispose();
        };

        Add (welcome, view_data, statistics, info, quit);
    }
}
