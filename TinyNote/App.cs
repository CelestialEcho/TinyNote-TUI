using Terminal.Gui;
using TinyNote;

using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System;

class App
{
    public static ColorScheme sameAsBg = new()
    {
        Normal    = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black),
        Focus     = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black),
        HotNormal = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black),
        HotFocus  = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black),
        Disabled  = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black)
    };

    public static Toplevel top = Application.Top;
    public static FrameView sidebar = new("Notes") { X = 0, Y = 0, Width = 22, Height = Dim.Fill() - 5 };
    public static FrameView creator = new("Note Manager") { X = 0, Y = Pos.Bottom(sidebar), Width = 22, Height = Dim.Fill() - sidebar.Frame.Height };
    public static FrameView noteviewer = new("Note Content") { X = 22, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

    private static void RefreshNotes()
    {
        NoteManager.UpdateInfo(out List<Note> note_buttons, out List<string> note_paths);
        sidebar.RemoveAll();

        for (int i = 0; i < note_buttons.Count; i++)
        {
            var tempNote = note_buttons[i];
            var tempButton = new Button(tempNote.name) { X = 0, Y = i, Width = Dim.Fill() };
            int index = i;
            tempButton.Clicked += () => OpenNoteEditor(tempNote, tempButton, note_buttons, note_paths, index);
            sidebar.Add(tempButton);
        }
    }

    private static void OpenNoteEditor(Note note, Button button, List<Note> notes, List<string> paths, int index)
    {
        var inputField = new TextView()
        {
            Text = note.content,
            X = 1,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = sameAsBg,
            WordWrap = true
        };

        inputField.KeyDown += (e) =>
        {
            if (e.KeyEvent.Key == Key.Esc)
            {
                notes[index].Update(inputField.Text.ToString());
                File.WriteAllText(paths[index], notes[index].content);
                button.SetFocus();
                e.Handled = true;
            }
        };

        noteviewer.RemoveAll();
        noteviewer.Add(inputField);
        inputField.SetFocus();
    }

    private static void SetupCreateButton()
    {
        var createNote = new Button("Create") { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };
        creator.Add(createNote);

        createNote.Clicked += () =>
        {
            var dialog = new Dialog("Note Creation", 60, 5);
            var textField = new TextField()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                ColorScheme = sameAsBg
            };

            var cancel = new Button("Cancel") { X = 30, Y = 2, Width = Dim.Fill() };
            cancel.Clicked += () => Application.RequestStop(dialog);

            var ok = new Button("Ok") { X = 0, Y = 2, Width = Dim.Fill() - 30 };
            ok.Clicked += () =>
            {
                var t = File.Create($"{NoteManager.basePath}/{textField.Text}.tn");
                t.Close();
                RefreshNotes();
                Application.RequestStop(dialog);
            };

            dialog.Add(textField, ok, cancel);
            Application.Run(dialog);
        };
    }

    private static void SetupUI()
    {
        top.Add(sidebar);
        top.Add(creator);
        top.Add(noteviewer);

        var dTextArt = @"
        MMP""MM""YMM   db                     `7MN.   `7MF'         mm           
        P'   MM      `7                       MMN.    M           MM           
             MM    `7MM  `7MMpMMMb.`7M'   `MF'M YMb   M  ,pW""Wq.mmMMmm .gP""Ya  
             MM      MM    MM    MM  VA   ,V  M  `MN. M 6W'   `Wb MM  ,M'   Yb 
             MM      MM    MM    MM   VA ,V   M   `MM.M 8M     M8 MM  8M"""""" 
             MM      MM    MM    MM    VVV    M     YMM YA.   ,A9 MM  YM.    , 
           .JMML.  .JMML..JMML  JMML.  ,V   .JML.    YM  `Ybmd9'  `Mbmo`Mbmmd' 
                                      ,V                                       
                                   OOb""                                        ";

        var version = Assembly.GetExecutingAssembly().GetName().Version;
        var dTextInfo = $"Build {version?.Major ?? 0}.{version?.Minor ?? 0} | author: https://github.com/CelestialEcho \nExit: F12\nSave Note: ESC";

        var defaultArt = new Label(dTextArt) { X = Pos.Center(), Y = Pos.Center() };
        var defaultText = new Label(dTextInfo) { X = Pos.Center(), Y = defaultArt.Y + 5 };
        noteviewer.Add(defaultArt, defaultText);

        top.KeyDown += (e) =>
        {
            if (e.KeyEvent.Key == Key.F12)
                Application.RequestStop(top);
        };
    }

    public static void Main()
    {
        Console.Title = "TinyNote-TUI";

        Application.Init();

        Colors.Base.Normal    = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black);
        Colors.Base.Focus     = Application.Driver.MakeAttribute(Color.Black, Color.BrightYellow);
        Colors.Base.HotNormal = Application.Driver.MakeAttribute(Color.BrightRed, Color.Black);
        Colors.Base.HotFocus  = Application.Driver.MakeAttribute(Color.BrightRed, Color.BrightYellow);
        Colors.TopLevel.Normal= Application.Driver.MakeAttribute(Color.White, Color.Black);
        Colors.TopLevel.Focus = Application.Driver.MakeAttribute(Color.Black, Color.BrightYellow);
        Colors.Menu.Normal    = Application.Driver.MakeAttribute(Color.BrightCyan, Color.Black);
        Colors.Menu.Focus     = Application.Driver.MakeAttribute(Color.Black, Color.BrightCyan);

        NoteManager.Init();

        SetupUI();
        RefreshNotes();
        SetupCreateButton();

        Application.Run();
    }
}
