using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace TheShawshankRedemption.Classes.Utility
{
    // xml操作用Utility
    static class XmlUtility
    {
        // xml保存
        static public void SaveToXml<T>( T _target, string _path)
        {
            string OutputDir = Path.GetDirectoryName(_path);
            if ((OutputDir != string.Empty) && (!Directory.Exists(Path.GetDirectoryName(_path))))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_path));
            }

            FileStream fstream = new FileStream(_path, FileMode.Create);
            StreamWriter fwriter = new StreamWriter(fstream);

            XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add(string.Empty, string.Empty);
            XmlSerializer targetSerializer = new XmlSerializer(typeof(T));
            targetSerializer.Serialize(fwriter, _target, emptyNamespace);
            fstream.Close();
        }

        // xml読み込み
        static public T LoadFromXml<T>(string _path, bool _checkExist = false)
        {
            if (!File.Exists(_path))
            {
                if (_checkExist)
                {
                    string Message = "指定されたファイルが見つかりません。\n FilePath : " + _path;
                    System.Windows.MessageBox.Show(Message, "Error", System.Windows.MessageBoxButton.OK);
                }
                return default(T);
            }

            FileStream fstream = new FileStream(_path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            object loadedXmlObject = xmlSerializer.Deserialize(fstream);
            
            fstream.Close();
            return (T)loadedXmlObject;
        }
    }
}
