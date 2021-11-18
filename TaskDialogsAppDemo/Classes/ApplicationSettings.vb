Imports System.IO
Imports Newtonsoft.Json

Namespace Classes
    Public Class ApplicationSettings
        Public Property ShowAgain() As Boolean
        Public Property Heading() As String
        Public Property Text() As String
        Public Property Caption() As String
        Public Property VerificationText() As String
    End Class
    Public Class SettingOperations
        Public Shared Property FileName() As String = "appsettings.json"
        Public Shared Sub Create()
            Dim settings = New ApplicationSettings With {.ShowAgain = True, .Heading = "Are you sure you want to stop?", .Text = "Stopping the operation might leave your database in a corrupted state.", .Caption = "Confirmation", .VerificationText = "Do not show again"}
            Dim json = JsonConvert.SerializeObject(settings, Formatting.Indented)
            File.WriteAllText(FileName, json)
        End Sub
        ''' <summary>
        ''' Does <see cref="FileName"/> exists for settings
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property FileExists() As Boolean
            Get
                Return File.Exists(FileName)
            End Get
        End Property
        ''' <summary>
        ''' Read settings from file
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property GetSetting() As ApplicationSettings
            Get
                Return JsonConvert.DeserializeObject(Of ApplicationSettings)(File.ReadAllText(FileName))
            End Get
        End Property
        ''' <summary>
        ''' Indicates whether to show or not show the dialog 
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property ShowAgain() As Boolean
            Get
                Return GetSetting.ShowAgain
            End Get
        End Property
        ''' <summary>
        ''' Save settings to file
        ''' </summary>
        ''' <param name="settings"></param>
        Public Shared Sub SaveChanges(settings As ApplicationSettings)
            Dim json = JsonConvert.SerializeObject(settings, Formatting.Indented)
            File.WriteAllText(FileName, json)
        End Sub
        ''' <summary>
        ''' Save ShowAgain property only
        ''' </summary>
        ''' <param name="value"></param>
        Public Shared Sub SetShowAgain(value As Boolean)
            Dim current = GetSetting
            current.ShowAgain = value
            SaveChanges(current)
        End Sub

    End Class
End Namespace