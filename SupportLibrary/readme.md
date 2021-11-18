# About

Provides helper methods using [TaskDialogPage](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogpage?view=windowsdesktop-6.0). 

These code samples are based off code samples done by Microsoft while Microsoft's were done in a form these are in a class library which a form's project can reference and use without the need to copy-n-paste code from project to project.

For those who don't care for class libraries the methods here are done in TaskDialosAppDemo without calling this library.

|Method| Description|
|:-----|:-----|
| DoNotShowAgain |Display a modal dialog with open to not show again|
| Question |Ask a question returning [DialogResult](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.dialogresult?view=windowsdesktop-6.0) |
| AutoClosingTaskDialog |Presents a dialog with progressbar asking to retry or cancel an operation.|

## Requires

.NET Core 5 or higher, [NewtonSoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) NuGet package while native Json classes may be used instead.