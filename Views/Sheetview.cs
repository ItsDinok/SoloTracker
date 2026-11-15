using Terminal.Gui.App;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace SoloTracker;

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

    // TODO: Find a way to cut down on repetitive code here
    // I can probably use a list and a loop or something idk
    // Just get MVP out first
    private void EditRow(Row row)
    {
        Dialog dialogue = new()
        {
            Title = "Edit Row",
            Width = 100,
            Height = 25,
        };

        Label encounter_label = new(){
            Text = "Encounter",
            X = 1, Y = 1, Width = Dim.Fill (1)};

        TextField encounter_field = new(){
            Text = row.Encounter, X = 15, Y = 1,
            Width = Dim.Fill(1)};

        Label difficulty_label = new(){
            Text = "Difficulty",
            X = 1, Y = 3, Width = Dim.Fill(1)};

        TextField difficulty_field = new(){
            Text = row.Difficulty.ToString(),
            X = 15, Y = 3, Width = Dim.Fill(1)};

        Label completed_label = new(){
            Text = "Completed",
            X = 1, Y = 5, Width = Dim.Fill(1)};

        CheckBox completed_box = new(){
            Value = row.isCompleted ? CheckState.Checked : CheckState.UnChecked,
            X = 15, Y = 5};

        Label variants_label = new(){
            Text = "Variants",
            X = 1, Y = 7, Width = Dim.Fill(1)};       Label encounter_label = new(){
            Text = "Encounter",
            X = 1, Y = 1, Width = Dim.Fill (1)};

        TextField encounter_field = new(){
            Text = row.Encounter, X = 15, Y = 1,
            Width = Dim.Fill(1)};

        Label difficulty_label = new(){
            Text = "Difficulty",
            X = 1, Y = 3, Width = Dim.Fill(1)};

        TextField difficulty_field = new(){
            Text = row.Difficulty.ToString(),
            X = 15, Y = 3, Width = Dim.Fill(1)};

        Label completed_label = new(){
            Text = "Completed",
            X = 1, Y = 5, Width = Dim.Fill(1)};

        CheckBox completed_box = new(){
            Value = row.isCompleted ? CheckState.Checked : CheckState.UnChecked,
            X = 15, Y = 5};

        Label variants_label = new(){
            Text = "Variants",
            X = 1, Y = 7, Width = Dim.Fill(1)};

        TextField variants_field = new(){
            Text = row.Variants,
            X = 15, Y = 7, Width = Dim.Fill(1)};

        Label additional_label = new(){
            Text = "Additional",
            X = 1, Y = 9, Width = Dim.Fill(1)};

        TextField additional_field = new(){
            Text = row.Additional,
            X = 15, Y = 9, Width = Dim.Fill(1)};

        Label uploaded_label = new(){
            Text = "Uploaded",
            X = 1, Y = 11, Width = Dim.Fill(1)};

        CheckBox uploaded_box = new(){
            Value = row.Uploaded ? CheckState.Checked : CheckState.UnChecked,
            X = 15, Y = 11, Width = Dim.Fill(1)};

        Label upload_note_label = new(){
            Text = "Upload Notes:",
            X = 19, Y = 11, Width = Dim.Fill(1)};

        TextField upload_note_field = new(){
            Text = row.Upload_Notes,
            X = 35, Y = 11, Width = Dim.Fill(1)};

        Label warlock_label = new(){
            Text = "Warlock",
            X = 1, Y = 13, Width = Dim.Fill(1)};

        Label titan_label = new(){
            Text = "Titan",
            X = 33, Y = 13, Width = Dim.Fill(1)};

        Label hunter_label = new(){
            Text = "Hunter",
            X = 67, Y = 13, Width = Dim.Fill(1)};

        CheckBox warlock_box = new(){
            Value = row.isDoneOnWarlock ? CheckState.Checked : CheckState.UnChecked,
            X = 4, Y = 14, Width = Dim.Fill(1)};

        CheckBox titan_box = new(){
            Value = row.isDoneOnTitan ? CheckState.Checked : CheckState.UnChecked,
            X = 35, Y = 14, Width = Dim.Fill(1)};

        CheckBox hunter_box = new(){
            Value = row.isDoneOnHunter ? CheckState.Checked : CheckState.UnChecked,
            X = 69, Y = 14, Width = Dim.Fill(1)};

        Label raid_label = new(){
            Text = "Raid",
            X = 1, Y = 16, Width = Dim.Fill(1)};

        DropDownList<Raid> raid_select = new(){
            Value = row._Raid,
            X = 15, Y = 16, Width = Dim.Fill(1)};

        Label archived_label = new(){
            Text = "Archived:",
            X = 1, Y = 18, Width = Dim.Fill(1)};

        CheckBox archived_box = new(){
            Value = row.isArchived ? CheckState.Checked : CheckState.UnChecked,
            X = 15, Y = 18, Width = Dim.Fill(1)};

        TextField variants_field = new(){
            Text = row.Variants,
            X = 15, Y = 7, Width = Dim.Fill(1)};

        Label additional_label = new(){
            Text = "Additional",
            X = 1, Y = 9, Width = Dim.Fill(1)};

        TextField additional_field = new(){
            Text = row.Additional,
            X = 15, Y = 9, Width = Dim.Fill(1)};

        Label uploaded_label = new(){
            Text = "Uploaded",
            X = 1, Y = 11, Width = Dim.Fill(1)};

        CheckBox uploaded_box = new(){
            Value = row.Uploaded ? CheckState.Checked : CheckState.UnChecked,
            X = 15, Y = 11, Width = Dim.Fill(1)};

        Label upload_note_label = new(){
            Text = "Upload Notes:",
            X = 19, Y = 11, Width = Dim.Fill(1)};

        TextField upload_note_field = new(){
            Text = row.Upload_Notes,
            X = 35, Y = 11, Width = Dim.Fill(1)};

        Label warlock_label = new(){
            Text = "Warlock",
            X = 1, Y = 13, Width = Dim.Fill(1)};

        Label titan_label = new(){
            Text = "Titan",
            X = 33, Y = 13, Width = Dim.Fill(1)};

        Label hunter_label = new(){
            Text = "Hunter",
            X = 67, Y = 13, Width = Dim.Fill(1)};

        CheckBox warlock_box = new(){
            Value = row.isDoneOnWarlock ? CheckState.Checked : CheckState.UnChecked,
            X = 4, Y = 14, Width = Dim.Fill(1)};

        CheckBox titan_box = new(){
            Value = row.isDoneOnTitan ? CheckState.Checked : CheckState.UnChecked,
            X = 35, Y = 14, Width = Dim.Fill(1)};

        CheckBox hunter_box = new(){
            Value = row.isDoneOnHunter ? CheckState.Checked : CheckState.UnChecked,
            X = 69, Y = 14, Width = Dim.Fill(1)};

        Label raid_label = new(){
            Text = "Raid",
            X = 1, Y = 16, Width = Dim.Fill(1)};

        DropDownList<Raid> raid_select = new(){
            Value = row._Raid,
            X = 15, Y = 16, Width = Dim.Fill(1)};

        Label archived_label = new(){
            Text = "Archived:",
            X = 1, Y = 18, Width = Dim.Fill(1)};

        CheckBox archived_box = new(){
            Value = row.isArchived ? CheckState.Checked : CheckState.UnChecked,
            X = 15, Y = 18, Width = Dim.Fill(1)};

        dialogue.Add(
            encounter_label, encounter_field,
            difficulty_label, difficulty_field,
            completed_label, completed_box,
            variants_label, variants_field,
            additional_label, additional_field,
            uploaded_label, uploaded_box,
            upload_note_label, upload_note_field,
            warlock_label, titan_label, hunter_label,
            warlock_box, titan_box, hunter_box,
            raid_label, raid_select,
            archived_label, archived_box
        );

        dialogue.AddButton(new Button{Text = "_Cancel"});
        dialogue.AddButton(new Button{Text = "_Save"});

        App!.Run(dialogue);

        if (dialogue.Result == 1)
        {
            // Text
            row.Encounter = encounter_field.Text;
            row.Variants = variants_field.Text;
            row.Additional = additional_field.Text;
            row.Upload_Notes = upload_note_field.Text;

            // Checkboxes
            row.isCompleted = completed_box.Value == CheckState.Checked;
            row.isDoneOnWarlock = warlock_box.Value == CheckState.Checked;
            row.isDoneOnTitan = titan_box.Value == CheckState.Checked;
            row.isDoneOnHunter = hunter_box.Value == CheckState.Checked;
            row.isArchived = archived_box.Value == CheckState.Checked;

            // Enum
            row._Raid = raid_select.Value ?? Raid.LastWish;

            // Number handler
            if (float.TryParse(difficulty_field.Text, out float amount))
            {
                row.Difficulty = amount;
            }

            _table.Update();
        }

        dialogue.Dispose();
    }
}
