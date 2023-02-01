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
            var repeats = array[x, y];
            var time = timeStart.AddDays(index++);

            for (int i = 0; i < repeats; i++) {
                commitTimes.Enqueue(time.AddMinutes(i));
            }
        }
    }
}

int commitCount = commitTimes.Count;

// Copy the basic files at the initial commit
{
    var initialTime = commitTimes.Dequeue();
    Directory.CreateDirectory("SuspiciousHistory");
    File.Copy("../../../.gitignore", "SuspiciousHistory/.gitignore");
    File.Copy("../../../SuspiciousHistory.csproj", "SuspiciousHistory/SuspiciousHistory.csproj");
    File.Copy("../../../SuspiciousHistory.sln", "SuspiciousHistory/SuspiciousHistory.sln");
    File.WriteAllText("SuspiciousHistory/Program.cs", "");
    RunCommand("git init");
    GitCommit("💡 Initial commit", initialTime);
}

var programContent = File.ReadAllText("../../../Program.cs");

// Incrementally build Program.cs
{
    var emojis = new[] {
        "🎉", "🍎", "⚙", "📁", "🔧", "🛠", "🤔", "🔥", "😎"
    };
    var messages = new[] {
        "Continues progress on the implemententation",
        "Adds various fixes",
        "Commit before breaking it",
        "Implements the upcoming feature",
        "Develops the program",
