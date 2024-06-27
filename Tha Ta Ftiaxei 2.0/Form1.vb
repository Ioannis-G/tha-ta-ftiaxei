Imports System
Imports System.Windows.Forms
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports System.Globalization
Public Class Form1
    'Open GeoJSON File Button Logic
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

    'Convert to DMS Function
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

        Return String.Format("{0}{1:00}.{2:00}.{3:00.000}:{4}{5:00}.{6:00}.{7:00.000}", latHemisphere, latDeg, latMin, latSec, lonHemisphere, lonDeg, lonMin, lonSec)
    End Function

    'Process GeoJSON File Method
    Private Sub ProcessGeoJSONFile(filePath As String)
        Dim jsonContent As String = File.ReadAllText(filePath)
        Dim geoJson As JObject = JObject.Parse(jsonContent)

        Dim features As JToken = geoJson("features")
        Dim containsPolygonOrMultiPolygon As Boolean = features.Any(Function(feature) feature("geometry")("type").ToString() = "Polygon" OrElse feature("geometry")("type").ToString() = "MultiPolygon")

        Dim polygonColor As String = String.Empty
        If containsPolygonOrMultiPolygon Then
            polygonColor = InputBox("Enter the color for the polygons:", "Polygon Color", "yourcolorhere")
            If String.IsNullOrWhiteSpace(polygonColor) Then
                MessageBox.Show("No color was entered. Processing will continue without color for polygons.", "Missing Color", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        Dim supportedTypeFound As Boolean = False
        Dim isFirstPolygon As Boolean = True

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

                Case "LineString"
                    supportedTypeFound = True
                    ProcessLineString(coordinates)

                Case "MultiLineString"
                    supportedTypeFound = True
                    ProcessLineString(coordinates, True)

                Case "Point", "MultiPoint"
                    supportedTypeFound = True
                    ProcessPoint(coordinates)

                Case Else
                    ' Unsupported type, do nothing
            End Select
        Next

        If Not supportedTypeFound Then
            MessageBox.Show("The selected file does not contain any of the supported geometry types. Please select a valid file.", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Button1_Click(Nothing, Nothing) 'Reopen file dialog
        End If
    End Sub

    'Copy to Clipboard Button Logic
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clipboard.SetText(TextBox1.Text)
    End Sub

    'Polygon Processing Method
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

    'LineString Processing Method
    Private Sub ProcessLineString(lineCoordinates As JToken, Optional isMultiLineString As Boolean = False)
        If isMultiLineString Then
            For Each line As JToken In lineCoordinates
                ProcessSingleLineString(line)
            Next
        Else
            ProcessSingleLineString(lineCoordinates)
        End If
    End Sub

    'LineString Processing Helper Method
    Private Sub ProcessSingleLineString(lineCoordinates As JToken)
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
    End Sub

    'Point Processing Method
    Private Sub ProcessPoint(point As JToken, Optional isMultiPoint As Boolean = False)
        If isMultiPoint Then
            For Each pt As JToken In point.Children()
                ProcessSinglePoint(pt)
            Next
        Else
            ProcessSinglePoint(point)
        End If
    End Sub

    'Point Processing Helper Method
    Private Sub ProcessSinglePoint(point As JToken)
        If point.HasValues AndAlso point(0).Type = JTokenType.Array Then
            ' Assuming the first element of the array is another array with at least two elements (longtitude and latitude).
            Dim coordinateArray As JArray = point(0)
            If coordinateArray.Count >= 2 Then
                Dim lon As Double = coordinateArray(0).ToObject(Of Double)()
                Dim lat As Double = coordinateArray(1).ToObject(Of Double)()
                Dim formattedCoord As String = ConverttoDMS(lat, lon)

                Dim label As String = String.Empty
                While String.IsNullOrWhiteSpace(label)
                    label = InputBox($"Enter the label {formattedCoord}", "Point Label", "yourlabelhere")
                    If String.IsNullOrWhiteSpace(label) Then
                        MessageBox.Show("No label was entered. Please try again.", "Label Required", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End While

                TextBox1.AppendText($"TEXT:{formattedCoord}:{label}" & Environment.NewLine)
            End If
        Else
            Throw New InvalidOperationException("No coordinates found or unexpected structure")
        End If

    End Sub

End Class
