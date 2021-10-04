using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YDL_Downloader
{
    public class Workstation
    {
        public string dataset_csv_root_path { get; set; }
        public string dataset_json_root_path { get; set; }
        public string dataset_upload_root_path { get; set; }
        public string core_root_path { get; set; }
    }
    public enum ClientStatus
    {
        Downloading,
        NotDownload,
        Busy,
        NotBusy
    }

    public class ListAudioData
    {
        public string Id { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
        public string Cats { get; set; }
        public string Ready { get; set; }
    }
    //[ValueConversion(typeof(DateTime), typeof(String))]
    public class DateConverter : IValueConverter
    {
        static Dictionary<string, string> dictPars = DictPars.ParseLabelsIndices();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string patternFourthString = "([^\",]+)";
            string cats = (string)value, nameAudion;
            MatchCollection matches = Regex.Matches(cats, patternFourthString);
            StringBuilder fullstr = new StringBuilder();
            foreach (Match CatsMatch in matches)
            {
                if (dictPars.TryGetValue(CatsMatch.Value, out nameAudion))
                {
                    fullstr.Append(nameAudion + ", ");
                }
            }
            fullstr.Remove(fullstr.Length - 2, 2);
            return fullstr.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /**
            string strValue = value as string;
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return DependencyProperty.UnsetValue;
            */
            throw new Exception();
        }
    }
}
