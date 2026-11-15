using Terminal.Gui.App;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace SoloTracker;

// TODO: Pretty print enums
// TODO: Add deletion
// TODO: Handle incomplete rows

// Full screen window thta shows rows as a table
// Opened from MainWindow with the App!.Run();

internal sealed class RowsWindow : Window
{
    private readonly List<Row> _rows;
    private readonly TableView _table;

    public RowsWindow(List<Row> rows)
    {
        _rows = rows;
        Title = "Rows (Esc to go back)";

        _table = new()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(2), // Leave two lines at bottom for buttons
            FullRowSelect = true
        };
        ShowRows();

        // Enter / double-click on a row
        _table.Accepted += (_, _) =>
        {
            var cell = _table.Value!.SelectedCell; // X = column, y = row
            EditRow(_rows [cell.Y]);
            JsonManager.Write_Json(_rows);
        };

        Button add = new()
        {
            Text = "_Add",
            X = 1,
            Y = Pos.AnchorEnd()
        };
        Button save = new()
        {
            Text = "_Save",
            X = Pos.Right(add) + 2,
            Y = Pos.AnchorEnd()
        };
        Button back = new()
        {
            Text = "_Back",
            X = Pos.Right(save) + 2,
            Y = Pos.AnchorEnd()
        };

        save.Accepted += (_, _) => JsonManager.Write_Json(_rows);
        back.Accepted += (_, _) => App!.RequestStop(this);
        add.Accepted += (_, _) =>
        {
            Row row = new();
            EditRow(row);
            _rows.Add(row);
            JsonManager.Write_Json(rows);
            ShowRows();
        };

        Add(_table, add, save, back);
    }

    private void ShowRows()
    {
        _table.Table = new EnumerableTableSource<Row>(
            _rows,
            new Dictionary<string, Func<Row, object>>
            {
                {"Encounter",       r => r.Encounter},
                {"Difficulty",      r => r.Difficulty},
                {"Complete",        r => r.isCompleted},
                {"Variants",        r => r.Variants},
                {"Additional",      r => r.Additional},
                {"Uploaded",        r => r.Uploaded},
                {"Upload Notes",    r => r.Upload_Notes},
                {"Warlock",         r => r.isDoneOnWarlock},
                {"Titan",           r => r.isDoneOnTitan},
                {"Hunter",          r => r.isDoneOnHunter},
                {"Raid",            r => r._Raid},
                {"Archived",        r => r.isArchived}
            }
        );
    }

    private void EditRow(Row row)
    {
        Dialog dialogue = new()
        {
            Title = "Edit Row",
            Width = 100,
            Height = 25,
        };

        int y = 1;

        T AddRow<T> (string label, T view) where T : View
        {
            view.X = 15;
            view.Y = y;
            dialogue.Add(new Label {Text = label, X = 1, Y = y}, view);
            y += 2;
            return view;
        }

        static CheckState ToState (bool value) => value ? CheckState.Checked : CheckState.UnChecked;
        static bool IsChecked (CheckBox box) => box.Value == CheckState.Checked;

        TextField   encounter       = AddRow("Encounter",   new TextField {Text = row.Encounter, Width = Dim.Fill(1)});
        TextField   difficulty      = AddRow("Difficulty",  new TextField {Text = row.Difficulty.ToString(), Width = Dim.Fill(1)});
        CheckBox    completed       = AddRow("Completed",   new CheckBox  {Value = ToState(row.isCompleted), Width = Dim.Fill(1)});
        TextField   variants        = AddRow("Variants",    new TextField {Text = row.Variants, Width = Dim.Fill(1)});
        TextField   additional      = AddRow("Additional",  new TextField {Text = row.Additional, Width = Dim.Fill(1)});

        CheckBox    uploaded        = AddRow("Uploaded", new CheckBox{Value = ToState(row.Uploaded)});
        TextField   upload_notes    = new() {Text = row.Upload_Notes, X = 35, Y = Pos.Top(uploaded), Width = Dim.Fill(1)};
        dialogue.Add(new Label{Text = "Upload Notes:", X = 19, Y = Pos.Top(uploaded)}, upload_notes);

        CheckBox    warlock     = new () { Text = " Warlock", Value = ToState(row.isDoneOnWarlock), X = 1, Y = y};
        CheckBox    titan       = new () { Text = " Titan", Value = ToState(row.isDoneOnTitan), X = 33, Y = y};
        CheckBox    hunter      = new () { Text = " Hunter", Value = ToState(row.isDoneOnHunter), X = 67, Y = y};
        dialogue.Add(warlock, titan, hunter);
        y += 2;

        DropDownList<Raid> raid = AddRow("Raid", new DropDownList<Raid> { Value = row._Raid, Width = Dim.Fill(1)});
        CheckBox    archived    = AddRow("Archived", new CheckBox { Value = ToState(row.isArchived)});

        dialogue.AddButton(new Button{Text = "_Cancel"});
        dialogue.AddButton(new Button{Text = "_Save"});

        App!.Run(dialogue);

        if (dialogue.Result == 1)
        {
            row.Encounter       = encounter.Text;
            row.isCompleted     = IsChecked(completed);
            row.Variants        = variants.Text;
            row.Additional      = additional.Text;
            row.Uploaded        = IsChecked(uploaded);
            row.Upload_Notes    = upload_notes.Text;
            row.isDoneOnWarlock = IsChecked(warlock);
            row.isDoneOnTitan   = IsChecked(titan);
            row.isDoneOnHunter  = IsChecked(hunter);
            row._Raid           = raid.Value ?? row._Raid; // Keep old value if raid empty
            row.isArchived      = IsChecked(archived);

            if (float.TryParse(difficulty.Text, out float amount))
            {
                row.Difficulty = amount;
            }

            _table.Update();
        }

        dialogue.Dispose();
    }
}
