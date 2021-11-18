
Namespace Classes
    Public Class Dialogs
        Public Delegate Sub OnReconnect(sender As Boolean)
        Public Shared Event RetryHandler As OnReconnect
        Public Shared Function DoNotShowAgain(Options As NoShowAgain) As (DialogResult As NoShow, ShowAgain As Boolean)

            Dim page = New TaskDialogPage() With
                    {
                        .Heading = Options.Heading,
                        .Text = Options.Text,
                        .Caption = Options.Caption,
                        .Icon = New TaskDialogIcon(Options.Icon),
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

        Public Shared Sub ShowAutoClosingTaskDialog(owner As Form, Icon As Icon)

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
                        .Caption = "Alert",
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

                '
                ' Perhaps invoke an event here?
                '
                If result = reconnectButton Then
                    Debug.WriteLine("Reconnecting.")
                    RaiseEvent RetryHandler(True)
                Else
                    Debug.WriteLine("Not reconnecting.")
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
            ' Here for a real application 
            '
            AddHandler radioButtonFull.CheckedChanged,
                Sub(s, e)
                    Debug.WriteLine("Full Backup: " & radioButtonFull.Checked)
                End Sub

            AddHandler radioButtonIncremental.CheckedChanged,
                Sub(s, e)
                    Debug.WriteLine("Incremental Backup: " & radioButtonIncremental.Checked)
                End Sub

            Dim result = CType(TaskDialog.ShowDialog(owner, page).Tag, DialogResult)

            Return (result, radioButtonFull.Checked)

        End Function

        Public Shared Sub ShowMultiPageTaskDialog()
            ' Disable the "Yes" button and only enable it when the check box is checked.
            ' Also, don't close the dialog when this button is clicked.
            Dim initialButtonYes = TaskDialogButton.Yes
            initialButtonYes.Enabled = False
            initialButtonYes.AllowCloseDialog = False

            ' A modeless dialog can be minimizable.
            Dim initialPage = New TaskDialogPage() With {
                .Caption = "My Application",
                .Heading = "Clean up database?",
                .Text = $"Do you really want to do a clean-up?{vbLf}This action is irreversible!",
                .Icon = TaskDialogIcon.ShieldWarningYellowBar,
                .AllowCancel = True,
                .AllowMinimize = True,
                .Verification = New TaskDialogVerificationCheckBox() With {.Text = "I know what I'm doing"
            },
            .Buttons = New TaskDialogButtonCollection() From {TaskDialogButton.No, initialButtonYes},
            .DefaultButton = TaskDialogButton.No
        }

            ' For the "In Progress" page, don't allow the dialog to close, by adding
            ' a disabled button (if no button was specified, the task dialog would
            ' get an (enabled) 'OK' button).
            Dim inProgressCloseButton = TaskDialogButton.Close
            inProgressCloseButton.Enabled = False

            Dim inProgressPage = New TaskDialogPage() With {
                    .Caption = "My Application",
                    .Heading = "Operation in progress...",
                    .Text = "Please wait while the operation is in progress.",
                    .Icon = TaskDialogIcon.Information,
                    .AllowMinimize = True,
                .ProgressBar = New TaskDialogProgressBar() With {.State = TaskDialogProgressBarState.Marquee
            },
            .Expander = New TaskDialogExpander() With {
                .Text = "Initializing...",
                .Position = TaskDialogExpanderPosition.AfterFootnote
            },
            .Buttons = New TaskDialogButtonCollection() From {
                inProgressCloseButton
            }
        }

            ' Add an invisible Cancel button where we will intercept the Click event
            ' to prevent the dialog from closing (when the User clicks the "X" button
            ' in the title bar or presses ESC or Alt+F4).
            Dim invisibleCancelButton = TaskDialogButton.Cancel
            invisibleCancelButton.Visible = False
            invisibleCancelButton.AllowCloseDialog = False
            inProgressPage.Buttons.Add(invisibleCancelButton)

            Dim finishedPage = New TaskDialogPage() With {
                .Caption = "My Application",
                .Heading = "Success!",
                .Text = "The operation finished.",
                .Icon = TaskDialogIcon.ShieldSuccessGreenBar,
                .AllowMinimize = True,
                .Buttons = New TaskDialogButtonCollection() From {TaskDialogButton.Close}
        }

            Dim showResultsButton As TaskDialogButton = New TaskDialogCommandLinkButton("Show &Results")
            finishedPage.Buttons.Add(showResultsButton)

            ' Enable the "Yes" button only when the checkbox is checked.
            Dim checkBox As TaskDialogVerificationCheckBox = initialPage.Verification
            AddHandler checkBox.CheckedChanged,
            Sub(sender, e)
                initialButtonYes.Enabled = checkBox.Checked
            End Sub

            ' When the user clicks "Yes", navigate to the second page.
            AddHandler initialButtonYes.Click,
            Sub(sender, e)
                ' Navigate to the "In Progress" page that displays the
                ' current progress of the background work.
                initialPage.Navigate(inProgressPage)

                ' NOTE: When you implement a "In Progress" page that represents
                ' background work that is done e.g. by a separate thread/task,
                ' which eventually calls Control.Invoke()/BeginInvoke() when
                ' its work is finished in order to navigate or update the dialog,
                ' then DO NOT start that work here already (directly after
                ' setting the Page property). Instead, start the work in the
                ' TaskDialogPage.Created event of the new page.
                '
                ' See comments in the code sample in https://github.com/dotnet/winforms/issues/146
                ' for more information.
            End Sub

            ' Simulate work by starting an async operation from which we are updating the
            ' progress bar and the expander with the current status.
            ' Note: VB.NET doesn't support 'await foreach' and async methods returning
            ' IAsyncEnumerable<T> as in C#, so we use a callback instead.
            Dim StreamBackgroundOperationProgressAsync = New Func(Of Action(Of Integer), Task)(
            Async Function(callback) As Task
                ' Note: The code here will run in the GUI thread - use
                ' "Await Task.Run(...)" to schedule CPU-intensive operations in a
                ' worker thread.

                ' Wait a bit before reporting the first progress.
                Await Task.Delay(2800)

                For index As Integer = 0 To 100 Step 4
                    ' Report the progress.
                    callback(index)

                    ' Wait a bit to simulate work.
                    Await Task.Delay(200)
                Next
            End Function)

            AddHandler inProgressPage.Created,
            Async Sub(s, e)
                ' Run the background operation and iterate over the streamed values to update
                ' the progress. Because we call the async method from the GUI thread,
                ' it will use this thread's synchronization context to run the continuations,
                ' so we don't need to use Control.[Begin]Invoke() to schedule the callbacks.
                Dim progressBar = inProgressPage.ProgressBar

                Await StreamBackgroundOperationProgressAsync(
                    Sub(progressValue)
                        ' When we display the first progress, switch the marquee progress bar
                        ' to a regular one.
                        If progressBar.State = TaskDialogProgressBarState.Marquee Then
                            progressBar.State = TaskDialogProgressBarState.Normal
                        End If


                        progressBar.Value = progressValue
                        inProgressPage.Expander.Text = $"Progress: {progressValue} %"

                    End Sub)

                ' Work Is finished, so navigate to the third page.
                inProgressPage.Navigate(finishedPage)
            End Sub

            ' Show the dialog (modeless).
            Dim result As TaskDialogButton = TaskDialog.ShowDialog(initialPage)

            If result = showResultsButton Then
                Debug.WriteLine("Showing Results!")
            End If

        End Sub

    End Class
End Namespace