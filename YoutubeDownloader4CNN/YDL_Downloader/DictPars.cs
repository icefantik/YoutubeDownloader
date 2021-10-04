using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace YDL_Downloader
{
    public class DictPars
    {
        static public Dictionary<string, string> ParseLabelsIndices()
        {
            string pattern = "([^,\"]+)";
            Dictionary<string, string> countries = new Dictionary<string, string>();
            StreamReader fileStreamRead = new StreamReader(@"D:\Projects\YoutubeDownloader4CNN\class_labels_indices.csv");
            string line = fileStreamRead.ReadLine();
            while (!fileStreamRead.EndOfStream)
            {
                line = fileStreamRead.ReadLine();
                MatchCollection matchsFileString = Regex.Matches(line, pattern);
                countries.Add(matchsFileString[1].Groups[0].Value, matchsFileString[2].Groups[0].Value);
            }
            return countries;
        }
        public void ParseTestFile()
        {
            StreamReader fileStreamRead = new StreamReader(@"C:\Users\a.petrenko\Desktop\Производственная практика\csvs");
            string line = fileStreamRead.ReadLine();
            while (!fileStreamRead.EndOfStream)
            {
                line = fileStreamRead.ReadLine();
            }
        }
        public void ParseTest2File()
        {
            string pattern = "([^,\\[\"\\]]+)", patterForDouble = "([^,\"\\[\\]]+)";
            StreamReader fileStreamRead = new StreamReader(@"C:\Users\a.petrenko\Desktop\Производственная практика\csvs\test2.csv");
            string line = fileStreamRead.ReadLine();
            while (!fileStreamRead.EndOfStream)
            {
                line = fileStreamRead.ReadLine();
                MatchCollection matchsFileString = Regex.Matches(line, pattern);
                /**
                string s = matchsFileString[0].Groups[0].Value;
                string s2 = matchsFileString[1].Groups[0].Value;
                string s3 = matchsFileString[2].Groups[0].Value;
                string s4 = matchsFileString[3].Groups[0].Value;
                */
            }
        }
    }
}
