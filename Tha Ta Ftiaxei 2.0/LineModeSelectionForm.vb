Public Class LineModeSelectionForm
    Public ReadOnly Property SelectedMode() As String
        Get
            Return comboBoxModes.SelectedItem.ToString()
        End Get
    End Property

    Private Sub LineModeSelectionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        comboBoxModes.Items.AddRange(New String() {"TopSky Line Mode", "ESE GND-Net Mode"})
        comboBoxModes.SelectedIndex = -1 ' No default selection
    End Sub

    Private Sub comboBoxModes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBoxModes.SelectedIndexChanged
        buttonOK.Enabled = comboBoxModes.SelectedIndex <> -1 ' Enable OK button if a mode is selected
    End Sub

    Private Sub buttonOK_Click(sender As Object, e As EventArgs) Handles buttonOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub LineModeSelectionForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult <> DialogResult.OK Then
            Me.DialogResult = DialogResult.Cancel
        End If
    End Sub
End Class