using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Serialization;

namespace AClockworkOrange.Source.Data
{
    [System.Xml.Serialization.XmlRoot("Project")]
    public class Project
    {
        #region SerializedMembers
        // プロジェクト名
        [System.Xml.Serialization.XmlElement("ProjectName")]
        public string ProjectName;

        // 各タスクファイルへのパス
        [System.Xml.Serialization.XmlElement("TasksFiles")]
        public List<string> TaskFiles { get; set; }
        
        // プロジェクト参加者
        [System.Xml.Serialization.XmlElement("Members")]
        public List<Member> Members { get; set; }
        #endregion
        
        #region StaticMembers
        // プロジェクトファイルの拡張子
        public static string Extension = ".tpj";
        #endregion

        #region PublicMembers
        // ファイル名
        public string FileName { get { return ProjectName + Extension; } }
        #endregion 

        #region PrivateMembers
        // プロジェクトに含まれるタスク
        private List<Task> Tasks = new List<Task>();
        #endregion

        #region PublicMethods
        // コンストラクタ
        // Listを作成()
        public Project()
        {
            TaskFiles = new List<string>();
            Members = new List<Member>();
        }

        // Taskの追加
        public void AddTask(Task _task)
        {
            Tasks.Add(_task);
            TaskFiles.Add(_task.FileName);
        }

        // プロジェクトファイルの読み込み
        public static Project LoadFromFile(string _filePath)
        {
            if (!File.Exists(_filePath))
            {
                string Message = "指定されたファイルが見つかりません。\n FilePath : " + _filePath;

                System.Windows.MessageBox.Show(Message, "Error", System.Windows.MessageBoxButton.OK);
                return null;
            }

            FileStream fstream = new FileStream(_filePath, FileMode.Open);
            XmlSerializer projectSerializer = new XmlSerializer(typeof(Project));

            
            Project loadedProject = (Project)projectSerializer.Deserialize(fstream);
            string DirectoryPath = Path.GetDirectoryName(_filePath);
            foreach (string taskFilePath in loadedProject.TaskFiles)
            {
                loadedProject.Tasks.Add(Task.LoadFromFile(DirectoryPath + "/" + taskFilePath));
            }
            fstream.Close();
            return loadedProject;
        }

        // プロジェクトファイルの保存
        public bool SaveToFile(string _directoryPath)
        {
            string outputDir = _directoryPath + "/" + ProjectName;
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            FileStream fstream = new FileStream(outputDir + "/" + FileName, FileMode.Create);
            StreamWriter fwriter = new StreamWriter(fstream);

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add(string.Empty, string.Empty);
            XmlSerializer projectSerializer = new XmlSerializer(typeof(Project));
            projectSerializer.Serialize(fwriter, this, emptyNamespace);

            foreach(Task iTask in Tasks)
            {
                iTask.SaveToFile(outputDir);
            }
            fstream.Close();
            return true;
        }
        #endregion
    }
}
