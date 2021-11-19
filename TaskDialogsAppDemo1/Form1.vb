Imports System.IO
Imports SupportLibrary.Classes

Public Class Form1
    Private Sub YesNoCancelQuestionButton_Click(sender As Object, e As EventArgs) Handles YesNoCancelQuestionButton.Click

        Dim result = Dialogs.Question(Me, "Question", "Do you want to save changes to Untitled?")

        If result = DialogResult.Yes Then
            MessageBox.Show("Yes")
        ElseIf result = DialogResult.No Then
            MessageBox.Show("Nope")
        ElseIf result = DialogResult.Cancel Then
            MessageBox.Show("User canceled")
        End If

    End Sub

    Private Sub QuestionSelectiveButton_Click(sender As Object, e As EventArgs) Handles QuestionSelectiveButton1.Click

        If Dialogs.Question(Me, "Question", "Do you want to save changes to Untitled?", "&Yes", "&No") Then
            MessageBox.Show("Yes")
        Else
            MessageBox.Show("No")
        End If

    End Sub

    Private Sub QuestionSelectiveButton2_Click(sender As Object, e As EventArgs) Handles QuestionSelectiveButton2.Click

        Dim DefaultButtonType = DialogResult.No
        If YesRadioButton.Checked Then
            DefaultButtonType = DialogResult.Yes
        End If

        If Dialogs.Question(Me, "Question", "Copy to clipboard?", "&Yes", "&No", DefaultButtonType) Then
            MessageBox.Show("Yes")
        Else
            MessageBox.Show("No")
        End If

    End Sub

    Private Sub SimpleQuestionButton_Click(sender As Object, e As EventArgs) Handles SimpleQuestionButton.Click

        Dim questionText = "Do it"
        If Not String.IsNullOrWhiteSpace(QuestionTextBox.Text) Then
            questionText = QuestionTextBox.Text
        End If

        If Dialogs.Question(Me, questionText) Then
            MessageBox.Show("Yes")
        Else
            MessageBox.Show("No")
        End If
    End Sub

    Private Sub InformationalButton_Click(sender As Object, e As EventArgs) Handles InformationalButton.Click
        Dialogs.Information(Me, "Your trial period is about to expire!!!", "Got it")
    End Sub

    Private Sub ErrorDialogButton_Click(sender As Object, e As EventArgs) Handles ErrorDialogButton.Click
        Try
            Dim ohCrap = File.ReadAllText("C:\Data\Orders.json")
        Catch ex As Exception
            Dialogs.ErrorBox(ex, My.Resources.agent1)
        End Try
    End Sub
End Class
