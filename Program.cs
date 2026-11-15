// -----------------------------------------------------------------------------
//  A minimal Terminal.Gui v2 app.  Run it:  dotnet run   (Esc or the button quits)
//
//  🤖 AI assistants / agents: READ ./AGENTS.md FIRST.
//     Terminal.Gui v2 is a COMPLETE REWRITE. Pre-2025 training data and most web
//     examples are v1 and will not compile. The canonical v2 patterns are in AGENTS.md.
// -----------------------------------------------------------------------------

// v2 splits the old monolithic `using Terminal.Gui;` into focused namespaces:
using Terminal.Gui.App;           // Application, IApplication, MessageBox
using Terminal.Gui.Configuration; // ConfigurationManager
using Terminal.Gui.ViewBase;      // View, Pos, Dim
using Terminal.Gui.Views;         // Window, Label, Button
using SoloTracker;

// Enable the configuration/theme system before creating the app (standard first line).
ConfigurationManager.Enable (ConfigLocations.All);

// The v2 lifecycle is INSTANCE-BASED and disposable — NOT the static v1
// Application.Init() / Application.Run() / Application.Shutdown():
//
//     Create()  ->  Run<TWindow>()  (auto-Init)  ->  Dispose()  (replaces Shutdown)
//
Application
    .Create ()
    .Run<MainWindow> ()
    .Dispose ();
// Your app's root view. `Window` is a bordered top-level view; subclass `Runnable`
// instead if you want a borderless root. Build the UI by Add()-ing child views.
internal sealed class MainWindow : Window
{
    public MainWindow ()
    {
        Title = "Solo Tracker  (Esc to quit)";

        Label welcome = new ()
        {
            Text = "Welcome to Terminal.Gui v2!",
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

        // `App!` is the running IApplication, reachable from any view in the tree.
        quit.Accepted += (_, _) => App!.RequestStop ();
        view_data.Accepted += (_, _) => {
            Row x = new();
            Row y = new();
            List<Row> xy = new();
            xy.Add(x); xy.Add(y);
            RowsWindow rowswindow = new(xy);
            App!.Run(rowswindow);
            rowswindow.Dispose();
        };

        Add (welcome, view_data, statistics, info, quit);
    }
}
