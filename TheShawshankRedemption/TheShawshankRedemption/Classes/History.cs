using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheShawshankRedemption.Classes
{
    [XmlRoot("History")]
    public class History
    {
        public static History GetInstance { get { Instance = Instance != null ? Instance : new History(); return Instance; } }
        private static History Instance = null;

        private static string FileExtention { get { return "xml"; } }
        private static string FileName { get { return "History" + "." + FileExtention; } }

        [XmlElement("HistoryList")]
        private List<string> HistoryList { get; set; }

        private Stack<string> HistoryStack = new Stack<string>();

        public bool IsLoaded = false;

        private History()
        {
            HistoryList = new List<string>();
            LoadHistory();
        }

        public void SaveHistory()
        {
            HistoryList = HistoryStack.ToList();
            //TODO:ここにSave処理

        }
        
        public void LoadHistory()
        {
            //TODO:ここにLoad処理


            foreach(string iHistoryStr in HistoryList)
            {
                HistoryStack.Push(iHistoryStr);
            }
        }
    }
}
