<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PointModeSelectionForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PointModeSelectionForm))
        comboBoxModes = New ComboBox()
        buttonOK = New Button()
        labelPrompt = New Label()
        SuspendLayout()
        ' 
        ' comboBoxModes
        ' 
        comboBoxModes.FormattingEnabled = True
        comboBoxModes.Location = New Point(84, 42)
        comboBoxModes.Name = "comboBoxModes"
        comboBoxModes.Size = New Size(121, 21)
        comboBoxModes.TabIndex = 0
        ' 
        ' buttonOK
        ' 
        buttonOK.DialogResult = DialogResult.OK
        buttonOK.Enabled = False
        buttonOK.Location = New Point(107, 79)
        buttonOK.Name = "buttonOK"
        buttonOK.Size = New Size(75, 23)
        buttonOK.TabIndex = 1
        buttonOK.Text = "OK"
        buttonOK.UseVisualStyleBackColor = True
        ' 
        ' labelPrompt
        ' 
        labelPrompt.AutoSize = True
        labelPrompt.Font = New Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        labelPrompt.Location = New Point(80, 9)
        labelPrompt.Name = "labelPrompt"
        labelPrompt.Size = New Size(125, 14)
        labelPrompt.TabIndex = 2
        labelPrompt.Text = "Please select a Mode:"
        labelPrompt.TextAlign = ContentAlignment.TopCenter
        ' 
        ' PointModeSelectionForm
        ' 
        AutoScaleDimensions = New SizeF(6F, 13F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(292, 123)
        Controls.Add(labelPrompt)
        Controls.Add(buttonOK)
        Controls.Add(comboBoxModes)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "PointModeSelectionForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Select Point Processing Mode"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Private WithEvents comboBoxModes As ComboBox
    Private WithEvents buttonOK As Button
    Private WithEvents labelPrompt As Label
End Class
