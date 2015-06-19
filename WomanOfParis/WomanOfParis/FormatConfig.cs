using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.WomanOfParis
{
    public class FormatConfig
    {
        public static string TopSeparator   = "/**";
        public static string LinePrefix     = "*";
        public static string BotomSeparator = "*/";
        public static string File           = "@file      [FILENAME]";
        public static string Brief          = "@brief     [COPY_WRITE_DEFINE]";
        public static string Date           = "@date      [CREATE_DATE]";
        public static string Author         = "@author    [AUTHOR_NAME]";
        public static string Param          = "@param     [IN_OUT] [PARAM_NAME] [PARAM_DESCRIPTION]";
        public static string Return         = "@return    [RETURN_CODE]";
        public static string Sa             = "@sa        [LINK_FUNCTION]";
        public static string Detail         = "@detail    [DETAIL]";
        public static string Def            = "@def       [DEFINE__DESCRIPTION]";
        public static string Class          = "@class     [CLASS_DESCRIPTION]";
        public static string Enum           = "@enum      [ENUM_DESCRIPTION]";
        public static string Struct         = "@struct    [STRUCT_DESCRIPTION]";
        public static string Namespace      = "@namespace [NAMESPACE_DESCRIPTION]";
        public static string Fn             = "@fn        [FUNCTION_DESCRIPTION]";
    }
}
