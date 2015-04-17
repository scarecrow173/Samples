using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Serialization;

namespace AClockworkOrange.Source.Data
{
    public enum eTaskState
    {
        ToDo,
        Doing,
        Done,
        Canseled,
    }
    public enum ePriority
    {
        Trivial,
        Minor,
        Major,
        Critical,
        Blocker,
    }
    public class Task
    {
        #region SerializedMembers
        // タスクの識別ID
        [System.Xml.Serialization.XmlAttribute("id")]
        public UInt32 ID { get; set; }
        
        // タスクの現在の状態
        [System.Xml.Serialization.XmlElement("TaskState")]
        public eTaskState TaskState { get; set; }

        // サブタスクのList
        [System.Xml.Serialization.XmlElement("SubTasks")]
        public List<SubTask> SubTasks { get; set; }

        // タスク名
        [System.Xml.Serialization.XmlElement("Content")]
        public string Content { get; set; }

        // タスクの期限
        [System.Xml.Serialization.XmlElement("Due")]
        public DateTime Due { get; set; }

        // 担当者
        [System.Xml.Serialization.XmlElement("Assignee")]
        public List<UInt32> Assignee { get; set; }

        // 優先度
        [System.Xml.Serialization.XmlElement("Priority")]
        public ePriority Priority { get; set; }
        #endregion

        #region StaticMembers
        //　タスクファイルの拡張子
        public static string Extension = ".tsk";
        #endregion

        #region PublicMembers
        // ファイル名
        public string FileName { get { return ID.ToString() + Extension; } }
        #endregion

        #region PublicMethods
        // コンストラクタ
        public Task()
        {
            SubTasks = new List<SubTask>();
            Assignee = new List<UInt32>();
        }

        // タスクファイルの読み込み
        public static Task LoadFromFile(string _filePath)
        {
            FileStream fstream = new FileStream(_filePath.ToString(), FileMode.Open);
            XmlSerializer projectSerializer = new XmlSerializer(typeof(Task));
            Task result = (Task)projectSerializer.Deserialize(fstream);
            fstream.Close();
            return result;
        }

        // タスクファイルの保存
        public bool SaveToFile(string _directoryPath)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            FileStream fstream = new FileStream(_directoryPath + "/" + FileName, System.IO.FileMode.Create);
            StreamWriter fwriter = new StreamWriter(fstream);

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add(string.Empty, string.Empty);
            XmlSerializer taskSerializer = new XmlSerializer(typeof(Task));
            taskSerializer.Serialize(fwriter, this, emptyNamespace);
            fstream.Close();
            return true;
        }
        #endregion
    }
}
