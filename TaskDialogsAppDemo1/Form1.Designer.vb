<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.YesNoCancelQuestionButton = New System.Windows.Forms.Button()
        Me.QuestionSelectiveButton1 = New System.Windows.Forms.Button()
        Me.QuestionSelectiveButton2 = New System.Windows.Forms.Button()
        Me.YesRadioButton = New System.Windows.Forms.RadioButton()
        Me.NoRadioButton = New System.Windows.Forms.RadioButton()
        Me.SimpleQuestionButton = New System.Windows.Forms.Button()
        Me.QuestionTextBox = New System.Windows.Forms.TextBox()
        Me.InformationalButton = New System.Windows.Forms.Button()
        Me.ErrorDialogButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'YesNoCancelQuestionButton
        '
        Me.YesNoCancelQuestionButton.Location = New System.Drawing.Point(26, 12)
        Me.YesNoCancelQuestionButton.Name = "YesNoCancelQuestionButton"
        Me.YesNoCancelQuestionButton.Size = New System.Drawing.Size(168, 23)
        Me.YesNoCancelQuestionButton.TabIndex = 0
        Me.YesNoCancelQuestionButton.Text = "Question (Yes,No,Cancel)"
        Me.YesNoCancelQuestionButton.UseVisualStyleBackColor = True
        '
        'QuestionSelectiveButton1
        '
        Me.QuestionSelectiveButton1.Location = New System.Drawing.Point(26, 47)
        Me.QuestionSelectiveButton1.Name = "QuestionSelectiveButton1"
        Me.QuestionSelectiveButton1.Size = New System.Drawing.Size(168, 23)
        Me.QuestionSelectiveButton1.TabIndex = 1
        Me.QuestionSelectiveButton1.Text = "Question (buttons)"
        Me.QuestionSelectiveButton1.UseVisualStyleBackColor = True
        '
        'QuestionSelectiveButton2
        '
        Me.QuestionSelectiveButton2.Location = New System.Drawing.Point(26, 82)
        Me.QuestionSelectiveButton2.Name = "QuestionSelectiveButton2"
        Me.QuestionSelectiveButton2.Size = New System.Drawing.Size(168, 23)
        Me.QuestionSelectiveButton2.TabIndex = 2
        Me.QuestionSelectiveButton2.Text = "Question (button order)"
        Me.QuestionSelectiveButton2.UseVisualStyleBackColor = True
        '
        'YesRadioButton
        '
        Me.YesRadioButton.AutoSize = True
        Me.YesRadioButton.Location = New System.Drawing.Point(213, 82)
        Me.YesRadioButton.Name = "YesRadioButton"
        Me.YesRadioButton.Size = New System.Drawing.Size(42, 19)
        Me.YesRadioButton.TabIndex = 3
        Me.YesRadioButton.Text = "Yes"
        Me.YesRadioButton.UseVisualStyleBackColor = True
        '
        'NoRadioButton
        '
        Me.NoRadioButton.AutoSize = True
        Me.NoRadioButton.Checked = True
        Me.NoRadioButton.Location = New System.Drawing.Point(261, 82)
        Me.NoRadioButton.Name = "NoRadioButton"
        Me.NoRadioButton.Size = New System.Drawing.Size(41, 19)
        Me.NoRadioButton.TabIndex = 4
        Me.NoRadioButton.TabStop = True
        Me.NoRadioButton.Text = "No"
        Me.NoRadioButton.UseVisualStyleBackColor = True
        '
        'SimpleQuestionButton
        '
        Me.SimpleQuestionButton.Location = New System.Drawing.Point(26, 117)
        Me.SimpleQuestionButton.Name = "SimpleQuestionButton"
        Me.SimpleQuestionButton.Size = New System.Drawing.Size(168, 23)
        Me.SimpleQuestionButton.TabIndex = 5
        Me.SimpleQuestionButton.Text = "Question (No defaul button)"
        Me.SimpleQuestionButton.UseVisualStyleBackColor = True
        '
        'QuestionTextBox
        '
        Me.QuestionTextBox.Location = New System.Drawing.Point(202, 117)
        Me.QuestionTextBox.Name = "QuestionTextBox"
        Me.QuestionTextBox.PlaceholderText = "Question text"
        Me.QuestionTextBox.Size = New System.Drawing.Size(178, 23)
        Me.QuestionTextBox.TabIndex = 6
        '
        'InformationalButton
        '
        Me.InformationalButton.Location = New System.Drawing.Point(26, 152)
        Me.InformationalButton.Name = "InformationalButton"
        Me.InformationalButton.Size = New System.Drawing.Size(168, 23)
        Me.InformationalButton.TabIndex = 7
        Me.InformationalButton.Text = "Informational"
        Me.InformationalButton.UseVisualStyleBackColor = True
        '
        'ErrorDialogButton
        '
        Me.ErrorDialogButton.Location = New System.Drawing.Point(26, 186)
        Me.ErrorDialogButton.Name = "ErrorDialogButton"
        Me.ErrorDialogButton.Size = New System.Drawing.Size(168, 23)
        Me.ErrorDialogButton.TabIndex = 8
        Me.ErrorDialogButton.Text = "Error dialog"
        Me.ErrorDialogButton.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 226)
        Me.Controls.Add(Me.ErrorDialogButton)
        Me.Controls.Add(Me.InformationalButton)
        Me.Controls.Add(Me.QuestionTextBox)
        Me.Controls.Add(Me.SimpleQuestionButton)
        Me.Controls.Add(Me.NoRadioButton)
        Me.Controls.Add(Me.YesRadioButton)
        Me.Controls.Add(Me.QuestionSelectiveButton2)
        Me.Controls.Add(Me.QuestionSelectiveButton1)
        Me.Controls.Add(Me.YesNoCancelQuestionButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Using support library"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents YesNoCancelQuestionButton As Button
    Friend WithEvents QuestionSelectiveButton1 As Button
    Friend WithEvents QuestionSelectiveButton2 As Button
    Friend WithEvents YesRadioButton As RadioButton
    Friend WithEvents NoRadioButton As RadioButton
    Friend WithEvents SimpleQuestionButton As Button
    Friend WithEvents QuestionTextBox As TextBox
    Friend WithEvents InformationalButton As Button
    Friend WithEvents ErrorDialogButton As Button
End Class
