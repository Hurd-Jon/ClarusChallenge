namespace ApacheParser.Model
{
    class LogLine
    {
        public LogLine(string line)
        {
            var fields = line.Split("\t");

            IP = fields[0];
            Field1 = fields[1];
            Field2 = fields[2];
            Date1 = fields[3];
            Date2 = fields[4];
        }

        public string IP { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Date1 { get; set; }
        public string Date2 { get; set; }

        public string OutputFormat()
        {
            return string.Format("{0},{1},{2},{3},{4}", FormatIPAddress(IP), Field1, Field2, Date1, Date2);
        }

        string FormatIPAddress(string ip)
        {
            // strip out zerod that ae padding octets
            // Do this by converting to a number and then back to a string
            var parts = ip.Split(".");

            var result = string.Format("{0}.{1}.{2}.{3}", int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));

            return result;
        }

    }
}
