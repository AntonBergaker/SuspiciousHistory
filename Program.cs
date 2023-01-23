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
    var line = lin