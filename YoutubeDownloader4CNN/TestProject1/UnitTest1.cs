using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YDL_Downloader;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using YoutubeDLSharp;
using CsvHelper;
using System.Diagnostics;
using Functional;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace TestProject1
{
    public class AudioList: IEquatable<AudioList>
    {
        public string name { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string cat { get; set; }

        public bool Equals([AllowNull] AudioList other)
        {
            return this.name == other.name && this.start == other.start && this.end == other.end && this.cat == other.cat ? true : false;
        }
    }
    [TestClass]
    public class UnitTest1
    {
        private static int indexDataCsv;
        Queue<List<MyTableAudioData>> queueAudioData = new Queue<List<MyTableAudioData>>();

        [TestMethod]
        public void TestMethod1()
        {
            //string file_path = @"d:\Projects\\YoutubeDownloader4CNN\YoutubeDownloader4CNN\YDL_Downloader\WorkstationPathsConfig.json";
            //var workstation = File.Exists("WorkstationPathsConfig.json") ? JsonConvert.DeserializeObject<Workstation>(File.ReadAllText("WorkstationPathsConfig.json")) : new Workstation
            //{
            //    name = "Workstation",
            //    dates = 1
            //};

            //workstation.dates += 1;

            //File.WriteAllText("WorkstationPathsConfig.json", JsonConvert.SerializeObject(workstation));

            //Console.ReadKey();
        }
        [TestMethod]
        public void TestMethod2()
        {
            string file_path = @"D:\Projects\YoutubeDownloader4CNN\YoutubeDownloader4CNN\TestProject1\bin\Debug\netcoreapp3.1\WorkstationPathsConfig.json";
            WorkstationPathsConfig workstation = new WorkstationPathsConfig(file_path);

        }
        [TestMethod]
        public void TestYoutube()
        {
            var youtubeDl = new YoutubeDL();
            youtubeDl.YoutubeDLPath = @"D:\testdir\samples";
            youtubeDl.OutputFolder = @"D:\testdir\upload";
            var res = youtubeDl.RunVideoDownload("https://www.youtube.com/watch?v=anHgjpvfNs0");
        }
        [TestMethod]
        public void TestSizeFile()
        {
            string filename = "---1_cCGK4M", name;
            FileInfo audiofile = new FileInfo("D:\\Projects\\YoutubeDownloader4CNN\\Downloads\\" + filename + ".wav");
            long sizeFile = audiofile.Length;
        }
        [TestMethod]
        public void TestWriteInFile()
        {
            string pathCsvFile = @"C:\Users\a.petrenko\Desktop\1.csv";
            string audio_name = "", audio_start = "", audio_end = "", audio_cat = "";
            List<AudioList> audioLists = new List<AudioList>
            {
                new AudioList { name = audio_name, 
                    start = audio_start, end = audio_end, cat = audio_cat}
            };
            using (StreamWriter streamWrite = new StreamWriter(pathCsvFile))
            {
                /**using (CsvWriter csvReader = new CsvWriter(streamWrite))
                {
                    csvReader.Configuration.Delimiter = " ";
                    csvReader.WriteRecords(AudioList);
                }*/
            }
        }
        [TestMethod]
        public void workWithServer()
        {
            //http://localhost:8301
        }
        [TestMethod]
        public void TestParser()
        {
            DictPars dictPars = new DictPars();
            /**
            dictPars.ParseLabelsIndices();
            foreach (KeyValuePair<string, string> keyValue in dictPars.ParseLabelsIndices())
            {
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            }
            */
            //dictPars.ParseTest2File();
            Trace.WriteLine("aaaaaaaaaaaaaaaaaaa");
        }
        [TestMethod]
        public void LessonList()
        {
            List<int> a = new List<int> { 1, 2, 4, 3, 5, 4, 4, 2, 6 };
            foreach (int val in a.Distinct())
            {
                string s = val + " - " + a.Where(x => x == val).Count() + " раз";
            }
            List<string> integerStr = new List<string> { "one", "two", "three", "four", "five" };
            integerStr.Remove("three");
            List<AudioList> audioDatas = new List<AudioList>();
            for (int i = 0; i < audioDatas.Count; ++i)
            {

            }
            string my_name = "wuierow";
            AudioList audio = new AudioList()
            {
                name = "wuierow",
                start = "03402034",
                end = "35",
                cat = "gfhgfh"
            };
            audioDatas.Add(new AudioList() { name = "wuierow", start = "03402034", end = "35", cat = "gfhgfh" });
            //if (audioDatas.Contains(new AudioList(){ name = "wuierow", start = "03402034", end = "35", cat = "gfhgfh" }))
            audioDatas.Remove(audio);
        }
    }
}

