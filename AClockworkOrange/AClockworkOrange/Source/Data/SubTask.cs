using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AClockworkOrange.Source.Data
{
    public class SubTask
    {
        #region SerializedMembers
        // サブタスク識別ID
        [System.Xml.Serialization.XmlAttribute("id")]
        public string ID { get; set; }

        // サブタスク完了フラグ
        [System.Xml.Serialization.XmlElement("IsComplated")]
        public bool IsComplated { get; set; }

        // サブタスク項目名
        [System.Xml.Serialization.XmlElement("Content")]
        public string Content { get; set; }

        //　サブタスクの完了期限
        [System.Xml.Serialization.XmlElement("Due")]
        public DateTime Due { get; set; }
        #endregion
    }
}
