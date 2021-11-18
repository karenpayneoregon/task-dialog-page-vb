<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.DoNotShowAgainButton = New System.Windows.Forms.Button()
        Me.AutoCloseButton = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ShowAgainCheckBox = New System.Windows.Forms.CheckBox()
        Me.RetryLabel = New System.Windows.Forms.Label()
        Me.AskQuestionButton = New System.Windows.Forms.Button()
        Me.MultiDialogButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'DoNotShowAgainButton
        '
        Me.DoNotShowAgainButton.Location = New System.Drawing.Point(22, 12)
        Me.DoNotShowAgainButton.Name = "DoNotShowAgainButton"
        Me.DoNotShowAgainButton.Size = New System.Drawing.Size(150, 23)
        Me.DoNotShowAgainButton.TabIndex = 0
        Me.DoNotShowAgainButton.Text = "Do not show again"
        Me.DoNotShowAgainButton.UseVisualStyleBackColor = True
        '
        'AutoCloseButton
        '
        Me.AutoCloseButton.Location = New System.Drawing.Point(22, 82)
        Me.AutoCloseButton.Name = "AutoCloseButton"
        Me.AutoCloseButton.Size = New System.Drawing.Size(150, 23)
        Me.AutoCloseButton.TabIndex = 1
        Me.AutoCloseButton.Text = "Auto close"
        Me.AutoCloseButton.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Location = New System.Drawing.Point(178, 12)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(144, 64)
        Me.ListBox1.TabIndex = 2
        '
        'ShowAgainCheckBox
        '
        Me.ShowAgainCheckBox.AutoSize = True
        Me.ShowAgainCheckBox.Location = New System.Drawing.Point(22, 41)
        Me.ShowAgainCheckBox.Name = "ShowAgainCheckBox"
        Me.ShowAgainCheckBox.Size = New System.Drawing.Size(55, 19)
        Me.ShowAgainCheckBox.TabIndex = 3
        Me.ShowAgainCheckBox.Text = "Show"
        Me.ShowAgainCheckBox.UseVisualStyleBackColor = True
        '
        'RetryLabel
        '
        Me.RetryLabel.AutoSize = True
        Me.RetryLabel.Location = New System.Drawing.Point(188, 85)
        Me.RetryLabel.Name = "RetryLabel"
        Me.RetryLabel.Size = New System.Drawing.Size(39, 15)
        Me.RetryLabel.TabIndex = 4
        Me.RetryLabel.Text = "Result"
        '
        'AskQuestionButton
        '
        Me.AskQuestionButton.Location = New System.Drawing.Point(22, 111)
        Me.AskQuestionButton.Name = "AskQuestionButton"
        Me.AskQuestionButton.Size = New System.Drawing.Size(150, 23)
        Me.AskQuestionButton.TabIndex = 5
        Me.AskQuestionButton.Text = "Question"
        Me.AskQuestionButton.UseVisualStyleBackColor = True
        '
        'MultiDialogButton
        '
        Me.MultiDialogButton.Location = New System.Drawing.Point(22, 140)
        Me.MultiDialogButton.Name = "MultiDialogButton"
        Me.MultiDialogButton.Size = New System.Drawing.Size(150, 23)
        Me.MultiDialogButton.TabIndex = 6
        Me.MultiDialogButton.Text = "Multi-Page dialog"
        Me.MultiDialogButton.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(331, 174)
        Me.Controls.Add(Me.MultiDialogButton)
        Me.Controls.Add(Me.AskQuestionButton)
        Me.Controls.Add(Me.RetryLabel)
        Me.Controls.Add(Me.ShowAgainCheckBox)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.AutoCloseButton)
        Me.Controls.Add(Me.DoNotShowAgainButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TaskDialog"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DoNotShowAgainButton As Button
    Friend WithEvents AutoCloseButton As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ShowAgainCheckBox As CheckBox
    Friend WithEvents RetryLabel As Label
    Friend WithEvents AskQuestionButton As Button
    Friend WithEvents MultiDialogButton As Button
End Class
