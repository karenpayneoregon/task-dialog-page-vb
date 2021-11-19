Imports TaskDialogsAppDemo.Classes

Public Class Form1
    Private Sub DoNotShowAgainButton_Click(sender As Object, e As EventArgs) Handles DoNotShowAgainButton.Click

        Dim settings = SettingOperations.GetSetting

        If Not settings.ShowAgain Then
            Exit Sub
        End If

        Dim options As New NoShowAgain With
                {
                    .Heading = settings.Heading,
                    .Text = settings.Text,
                    .Caption = settings.Caption,
                    .Icon = My.Resources.vb,
                    .VerificationText = settings.VerificationText,
                    .Owner = Me
                }

        Dim result As (DialogResult As NoShow, ShowAgain As Boolean) = Dialogs.DoNotShowAgain(options)

        ShowAgainCheckBox.Checked = result.ShowAgain

        ListBox1.Items.Add(result.ToString())

    End Sub

    Private Sub AutoCloseButton_Click(sender As Object, e As EventArgs) Handles AutoCloseButton.Click
        AddHandler Dialogs.RetryHandler, AddressOf RetryConnection
        Dialogs.ShowAutoClosingTaskDialog(Me, Icon)
        RemoveHandler Dialogs.RetryHandler, AddressOf RetryConnection
    End Sub

    Private Sub RetryConnection(sender As Boolean)
        RetryLabel.Text = $"Try again? {sender.ToYesNoString()}"
    End Sub

    Private Sub ShowAgainCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ShowAgainCheckBox.CheckedChanged
        Dim settings = SettingOperations.GetSetting
        settings.ShowAgain = ShowAgainCheckBox.Checked
        SettingOperations.SaveChanges(settings)
    End Sub
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        If Not SettingOperations.FileExists Then
            SettingOperations.Create()
        End If

        ShowAgainCheckBox.Checked = SettingOperations.ShowAgain
    End Sub

    Private Sub AskQuestionButton_Click(sender As Object, e As EventArgs) Handles AskQuestionButton.Click

        Dim result = Dialogs.Question(Me, "Question", "Do you want to save changes to Untitled?")

        If result = DialogResult.Yes Then
            MessageBox.Show("Yes")
        ElseIf result = DialogResult.No Then
            MessageBox.Show("Nope")
        ElseIf result = DialogResult.Cancel Then
            MessageBox.Show("User canceled")
        End If

    End Sub

    Private Sub MultiDialogButton_Click(sender As Object, e As EventArgs) Handles MultiDialogButton.Click
        Dialogs.ShowMultiPageTaskDialog()
    End Sub
End Class
