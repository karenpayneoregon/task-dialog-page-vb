Imports System.Drawing
Imports System.Windows.Forms

Namespace Classes
    Public Class Dialogs
        Public Delegate Sub OnReconnect(sender As Boolean)
        ''' <summary>
        ''' For <see cref="ShowAutoClosingTaskDialog"/> to send decision back to caller
        ''' </summary>
        Public Shared Event RetryHandler As OnReconnect
        ''' <summary>
        ''' Display a modal dialog with open to not show again
        ''' </summary>
        ''' <param name="Options">Value to set <see cref="TaskDialogPage"/></param>
        ''' <param name="Icon">Icon to present with dialog</param>
        ''' <returns>Value tuple of <see cref="NoShow"/> result, boolean indicator to show or not show again</returns>
        Public Shared Function DoNotShowAgain(Options As NoShowAgain, Icon As Icon) As (DialogResult As NoShow, ShowAgain As Boolean)

            Dim page = New TaskDialogPage() With
                    {
                        .Heading = Options.Heading,
                        .Text = Options.Text,
                        .Caption = Options.Caption,
                        .Icon = New TaskDialogIcon(Icon),
                        .AllowCancel = True,
                        .Verification = New TaskDialogVerificationCheckBox() With {.Text = Options.VerificationText},
                        .Buttons = New TaskDialogButtonCollection() From {TaskDialogButton.Yes, TaskDialogButton.No},
                        .DefaultButton = TaskDialogButton.No
                    }

            If TaskDialog.ShowDialog(Options.Owner, page, TaskDialogStartupLocation.CenterScreen) = TaskDialogButton.Yes Then

                Dim showAgain As Boolean

                If page.Verification.Checked Then
                    SettingOperations.SetShowAgain(False)
                    showAgain = False
                Else
                    SettingOperations.SetShowAgain(True)
                    showAgain = True
                End If

                Return (NoShow.StopOperation, showAgain)

            Else

                Return (NoShow.No, True)

            End If

        End Function
        ''' <summary>
        ''' Presents a dialog with progressbar asking to retry or cancel an operation.
        ''' Use <see cref="RetryHandler"/> in caller to inform if a retry should be done or not
        ''' </summary>
        ''' <param name="owner">Calling form</param>
        ''' <param name="Icon">Icon to use e.g. the app icon or a custom icon</param>
        Public Shared Sub AutoClosingTaskDialog(owner As Form, Icon As Icon)

            Const textFormat = "Reconnecting in {0} seconds..."
            Dim remainingTenthSeconds = 50

            Dim reconnectButton = New TaskDialogButton("&Reconnect now")
            Dim cancelButton = TaskDialogButton.Cancel

            ' Display the form's icon in the task dialog.
            ' Note however that the task dialog will not scale the icon.
            Dim page = New TaskDialogPage() With
                    {
                        .Heading = "Connection lost; reconnecting...",
                        .Text = String.Format(textFormat, (remainingTenthSeconds + 9) \ 10),
                        .Icon = New TaskDialogIcon(Icon),
                        .ProgressBar = New TaskDialogProgressBar() With
                        {
                            .State = TaskDialogProgressBarState.Paused
                        },
                        .Buttons = New TaskDialogButtonCollection() From
                        {
                            reconnectButton,
                            cancelButton
                        }
                    }

            Using timer = New Timer() With {.Enabled = True, .Interval = 100}

                AddHandler timer.Tick, Sub(sender, args)

                                           remainingTenthSeconds -= 1

                                           If remainingTenthSeconds > 0 Then
                                               page.Text = String.Format(textFormat, (remainingTenthSeconds + 9) \ 10)
                                               page.ProgressBar.Value = 100 - remainingTenthSeconds * 2
                                           Else
                                               timer.Enabled = False
                                               reconnectButton.PerformClick()
                                           End If

                                       End Sub

                Dim result As TaskDialogButton = TaskDialog.ShowDialog(owner, page)

                If result = reconnectButton Then
                    RaiseEvent RetryHandler(True)
                Else
                    RaiseEvent RetryHandler(False)
                End If

            End Using

        End Sub

        ''' <summary>
        ''' Ask a question
        ''' </summary>
        ''' <param name="owner">Calling form</param>
        ''' <param name="caption">Caption for dialog</param>
        ''' <param name="heading">Heading for dialog</param>
        ''' <returns><see cref="DialogResult"/></returns>
        Public Shared Function Question(owner As Form, caption As String, heading As String) As DialogResult

            Dim CancelButton = New TaskDialogButton("&Cancel") With {.Tag = DialogResult.Cancel}
            Dim SaveButton = New TaskDialogButton("&Save") With {.Tag = DialogResult.Yes}
            Dim DoNotSaveSave = New TaskDialogButton("Do&n't save") With {.Tag = DialogResult.No}

            Dim page = New TaskDialogPage() With {
                    .Caption = caption,
                    .SizeToContent = True,
                    .Heading = heading,
                    .Icon = TaskDialogIcon.Information,
                    .Buttons = New TaskDialogButtonCollection() From {CancelButton, SaveButton, DoNotSaveSave}
                    }


            Dim result = TaskDialog.ShowDialog(owner, page)

            Return CType(result.Tag, DialogResult)

        End Function
    End Class
End Namespace