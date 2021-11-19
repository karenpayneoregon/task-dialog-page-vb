Imports System.Drawing
Imports System.Windows.Forms

Namespace Classes
    ''' <summary>
    ''' Collection of dialogs
    ''' </summary>
    ''' <remarks>
    ''' Each dialog first parameter is owner, you can bypass this as shown in Information overload if so desire
    ''' </remarks>
    Public Class Dialogs
        Public Delegate Sub OnReconnect(sender As Boolean)
        ''' <summary>
        ''' For <see cref="AutoClosingTaskDialog"/> to send decision back to caller
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
                        .ProgressBar = New TaskDialogProgressBar() With {.State = TaskDialogProgressBarState.Paused},
                        .Buttons = New TaskDialogButtonCollection() From {reconnectButton, cancelButton}
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
        Public Shared Function Question(owner As Form, caption As String, heading As String, YesText As String, NoText As String) As Boolean

            Dim YesButton = New TaskDialogButton(YesText) With {.Tag = DialogResult.Yes}
            Dim NoButton = New TaskDialogButton(NoText) With {.Tag = DialogResult.No}


            Dim page = New TaskDialogPage() With {
                        .Caption = caption,
                        .SizeToContent = True,
                        .Heading = heading,
                        .Icon = TaskDialogIcon.Information,
                        .Buttons = New TaskDialogButtonCollection() From {YesButton, NoButton}
                    }


            Dim result = TaskDialog.ShowDialog(owner, page)

            Return CType(result.Tag, DialogResult) = DialogResult.Yes

        End Function
        Public Shared Function Question(owner As Form, heading As String) As Boolean

            Dim YesButton = New TaskDialogButton("Yes") With {.Tag = DialogResult.Yes}
            Dim NoButton = New TaskDialogButton("No") With {.Tag = DialogResult.No}


            Dim page = New TaskDialogPage() With {
                        .Caption = "Question",
                        .SizeToContent = True,
                        .Heading = heading,
                        .Icon = TaskDialogIcon.Information,
                        .Buttons = New TaskDialogButtonCollection() From {NoButton, YesButton}
                    }


            Dim result = TaskDialog.ShowDialog(owner, page)

            Return CType(result.Tag, DialogResult) = DialogResult.Yes

        End Function
        Public Shared Sub Information(owner As Form, heading As String, Optional buttonText As String = "Ok")

            Dim SingleButton = New TaskDialogButton(buttonText)

            Dim page = New TaskDialogPage() With {
                        .Caption = "Information",
                        .SizeToContent = True,
                        .Heading = heading,
                        .Icon = TaskDialogIcon.Warning,
                        .Buttons = New TaskDialogButtonCollection() From {SingleButton}
                    }


            TaskDialog.ShowDialog(owner, page)

        End Sub

        Public Shared Sub Information(heading As String, Optional buttonText As String = "Ok")

            Dim SingleButton = New TaskDialogButton(buttonText)

            Dim page = New TaskDialogPage() With {
                        .Caption = "Information",
                        .SizeToContent = True,
                        .Heading = heading,
                        .Icon = TaskDialogIcon.Warning,
                        .Buttons = New TaskDialogButtonCollection() From {SingleButton}
                    }

            '
            ' Note that owner is not passed
            '
            TaskDialog.ShowDialog(page)

        End Sub
        Public Shared Sub ErrorBox(exception As Exception, Optional buttonText As String = "Silly programmer")

            Dim SingleButton = New TaskDialogButton(buttonText)

            Dim text = $"Encountered the following{vbLf}{exception.Message}"


            Dim page = New TaskDialogPage() With {
                        .Caption = "Information",
                        .SizeToContent = True,
                        .Heading = text,
                        .Icon = TaskDialogIcon.Error,
                        .Buttons = New TaskDialogButtonCollection() From {SingleButton}
                    }


            TaskDialog.ShowDialog(page)

        End Sub
        Public Shared Sub ErrorBox(exception As Exception, icon As Icon)

            Dim SingleButton = New TaskDialogButton("Ooops")

            Dim text = $"Encountered the following{vbLf}{exception.Message}"

            Dim footer = New TaskDialogFootnote("Paid support available at (555) 555-5555")
            footer.Icon = New TaskDialogIcon(icon)


            Dim page = New TaskDialogPage() With {
                        .Caption = "Information",
                        .SizeToContent = True,
                        .Heading = text,
                        .Icon = TaskDialogIcon.Error,
                        .Footnote = footer,
                        .Buttons = New TaskDialogButtonCollection() From {SingleButton}
                    }


            TaskDialog.ShowDialog(page)

        End Sub
        Public Shared Function Question(owner As Form, caption As String, heading As String, YesText As String, NoText As String, DefaultButton As DialogResult) As Boolean

            Dim YesButton = New TaskDialogButton(YesText) With {.Tag = DialogResult.Yes}
            Dim NoButton = New TaskDialogButton(NoText) With {.Tag = DialogResult.No}

            Dim buttons = New TaskDialogButtonCollection

            If DefaultButton = DialogResult.Yes Then
                buttons.Add(YesButton)
                buttons.Add(NoButton)
            Else
                buttons.Add(NoButton)
                buttons.Add(YesButton)
            End If


            Dim page = New TaskDialogPage() With {
                        .Caption = caption,
                        .SizeToContent = True,
                        .Heading = heading,
                        .Icon = TaskDialogIcon.Information,
                        .Buttons = buttons
                    }


            Dim result = TaskDialog.ShowDialog(owner, page)

            Return CType(result.Tag, DialogResult) = DialogResult.Yes

        End Function
        Public Shared Function QuestionBackup(owner As Form, caption As String, heading As String) As (result As DialogResult, full As Boolean)

            Dim ContinueButton = New TaskDialogButton("&Continue") With {.Tag = DialogResult.Yes}
            Dim CancelButton = New TaskDialogButton("&Cancel") With {.Tag = DialogResult.Cancel}


            Dim page = New TaskDialogPage() With {
                        .Caption = caption,
                        .SizeToContent = True,
                        .Heading = heading,
                        .Icon = TaskDialogIcon.Information,
                        .Buttons = New TaskDialogButtonCollection() From {ContinueButton, CancelButton}
                    }

            Dim radioButtonFull = page.RadioButtons.Add("&Full backup")
            Dim radioButtonIncremental = page.RadioButtons.Add("Incremental backup")

            radioButtonFull.Checked = True

            '
            ' for a real application, if s pre action is needed insert code in CheckedChange or perhaps
            ' raise an event.
            '
            AddHandler radioButtonFull.CheckedChanged,
                Sub(s, e)
                    'Debug.WriteLine("Full Backup: " & radioButtonFull.Checked)
                End Sub

            AddHandler radioButtonIncremental.CheckedChanged,
                Sub(s, e)
                    'Debug.WriteLine("Incremental Backup: " & radioButtonIncremental.Checked)
                End Sub

            Dim result = CType(TaskDialog.ShowDialog(owner, page).Tag, DialogResult)

            Return (result, radioButtonFull.Checked)

        End Function

    End Class
End Namespace