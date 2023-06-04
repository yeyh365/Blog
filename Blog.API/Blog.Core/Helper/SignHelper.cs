using System;
using System.Collections.Generic;
using System.Linq;
using TrainingPlaning.Core.Helper;

namespace Blog.Core
{
    public class SignHelper
    {
        public static string MakeSign(Dictionary<string, string> dict)
        {
            var sortedDict = dict.OrderBy(x => x.Key, StringComparer.Ordinal).ToDictionary(x => x.Key, y => y.Value);
            var queryStr = string.Join("&", sortedDict.Select(m => m.Key + "=" + m.Value));
            var result = SHA1Helper.SHA1Encrypt(queryStr, false, true);
            result = result.Replace("-", "");
            return result;
        }

        public static string MakeSignNoSort(Dictionary<string, string> dict)
        {
            //var sortedDict = dict.OrderBy(x => x.Key, StringComparer.Ordinal).ToDictionary(x => x.Key, y => y.Value);
            var queryStr = string.Join("&", dict.Select(m => m.Key + "=" + m.Value));
            var result = SHA1Helper.SHA1Encrypt(queryStr, false, true);
            result = result.Replace("-", "");
            return result;
        }
    }
}
