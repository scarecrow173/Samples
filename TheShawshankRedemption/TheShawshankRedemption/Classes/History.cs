using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
namespace TheShawshankRedemption.Classes
{
    [XmlRoot("History")]
    public class History
    {
        public static History GetInstance { get { Instance = Instance != null ? Instance : LoadHistory(); return Instance; } }
        private static History Instance = null;

        private static string FileExtention { get { return "xml"; } }
        private static string FileName { get { return "History" + "." + FileExtention; } }
        public static UInt32 MaxQuantity = 200; // 履歴の最大件数。デフォルトは200

        [XmlElement("HistoryList")]
        public List<string> HistoryList { get; set; }

        [XmlIgnore]// XMLには吐き出したくないので
        public Queue<string> HistoryQueue = new Queue<string>();
        
        [XmlIgnore]// XMLには吐き出したくないので
        public bool IsLoaded = false;

        private History()
        {
            HistoryList = new List<string>();
        }

        // 履歴保存
        public void SaveHistory()
        {
            HistoryList.Clear();
            HistoryList = HistoryQueue.ToList();
            Utility.XmlUtility.SaveToXml( Instance, FileName);
        }
        
        // 履歴読み込み(Instance 作成時のみ)
        static private History LoadHistory()
        {
            History LoadedHistory = Utility.XmlUtility.LoadFromXml<History>(FileName);
            if (LoadedHistory == null)
                LoadedHistory = new History();
            

            foreach (string URL in LoadedHistory.HistoryList)
            {
                LoadedHistory.AddHistory(URL);
            }
            return LoadedHistory;
        }

        // Historyへ追加
        public void AddHistory(string URL)
        {
            // すでに存在していたら無視？タイムスタンプ更新？
            if (HistoryQueue.Contains(URL))
                return;
            HistoryQueue.Enqueue(URL);

            // 一定数を超えていたら古いものから順に削除
            if (HistoryQueue.Count > MaxQuantity)
                HistoryQueue.Dequeue();
                return;
        }
    }
}
