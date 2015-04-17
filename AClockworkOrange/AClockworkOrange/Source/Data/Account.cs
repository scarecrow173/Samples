using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AClockworkOrange.Source.Data
{
    public class Account
    {
        #region SerializedMembers
        // 公開アカウント情報
        [System.Xml.Serialization.XmlElement("PublicInfo")]
        public Member PublicInfo { get; set; }

        // パスワード
        [System.Xml.Serialization.XmlElement("Password")]
        public string Password { get; set; }
        #endregion
    }
}
