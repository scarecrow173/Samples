using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Serialization;

namespace AClockworkOrange.Source.Data
{

    public class Config
    {
        #region SerializedMembers
        // 管理対象ファイルのPath
        [System.Xml.Serialization.XmlElement("TargetProjectFilePath")]
        public string TargetProjectFilePath { get; set; }

        // ユーザーアカウント情報
        [System.Xml.Serialization.XmlElement("User")]
        public Account User { get; set; }

        #endregion

        #region StaticMembers
        // コンフィグファイルの拡張子
        public static string Extension = ".tcf";
        public static string DefaultConfigFileName = "default";
        public static string DirectoryName = "Configs";
        #endregion

        #region PublicMembers
        // ファイル名
        public string FileName { get { return DefaultConfigFileName + Extension; } }
        #endregion


        static public Config LoadFromFile(string _filePath)
        {
            if (!File.Exists(_filePath))
            {
                string Message = "指定されたファイルが見つかりません。\n FilePath : " + _filePath;

                System.Windows.MessageBox.Show(Message, "Error", System.Windows.MessageBoxButton.OK);
                return null;
            }

            FileStream fstream = new FileStream(_filePath, FileMode.Open);
            XmlSerializer projectSerializer = new XmlSerializer(typeof(Config));
            Config loadedConfig = (Config)projectSerializer.Deserialize(fstream);

            fstream.Close();
            return loadedConfig;
        }
        public bool SaveToFile(string _directoryPath)
        {
            string outputDirectoryPath = _directoryPath + DirectoryName;
            if (!Directory.Exists(outputDirectoryPath))
            {
                Directory.CreateDirectory(outputDirectoryPath);
            }

            FileStream fstream = new FileStream(outputDirectoryPath + "/" + FileName, FileMode.Create);
            StreamWriter fwriter = new StreamWriter(fstream);

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add(string.Empty, string.Empty);

            XmlSerializer configSerializer = new XmlSerializer(typeof(Config));
            configSerializer.Serialize(fwriter, this, emptyNamespace);
            fstream.Close();
            return true;
        }
    }
}
