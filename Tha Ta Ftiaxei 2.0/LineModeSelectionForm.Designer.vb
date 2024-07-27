<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LineModeSelectionForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LineModeSelectionForm))
        Me.comboBoxModes = New System.Windows.Forms.ComboBox()
        Me.buttonOK = New System.Windows.Forms.Button()
        Me.buttonCancel = New System.Windows.Forms.Button()
        Me.labelPrompt = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'comboBoxModes
        '
        Me.comboBoxModes.FormattingEnabled = True
        Me.comboBoxModes.Location = New System.Drawing.Point(84, 42)
        Me.comboBoxModes.Name = "comboBoxModes"
        Me.comboBoxModes.Size = New System.Drawing.Size(121, 21)
        Me.comboBoxModes.TabIndex = 0
        '
        'buttonOK
        '
        Me.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.buttonOK.Enabled = False
        Me.buttonOK.Location = New System.Drawing.Point(107, 79)
        Me.buttonOK.Name = "buttonOK"
        Me.buttonOK.Size = New System.Drawing.Size(75, 23)
        Me.buttonOK.TabIndex = 1
        Me.buttonOK.Text = "OK"
        Me.buttonOK.UseVisualStyleBackColor = True
        '
        'buttonCancel
        '
        Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonCancel.Location = New System.Drawing.Point(188, 79)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.Size = New System.Drawing.Size(75, 23)
        Me.buttonCancel.TabIndex = 2
        Me.buttonCancel.Text = "Cancel"
        Me.buttonCancel.UseVisualStyleBackColor = True
        '
        'labelPrompt
        '
        Me.labelPrompt.AutoSize = True
        Me.labelPrompt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelPrompt.Location = New System.Drawing.Point(80, 9)
        Me.labelPrompt.Name = "labelPrompt"
        Me.labelPrompt.Size = New System.Drawing.Size(125, 14)
        Me.labelPrompt.TabIndex = 3
        Me.labelPrompt.Text = "Please select a Mode:"
        Me.labelPrompt.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LineModeSelectionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 123)
        Me.Controls.Add(Me.labelPrompt)
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonOK)
        Me.Controls.Add(Me.comboBoxModes)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LineModeSelectionForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Line Processing Mode"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents comboBoxModes As ComboBox
    Private WithEvents buttonOK As Button
    Private WithEvents buttonCancel As Button
    Private WithEvents labelPrompt As Label
End Class
