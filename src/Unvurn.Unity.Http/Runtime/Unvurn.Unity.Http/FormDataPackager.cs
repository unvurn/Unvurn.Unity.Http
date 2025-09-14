using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unvurn.Network.Http;
using Unvurn.Unity.IO;

namespace Unvurn.Unity.Http
{
    public class FormDataAttribute : HttpContentPackagerAttribute
    {
        public FormDataAttribute(int index = default, string? name = default, FieldTypes fieldType = FieldTypes.Default) : base(index, name)
        {
            FieldType = fieldType;
        }

        public FieldTypes FieldType { get; }

        public enum FieldTypes
        {
            Default,
            BinaryData
        }
    }

    public class FormDataPackager : AbstractHttpContentPackager<WWWForm, FormDataAttribute>
    {
        protected override WWWForm PackageParameters<T>(T parameterObject, IEnumerable<IPackageInfo> packageInfos)
        {
            var form = new WWWForm();

            if (parameterObject != null)
            {
                foreach (var pi in packageInfos)
                {
                    var attr = (FormDataAttribute?)pi.Attribute;
                    if (attr == null)
                    {
                        continue;
                    }

                    switch (attr.FieldType)
                    {
                        case FormDataAttribute.FieldTypes.BinaryData:
                            var filename = pi.GetValue(parameterObject);
                            if (filename != null)
                            {
                                var data = FileUtils.ReadAll(filename);
                                form.AddBinaryData(GetNameFromPath(pi), data);
                            }
                            break;

                        default:
                            form.AddField(GetNameFromPath(pi), pi.GetValue(parameterObject));
                            break;
                    }
                }
            }

            return form;
        }

        private static string GetNameFromPath(IPackageInfo pi)
        {
            var (s0, ss) = (pi.Path[0], pi.Path.Skip(1));
            return $"{s0}{string.Join(string.Empty, ss.Select(s => $"[{s}]"))}";
        }
    }
}
