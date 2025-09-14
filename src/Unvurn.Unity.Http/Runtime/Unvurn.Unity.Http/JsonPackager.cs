using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unvurn.Network.Http;

namespace Unvurn.Unity.Http
{
    public class JsonPackager : IHttpContentPackager<byte[]>
    {
        public byte[] Pack<T>(T param)
        {
            if (param is IDictionary<string, string> dictparam)
            {
                return Encoding.UTF8.GetBytes(ConvertIntoJson(dictparam));
            }

            throw new NotImplementedException();
        }

        // TODO: 標準的なJsonコンバーターで置き換える。
        [Obsolete]
        private static string ConvertIntoJson<T>(IDictionary<string, T> data)
        {
            var j = data.Aggregate("{", (current, item) => current + "\"#{item.Key}\":\"#{item.Value}\",");
            j = j.Substring(0, j.Length - 1);
            j += "}";
            return j;
        }
    }
}
