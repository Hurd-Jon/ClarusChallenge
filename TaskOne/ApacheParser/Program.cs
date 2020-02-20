using Serilog;
using System;
using System.Linq;

namespace ApacheParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logfile.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var lines = ApacheParseHelper.LoadLogfile(@"C:\projects\Clarus\FirstChallenge.txt");

            // Question 1 : How many requests were successful
            // 200 is success
            // 300 range responses are also succesful but imply the client has some work to do - redirects etc.. 
            // 304 (NOT MODIFIED) is common in this file which means the locally cached object is ok
            var successfulRequests = lines.Where(l => l.Status >= 200  && l.Status < 400);
            Log.Debug("-------------------------------------------------");
            Log.Debug("-------------------------------------------------");
            Log.Debug("----------- Question 1 --------------------------");
            Log.Debug("---- How many requests were successful ----------");
            Log.Debug("There were {0} successful requests", successfulRequests.Count());
            Log.Debug("-------------------------------------------------");

            // Question 2 : Were any of the requests unsuccessful? if so list the requested object URI and the reason the request was unsuccessful?
            // In this file there is 1 404 response - NOT FOUND - There does not appear to be a uri in the 404 log entry
            var unsuccessfulRequests = lines.Where(l => l.Status >= 400);
            Log.Debug("----------- Question 2 -----------");
            Log.Debug("---- Unsuccessful request info ---");
            foreach (var line in unsuccessfulRequests.OrderBy(l => l.Status))
            {
                Log.Debug("{0} || {1}", line.URI, line.Status);
            }
            Log.Debug("There were {0} unsuccessful requests", unsuccessfulRequests.Count());

            // Question 3 : How many unique IP addresses made requests to this server?
            Log.Debug("----------- Question 3 -----------");
            Log.Debug("---- Unique IP addresses ---------");
            var uniqueIPAddresses = lines.Select(l => l.ForwardedFor).Where(l=>!(l=="-")).Distinct().OrderBy(l => l);
            Log.Debug("List of unique IP addresses");
            foreach (var ip in uniqueIPAddresses)
            {
                Log.Debug("{0}", ip);
            }
            Log.Debug("TOTAL COUNT OF UNIQUE IP Addresses {0}", uniqueIPAddresses.Count());

            // Question 4 : What was the largest object served?
            Log.Debug("----------- Question 4 -----------");
            Log.Debug("---- Largest Object Served--------");
            var largestObjectServed = lines.Max(l => l.ResponseSize);
            Log.Debug("The largest object served was {0} bytes", largestObjectServed);

            // Question 5 : What was the average size of objects successfully served?
            Log.Debug("----------- Question 5 -----------");
            Log.Debug("---- Average Object Size ---------");
            var averageSize = lines.Average(l => l.ResponseSize);
            Log.Debug("The average object size served was {0} bytes", averageSize);

            Console.WriteLine("Hello World");

        }
    }
}
