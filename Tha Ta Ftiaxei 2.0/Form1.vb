Imports System
Imports System.Windows.Forms
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports System.Globalization
Imports System.Media
Public Class Form1

    ' Sound Load Event
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PlayStartupSound()
    End Sub
    Private Sub PlayStartupSound()
        Try
            Dim player As New SoundPlayer("startup.wav")
            player.Play()
        Catch ex As Exception
            'Ignore any exceptions
        End Try
    End Sub

    ' Open GeoJSON File Button Logic
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim openFileDialog As OpenFileDialog = New OpenFileDialog With {
            .Filter = "GeoJSON files (*.geojson)|*.geojson|All files (*.*)|*.*",
            .RestoreDirectory = True
        }

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            TextBox1.Clear()

            Dim filepath As String = openFileDialog.FileName
            ProcessGeoJSONFile(filepath)
        End If
    End Sub

    ' Convert to DMS Function
    Private Function ConverttoDMS(lat As Double, lon As Double) As String
        Dim latHemisphere As String = If(lat >= 0, "N", "S")
        Dim lonHemisphere As String = If(lon >= 0, "E", "W")

        lat = Math.Abs(lat)
        lon = Math.Abs(lon)

        Dim latDeg As Integer = Math.Floor(lat)
        Dim lonDeg As Integer = Math.Floor(lon)

        Dim latMin As Integer = Math.Floor((lat - latDeg) * 60)
        Dim lonMin As Integer = Math.Floor((lon - lonDeg) * 60)

        Dim latSec As Double = ((lat - latDeg - latMin / 60) * 3600)
        Dim lonSec As Double = ((lon - lonDeg - lonMin / 60) * 3600)

        Return String.Format("{0}{1:000}.{2:00}.{3:00.000}:{4}{5:000}.{6:00}.{7:00.000}", latHemisphere, latDeg, latMin, latSec, lonHemisphere, lonDeg, lonMin, lonSec)
    End Function

    ' Process GeoJSON File Method
    Private Sub ProcessGeoJSONFile(filePath As String)
        Dim jsonContent As String = File.ReadAllText(filePath)
        Dim geoJson As JObject = JObject.Parse(jsonContent)

        Dim features As JToken = geoJson("features")
        Dim containsPolygonOrMultiPolygon As Boolean = features.Any(Function(feature) feature("geometry")("type").ToString() = "Polygon" OrElse feature("geometry")("type").ToString() = "MultiPolygon")
        Dim containsPointOrMultiPoint As Boolean = features.Any(Function(feature) feature("geometry")("type").ToString() = "Point" OrElse feature("geometry")("type").ToString() = "MultiPoint")
        Dim containsLineStringOrMultiLineString As Boolean = features.Any(Function(feature) feature("geometry")("type").ToString() = "LineString" OrElse feature("geometry")("type").ToString() = "MultiLineString")

        Dim polygonColor As String = String.Empty
        Dim isFirstPolygon As Boolean = True
        Dim pointMode As String = String.Empty
        Dim symbolType As String = String.Empty
        Dim lineMode As String = String.Empty
        Dim lineNumber As Integer = 1

        If containsPolygonOrMultiPolygon Then
            Dim validColorEntered As Boolean = False
            While Not validColorEntered
                polygonColor = InputBox("Enter the color for the polygons:", "Polygon Color", "yourcolorhere")
                If String.IsNullOrEmpty(polygonColor) Then ' Treats both cancellation and empty input similarly
                    Dim result As DialogResult = MessageBox.Show("Do you want to cancel the operation?", "Cancel?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                    If result = DialogResult.Yes Then
                        Exit Sub ' User confirmed cancellation
                    Else
                        ' User chose not to cancel; the loop will prompt for input again.
                    End If
                Else
                    validColorEntered = True
                End If
            End While
        End If

        If containsPointOrMultiPoint Then
            pointMode = ShowPointModeSelectionBox()
            If pointMode Is Nothing Then
                MessageBox.Show("Operation canceled by the user.", "Operation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub ' User canceled the operation
            End If

            If pointMode = "Symbol Mode" Then
                symbolType = InputBox("Enter point symbol type:", "Symbol Type", "yoursymboltypehere")
                If String.IsNullOrEmpty(symbolType) Then
                    MessageBox.Show("Symbol type cannot be empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub ' User did not provide a valid symbol type
                End If
            End If
        End If

        If containsLineStringOrMultiLineString Then
            lineMode = ShowLineModeSelectionBox()
            If lineMode Is Nothing Then
                MessageBox.Show("Operation canceled by the user.", "Operation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub ' User canceled the operation
            End If
        End If

        Dim supportedTypeFound As Boolean = False

        For Each feature In features
            Dim geometry As JToken = feature("geometry")
            Dim type As String = geometry("type").ToString()
            Dim coordinates As JToken = geometry("coordinates")

            Select Case type
                Case "Polygon", "MultiPolygon"
                    supportedTypeFound = True
                    If Not String.IsNullOrWhiteSpace(polygonColor) Then
                        If Not isFirstPolygon Then
                            TextBox1.AppendText("COLOR:" & polygonColor & Environment.NewLine)
                        End If
                        ProcessPolygon(coordinates)
                        isFirstPolygon = False
                    End If

                Case "LineString", "MultiLineString"
                    supportedTypeFound = True
                    ProcessLineString(coordinates, lineMode, lineNumber, type = "MultiLineString")

                Case "Point", "MultiPoint"
                    supportedTypeFound = True
                    ProcessPoint(coordinates, pointMode, type = "MultiPoint", symbolType)

                Case Else
                    ' Unsupported type, do nothing
            End Select
        Next

        If Not supportedTypeFound Then
            MessageBox.Show("The selected file does not contain any of the supported geometry types. Please select a valid file.", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Button1_Click(Nothing, Nothing) ' Reopen file dialog
        End If
    End Sub

    ' Copy to Clipboard Button Logic
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clipboard.SetText(TextBox1.Text)
    End Sub

    ' Polygon Processing Method
    Private Sub ProcessPolygon(polygon As JToken)
        For Each ring As JToken In polygon
            For Each coord As JToken In ring.First
                Dim lon As Double = coord(0).ToObject(Of Double)()
                Dim lat As Double = coord(1).ToObject(Of Double)()
                Dim formattedCoord As String = "COORD:" & ConverttoDMS(lat, lon)
                TextBox1.AppendText(formattedCoord & Environment.NewLine)
            Next
        Next
    End Sub

    ' Selection Box Method for LineString Processing Mode
    Private Function ShowLineModeSelectionBox() As String
        Using LineModeSelectionForm As New LineModeSelectionForm()
            If LineModeSelectionForm.ShowDialog() = DialogResult.OK Then
                Return LineModeSelectionForm.SelectedMode
            Else
                Return Nothing
            End If
        End Using
    End Function

    ' LineString Processing Method
    Private Sub ProcessLineString(lineCoordinates As JToken, mode As String, ByRef lineNumber As Integer, Optional isMultiLineString As Boolean = False)
        If isMultiLineString Then
            For Each line As JToken In lineCoordinates
                If mode = "ESE GND-Net Mode" Then
                    TextBox1.AppendText($";{lineNumber}-----------------------------------------" & Environment.NewLine)
                End If
                ProcessSingleLineString(line, mode)
                lineNumber += 1
            Next
        Else
            If mode = "ESE GND-Net Mode" Then
                TextBox1.AppendText($";{lineNumber}-----------------------------------------" & Environment.NewLine)
            End If
            ProcessSingleLineString(lineCoordinates, mode)
            lineNumber += 1
        End If
    End Sub

    ' LineString Processing Helper Method
    Private Sub ProcessSingleLineString(lineCoordinates As JToken, mode As String)
        If mode = "TopSky Line Mode" Then
            For i As Integer = 0 To lineCoordinates.Count - 2
                Dim startCoord As JToken = lineCoordinates(i)
                Dim endCoord As JToken = lineCoordinates(i + 1)

                Dim startLon As Double = startCoord(0).ToObject(Of Double)()
                Dim startLat As Double = startCoord(1).ToObject(Of Double)()
                Dim endLon As Double = endCoord(0).ToObject(Of Double)()
                Dim endLat As Double = endCoord(1).ToObject(Of Double)()

                Dim formattedStartCoord As String = ConverttoDMS(startLat, startLon)
                Dim formattedEndCoord As String = ConverttoDMS(endLat, endLon)

                TextBox1.AppendText($"LINE:{formattedStartCoord}:{formattedEndCoord}" & Environment.NewLine)
            Next
        ElseIf mode = "ESE GND-Net Mode" Then
            For Each coord As JToken In lineCoordinates
                Dim lon As Double = coord(0).ToObject(Of Double)()
                Dim lat As Double = coord(1).ToObject(Of Double)()
                Dim formattedCoord As String = "COORD:" & ConverttoDMS(lat, lon)
                TextBox1.AppendText(formattedCoord & Environment.NewLine)
            Next
        End If
    End Sub

    ' Selection Box Method for Point Processing Mode
    Private Function ShowPointModeSelectionBox() As String
        Using PointModeSelectionForm As New PointModeSelectionForm()
            If PointModeSelectionForm.ShowDialog() = DialogResult.OK Then
                Return PointModeSelectionForm.SelectedMode
            Else
                Return Nothing
            End If
        End Using
    End Function

    ' Point Processing Method
    Private Sub ProcessPoint(point As JToken, mode As String, Optional isMultiPoint As Boolean = False, Optional symboltype As String = "")
        If mode = "Symbol Mode" Then
            If isMultiPoint Then
                For Each pt As JToken In point
                    ProcessSinglePointAsSymbol(pt, symboltype)
                Next
            Else
                ProcessSinglePointAsSymbol(point, symboltype)
            End If
        Else
            If isMultiPoint Then
                For Each pt As JToken In point
                    ProcessSinglePointAsLabel(pt)
                Next
            Else
                ProcessSinglePointAsLabel(point)
            End If
        End If
    End Sub

    ' Point Processing As Text Label Helper Method
    Private Sub ProcessSinglePointAsLabel(point As JToken)
        If point.Type = JTokenType.Array AndAlso point.Count >= 2 Then
            Dim lon As Double = point(0).ToObject(Of Double)()
            Dim lat As Double = point(1).ToObject(Of Double)()
            Dim formattedCoord As String = ConverttoDMS(lat, lon)

            Dim label As String = String.Empty
            While String.IsNullOrEmpty(label)
                label = InputBox($"Enter the label for {formattedCoord}", "Point Label", "yourlabelhere")
                If String.IsNullOrEmpty(label) Then ' Treats both cancellation and empty input similarly
                    Dim result As DialogResult = MessageBox.Show("Do you want to cancel the operation?", "Cancel?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                    If result = DialogResult.Yes Then
                        Exit Sub 'User confirmed cancellation
                    Else
                        ' User chose not to cancel; the loop will prompt for input again.
                    End If
                End If
            End While

            TextBox1.AppendText($"TEXT:{formattedCoord}:{label}" & Environment.NewLine)
        Else
            Throw New InvalidOperationException("No coordinates found or unexpected structure")
        End If
    End Sub

    ' Point Processing As Symbol Helper Method
    Private Sub ProcessSinglePointAsSymbol(point As JToken, symbolType As String)
        If point.Type = JTokenType.Array AndAlso point.Count >= 2 Then
            Dim lon As Double = point(0).ToObject(Of Double)()
            Dim lat As Double = point(1).ToObject(Of Double)()
            Dim formattedCoord As String = ConverttoDMS(lat, lon)

            Dim symbolName As String = String.Empty
            While String.IsNullOrEmpty(symbolName)
                symbolName = InputBox($"Enter the symbol name for {formattedCoord}", "Symbol Name", "yoursymbolnamehere")
                If String.IsNullOrEmpty(symbolName) Then ' Treats both cancellation and empty input similarly
                    Dim result As DialogResult = MessageBox.Show("Do you want to cancel the operation?", "Cancel?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                    If result = DialogResult.Yes Then
                        Exit Sub ' User confirmed cancellation
                    Else
                        'user chose not to cancel; the loop will prompt for input again.
                    End If
                End If
            End While

            TextBox1.AppendText($"SYMBOL:{symbolType}:{formattedCoord}:{symbolName}" & Environment.NewLine)
        Else
            Throw New InvalidOperationException("No coordinates found or unexpected structure")
        End If
    End Sub
End Class
