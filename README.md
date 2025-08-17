<div style="display: flex; flex-direction: column; align-items: center; justify-content: center; gap: 20px;">
  <div>
    <img 
      src="./git_assets/TinyNote.png" 
      alt="TinyNote-TUI-logo" 
      width="571">
    <div>
      <h1></h1>
      <div style="text-align: center;">
        <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#">
        <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logoColor=white" alt=".NET">
        <img src="https://img.shields.io/badge/TUI-000000?style=for-the-badge&logoColor=white" alt="TUI">
        <img src="https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white" alt="Windows">
        </div>
    </div>
  </div>

  <div style="text-align: center;">
    <p><strong>TinyNote</strong>is a minimalist terminal-based note manager written in C# using the Terminal.Gui library.</p>
  </div>
</div>

---

## showcase

![Screenshot_main](./git_assets\Screenshot_main.png)
![Screenshot_note_open](./git_assets\Screenshot_note_open.png)
![Screenshot_creator](./git_assets\Screenshot_creator.png)

---

## Installation : dependencies

1. You need to install **[.NET 9.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)** if it’s not already installed.
2.  You need Git installed for cloning the repository.

## Intstallation

1. `git clone https://github.com/CelestialEcho/TinyNote-TUI.git`
2. `cd TineNote-TUI`
3. `dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishReadyToRun=true`
4. You'r done! Check out `build\net9.0\win-x64\publish\` path.

## Backend Overview

TinyNote stores all notes in the `%appdata%/TinyNote` directory. On startup, the application checks if this directory exists - if not, it creates it. Each .tn file in this folder is added into NoteBuffer and represented as a Note object. In the TUI, a side panel displays buttons for each note object.





