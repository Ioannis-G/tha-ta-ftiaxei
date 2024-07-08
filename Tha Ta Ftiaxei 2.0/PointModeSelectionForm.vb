Public Class PointModeSelectionForm
    Public ReadOnly Property SelectedMode() As String
        Get
            Return comboBoxModes.SelectedItem.ToString()
        End Get
    End Property

    Private Sub ModeSelectionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        comboBoxModes.Items.AddRange(New String() {"Text Label Mode", "Symbol Mode"})
        comboBoxModes.SelectedIndex = -1 ' No default selection
    End Sub

    Private Sub comboBoxModes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBoxModes.SelectedIndexChanged
        buttonOK.Enabled = comboBoxModes.SelectedIndex <> -1 ' Enable OK button if a mode is selected
    End Sub

    Private Sub buttonOK_Click(sender As Object, e As EventArgs) Handles buttonOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ModeSelectionFrom_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult <> DialogResult.OK Then
            Me.DialogResult = DialogResult.Cancel 'Ensure the dialog result is Cancel if the form is closed without OK button
        End If
    End Sub
End Class