using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeDLSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using YoutubeDLSharp.Options;
using System.Threading;
using System.Diagnostics;

namespace YDL_Downloader
{
    public class WorkstationPathsConfig
    {
        public string Tmp;
        public string Sample;
        public string Dir;
        Workstation workstaion = new Workstation();
        public WorkstationPathsConfig(string input)
        {
            string YoutuBeuri = "https://www.youtube.com/watch?v=";
            string Sampledir = @"\samples";
            string PathToDownload = @"\upload";
            string PathToTemp = @"\tmp";
            string AudioFormat = ".mp3";
            string Ds_Name = "Youtube8M";
            //string usr = Environment.UserName;
            string nameMachine = System.Net.Dns.GetHostName();
            string text = File.ReadAllText(input);
            JObject items = JObject.Parse(text);
            foreach (var item in items)
            {
                if (item.Key == nameMachine)
                {
                    JObject v = JObject.Parse(item.Value.ToString());
                    foreach (KeyValuePair<string, JToken> property in v)
                    {
                        if (property.Key == "dataset_csv_root_path")
                        {
                            workstaion.dataset_csv_root_path = property.Value.ToString();
                        }
                        else if (property.Key == "dataset_json_root_path")
                        {
                            workstaion.dataset_json_root_path = property.Value.ToString();
                        }
                        else if (property.Key == "dataset_upload_root_path")
                        {
                            workstaion.dataset_upload_root_path = property.Value.ToString();
                        }
                        else if (property.Key == "core_root_path")
                        {
                            workstaion.core_root_path = property.Value.ToString();
                        }
                    }
                    break;
                }
            }
            Tmp = workstaion.dataset_upload_root_path + PathToTemp;
            Sample = workstaion.dataset_upload_root_path + Sampledir;
            Dir = workstaion.dataset_upload_root_path + PathToDownload;
            try
            {
                if (!(Directory.Exists(Sample)))
                {
                    string path = Sample;
                    Directory.CreateDirectory(path);
                }
                if (!(Directory.Exists(Dir)))
                {
                    string path = Dir;
                    Directory.CreateDirectory(path);
                }
                if (!(Directory.Exists(Tmp)))
                {
                    string path = Tmp;
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
    public class SampleExtractUnit
    {
        public string download_dir;
        public string class_url;
        public string class_df;
        public string samples;
        public string SAMPLE_RATE = "22050";
        public SampleExtractUnit(string df, string url, string load_dir, string sample_dir) //если ссылка передаётся в конструктор, то зачем передовать ссылку в функцию
        {
            download_dir = load_dir;
            class_url = url;
            class_df = df;
            samples = sample_dir;
        }
        public void MyHook(string status)
        {
            if (status == "downloading")
            { 
            }
            if (status == "finished")
            { 
            }
            if (status == "error")
            { 
            }
        }
        public enum ErrorTypes
        {
            Download,
            FileSizeIsNull,
            FileNotExist,
            RunTimeOut,
            UnknownError
        }
        public ErrorTypes DownloadClip(string link, string name, double start, double end)
        {
            string filename = download_dir + ("\\" + name + ".wav");
            if (!(File.Exists(filename)))
            {
                var ydl_opts = new OptionSet() {
                    Format = "bestaudio/best",
                    Output = filename,
                    NoPlaylist = true,
                    Continue = true,
                    Quiet = true,
                    IgnoreErrors = true,
                    NoWarnings = true,
                    Verbose = false,
                    //PostprocessorArgs = "FFmpegExtractAudio wav 192";
                    AudioFormat = AudioConversionFormat.Wav,
                    KeepVideo = false,
                    AudioQuality = 128,
                    ExtractAudio = true //возможно false
                };
                try
                {
                    var youtube_dl = new YoutubeDL();
                    youtube_dl.OutputFolder = download_dir;
                    youtube_dl.OutputFileTemplate = filename;
                    youtube_dl.YoutubeDLPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\youtube-dl.exe";
                    youtube_dl.FFmpegPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\ffmpeg\\bin\\ffmpeg.exe";
                    var res = youtube_dl.RunAudioDownload(link,  AudioConversionFormat.Wav, new CancellationToken(), null, null, ydl_opts);
                        
                    FileInfo audiofile = new FileInfo(download_dir + "\\" + name + ".wav");
                    string pathAudio = audiofile.DirectoryName;
                    if (audiofile.Exists)
                        return ErrorTypes.FileNotExist;
                    
                    if (audiofile.Length == 0)
                        return ErrorTypes.FileSizeIsNull;
                    ExtractSamples(name, start, end);
                    return ErrorTypes.Download;
                }
                catch (Exception e)
                {
                    return ErrorTypes.UnknownError;
                }
            }
            else
            {
                filename += "already exist";
                return ErrorTypes.Download;
            }
        }
        public void DownloadDataSet(double start, double end, int count)
        {
            //double index = start;
            string link = class_url;
            bool tmp = true;
            DownloadClip(link, "" ,start, end);
            if (tmp) 
            {
                //var date_audion = new AudioData { audio_url = class_url };
                //MainWindow mw = new MainWindow;
                //add_grid();
                //MainWindow mw = new MainWindow;
            }
            else
            {

            }
        }
        public void ExtractSamples(string filename, double start, double end)
        {
            try
            {
                Thread.Sleep(3000); //Задержка нужна для того, чтобы аудио файл успел создаться
                string downloadDir = download_dir + "\\" + filename + ".wav";
                //filename = download_dir + filename;
                string dest = samples + "\\" + filename + ".wav";
                string cmd = "ffmpeg " + "-ss " + Convert.ToString(start) + " -i " + downloadDir + " -t " + Convert.ToString(end-start) + " -c " + "copy " + "-acodec " + "pcm_u8 " + "-ar " + SAMPLE_RATE + " " + dest;
                Process.Start("cmd.exe", "/C" + cmd);
            }
            catch (Exception err)
            {
                ;
            }
        }
        public void ConvertAudion() // Возможно он не нужен так как мы качаем в формате wav
        {
            string filename = ""; // временная переменная
            string cmd = "ffmpeg" + " -i " + filename + " -acodec" + " pcm_u8 " + SAMPLE_RATE;
            Process.Start("cmd.exe", "/C" + cmd);
        }
    }

    public class UserData
    {
        public UserData(string Id)
        {
            this.Id = Id;
        }
        public string Id { get; set; }
    }
}
