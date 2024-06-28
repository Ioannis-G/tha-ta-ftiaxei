# Tha Ta Ftiaxei 

Tha ta Ftiaxei is a rather simple tool that processes GeoJSON Files, converts geometry coordinates to DMS format and, depending on the geometry type, formats them in a way to assist mainly with TopSky and GroundRadar Map Development.

### Prerequisites

1. .NET 8.0 (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Compatible GeoJSON File(s). Supported features are "Polygon", "MultiPolygon", "LineString", "MultiLineString", "Point" and "MultiPoint".
3. Optionally, GIS software to create the GeoJSON files needed for map development.

## Brief Explanation of Tool Operation
The UI of the tool is really straightforward and consists of a TextBox where the output of each operation is printed, one Button to open the GeoJSON file to be processed and a second Button to copy the contents of the aforementioned TextBox to the clipboard.

Behaviour of the tool depends on the feature being processed:

### I. "Polygon" and "MultiPolygon" Features
1. Before processing any geometries, it is checked whether the GeoJSON file contains any Polygon or MultiPolygon features. If so, it prompts the user to enter a color for these polygons. This color information is intended to be used for all polygons defined, and is attached before each one except for the first one (or only one). That way, it is marked when a different polygon is being defined automatically.
2. Each coordinate pair which defines a point of these polygons is converted to a formatted string using a function which converts the latitude and longitude values to Degrees, Minutes, and Seconds format.
3. Each formatted coordinate string is then outputed to the TextBox with the prefix "COORD:"

Output should look like this: 

![image](https://github.com/Ioannis-G/tha-ta-ftiaxei/assets/113134133/f3968d0a-7d0e-4bd8-9adc-893040ce626a)

### II. "LineString" and "MultiLineString" Features
1. For both LineString and MultiLineString geometries, the coordinates of a LineString (whether from a single LineString or one within a MultiLineString) are processed and each pair of points is treated as the start and end of a line segment respectively.
2. For each line segment, the latitude and longitude of the start and end points are converted to Degrees, Minutes, and Seconds format using a function.
3. This information is then formated as "LINE:" segments, including the DMS-formatted start and end coordinates, and is then outputed to the TextBox.

Output should look like this:

![image](https://github.com/Ioannis-G/tha-ta-ftiaxei/assets/113134133/956582c6-d738-468d-a47b-0d1a77861d8f)

### III. "Point" and "MultiPoint" Features
**Note:** Points are treated by this tool as "TEXT:" labels.
1. While being processed, the coordinates of each point are converted to Degrees, Minutes, and Seconds format using a function. The user is prompted to enter a label for each point.
2. After the user provides a label for a point, a string that includes the DMS coordinates and the label of it is formatted and is then outputed to the TextBox.
3. This process is repeated until all points have been processed.

Output should look like this:

![image](https://github.com/Ioannis-G/tha-ta-ftiaxei/assets/113134133/17b2ba0d-1dad-44ee-97dd-65af3073d038)

## Closing Remarks

This is a personal project that I've uploaded to GitHub in case anyone finds it useful. As such, I maintain it to a level that satisfies my needs. However, if you wish to report a bug, suggest a feature, or propose a fix, please feel free to do so by opening an issue or submitting a pull request (all of which are subject to review). Thank you for visiting my project!

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details


