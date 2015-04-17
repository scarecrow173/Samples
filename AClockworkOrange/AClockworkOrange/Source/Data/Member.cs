using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AClockworkOrange.Source.Data
{
    public class Member
    {
        #region SerializedMembers
        // メンバ識別ID
        [System.Xml.Serialization.XmlAttribute("id")]
        public UInt32 ID { get; set; }
        #endregion
        #region PublicMembers
        // 名前
        public string Name;
        // メールアドレス
        public string MailAddress;
        #endregion
    }
}
