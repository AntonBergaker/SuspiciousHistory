using System.Diagnostics;
using System.Globalization;

var bean = """
    0000
    0111
    11AA
    1111
    1111
    0111
    0101
    """;

// Put the entries in an array
var array = new int[4, 7];
var lines = bean.Split("\n");
for (int y = 0; y < lines.Length; y++) {
    var line = lines[y];
    for (int x=0; x<line.Length ;x++) {
        array[x, y] = int.Parse(line[x].ToString(), NumberStyles.HexNumber);
    }
}

// Create the times we need to insert
var timeStart = DateTime.Parse(args[0]);

var commitTimes = new Queue<DateTime>();

{
    int index = 0;
    for (int x = 0; x < array.GetLength(0); x++) {
        for (int y = 0; y < array.GetLength(1); y++) {
 