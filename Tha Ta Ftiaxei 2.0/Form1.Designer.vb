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
        Dim Ellada_2_0 As PictureBox
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Button1 = New Button()
        Button2 = New Button()
        TextBox1 = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Ellada_2_0 = New PictureBox()
        CType(Ellada_2_0, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Ellada_2_0
        ' 
        Ellada_2_0.Image = CType(resources.GetObject("Ellada_2_0.Image"), Image)
        Ellada_2_0.Location = New Point(578, 370)
        Ellada_2_0.Name = "Ellada_2_0"
        Ellada_2_0.Size = New Size(228, 76)
        Ellada_2_0.SizeMode = PictureBoxSizeMode.StretchImage
        Ellada_2_0.TabIndex = 7
        Ellada_2_0.TabStop = False
        Ellada_2_0.WaitOnLoad = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(121, 395)
        Button1.Name = "Button1"
        Button1.Size = New Size(114, 41)
        Button1.TabIndex = 0
        Button1.Text = "Open GeoJSON File"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(364, 395)
        Button2.Name = "Button2"
        Button2.Size = New Size(100, 41)
        Button2.TabIndex = 1
        Button2.Text = "Copy to Clipboard"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(12, 12)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.ScrollBars = ScrollBars.Vertical
        TextBox1.Size = New Size(560, 331)
        TextBox1.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Blue
        Label1.Location = New Point(614, 12)
        Label1.Name = "Label1"
        Label1.Size = New Size(179, 29)
        Label1.TabIndex = 3
        Label1.Text = "Tha Ta Ftiaxei "
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(601, 62)
        Label2.MaximumSize = New Size(200, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(192, 136)
        Label2.TabIndex = 4
        Label2.Text = "This program allows you to process GeoJSON files, converting coordinates to DMS format and handling various geometry types, so as to assist with TopSky and GroundRadar map development. "
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(597, 229)
        Label3.MaximumSize = New Size(200, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(196, 51)
        Label3.TabIndex = 5
        Label3.Text = "To get started, click on 'Open File' and Select a supported GeoJSON file."
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial", 9.75F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.Gray
        Label4.Location = New Point(614, 311)
        Label4.Name = "Label4"
        Label4.Size = New Size(158, 32)
        Label4.TabIndex = 6
        Label4.Text = "Author: Ioannis Gaitaneris" & vbCrLf & "License: MIT"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(6F, 13F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(851, 470)
        Controls.Add(Ellada_2_0)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(TextBox1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.Fixed3D
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "Form1"
        Text = "Tha Ta Ftiaxei "
        CType(Ellada_2_0, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Ellada_2_0 As PictureBox

End Class
