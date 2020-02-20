using ApacheParser.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ApacheParser
{
    class ParseInfo
    {
        public Match RegExMatch { get; set; }
        public int Offset { get; set; }
    }

    class RegExInfo
    {
        public string Pattern { get; set; }
        public int Offset { get; set; }
    }

    internal static class ApacheParseHelper
    {
        internal static ParseInfo GetRegexMatch(string line)
        {
            string commonPostfixPattern = "\\[([\\w:/]+\\s[+\\-]\\d{4})\\] \"(.+?)\" (\\d{3}) (\\d+) \"([^\"]+)\" \"([^\"]+)\" (\\S+) (\\S+) (\\S+)";
            string logEntryPatternWithFourIP = "^([\\d.]+,) ([\\d.]+,) ([\\d.]+,) ([\\d.]+) (\\S+) (\\S+) " + commonPostfixPattern;
            string logEntryPatternWithThreeIP = "^([\\d.]+,) ([\\d.]+,) ([\\d.]+) (\\S+) (\\S+) " + commonPostfixPattern;
            string logEntryPatternWithTwoIP = "^([\\d.]+,) ([\\d.]+) (\\S+) (\\S+) " + commonPostfixPattern;
            string logEntryPatternWithoutClientIP = "^(\\S+) (\\S+) (\\S+) " + commonPostfixPattern;

            // use most common pattern first -- This is a pattern with 3 IP addresses at the beginning of the line
            Match regexMatch = Regex.Match(line, logEntryPatternWithThreeIP);
            if (regexMatch.Groups.Count > 1)
                return new ParseInfo { RegExMatch = regexMatch, Offset = 2 };

            // store the potential regex patterns in the order in which we are likely to need them
            var patterns = new List<RegExInfo> { new RegExInfo {Pattern=logEntryPatternWithTwoIP, Offset =1 }, new RegExInfo {Pattern=logEntryPatternWithFourIP, Offset=3 } };

            if (line.StartsWith("-"))
            {
                // if the line starts with a - we know there are not going to be any IP addresses - we know exactly which regex we will need.
                regexMatch = Regex.Match(line, logEntryPatternWithoutClientIP);

                return new ParseInfo { RegExMatch = regexMatch, Offset = 0 };
            }

            foreach (var regExInfo in patterns)
            {
                regexMatch = Regex.Match(line, regExInfo.Pattern);
                if (regexMatch.Groups.Count > 1)
                    return new ParseInfo { RegExMatch = regexMatch, Offset = regExInfo.Offset };
            }

            return new ParseInfo { Offset = -1 };
        }

        internal static List<LogLine> LoadLogfile(string logFile)
        {

            var rawLines = System.IO.File.ReadAllLines(logFile);

            var lines = new List<LogLine>();

            foreach (var line in rawLines)
            {
                var logLine = new LogLine(line);
                lines.Add(logLine);
            }

            return lines;
        }

    }
}
