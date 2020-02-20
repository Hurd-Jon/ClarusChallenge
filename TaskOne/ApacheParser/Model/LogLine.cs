using System;
using System.Globalization;

namespace ApacheParser.Model
{
    class LogLine
    {

        public LogLine(string line)
        {

            ParseInfo regexMatch = ApacheParseHelper.GetRegexMatch(line);

            ForwardedFor = regexMatch.RegExMatch.Groups[1].Value;
            RemoteLogName = regexMatch.RegExMatch.Groups[2 + regexMatch.Offset].Value;
            RemoteUser = regexMatch.RegExMatch.Groups[3 + regexMatch.Offset].Value;
            RequestTime = DateTime.ParseExact(regexMatch.RegExMatch.Groups[4 + regexMatch.Offset].Value.Replace(" +0000",""), "dd/MMM/yyyy:HH:mm:ss", new CultureInfo("en-IE"));
            URI = regexMatch.RegExMatch.Groups[8 + regexMatch.Offset].Value;
            Status = int.Parse(regexMatch.RegExMatch.Groups[6 + regexMatch.Offset].Value);
            ResponseSize = int.Parse(regexMatch.RegExMatch.Groups[7 + regexMatch.Offset].Value);
            Referrer = regexMatch.RegExMatch.Groups[10 + regexMatch.Offset].Value;
            if (!(regexMatch.RegExMatch.Groups[11 + regexMatch.Offset].Value == "-"))
                PHPTime = int.Parse(regexMatch.RegExMatch.Groups[11 + regexMatch.Offset].Value);
            if (!(regexMatch.RegExMatch.Groups[12 + regexMatch.Offset].Value == "-"))
                TimeTaken = int.Parse(regexMatch.RegExMatch.Groups[12 + regexMatch.Offset].Value);
            Browser = regexMatch.RegExMatch.Groups[9 + regexMatch.Offset].Value;

            // Cleanup

            if (ForwardedFor.EndsWith(","))
                ForwardedFor = ForwardedFor.Replace(",", "");


        }

        public string ForwardedFor { get; set; }
        public string RemoteLogName { get; set; }
        public string RemoteUser { get; set; }
        public DateTime RequestTime { get; set; }
        public string URI { get; set; }
        public int Status { get; set; }
        public int ResponseSize { get; set; }
        public string Referrer { get; set; }
        public string UserAgent { get; set; }
        public string ImageReaderSource { get; set; }
        public int PHPTime { get; set; }
        public int TimeTaken { get; set; }
        public string Browser { get; set; }
    }
}
