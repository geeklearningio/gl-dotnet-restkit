namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Reflection;

    public static class FormDataHelper
    {
        public static void SetFormData(this Dictionary<string, IFormData> formData, string name, object data)
        {
            switch (data)
            {
                case string str:
                    formData.Add(name, new FormData(str));
                    break;
                case Guid guid:
                    formData.Add(name, new FormData(guid.ToString("D")));
                    break;
                case DateTime dateTime:
                    formData.Add(name, new FormData(dateTime.ToString("o")));
                    break;
                case DateTimeOffset dateTime:
                    formData.Add(name, new FormData(dateTime.ToString("o")));
                    break;
                case ExpandoObject exp:
                    foreach (var item in exp)
                    {
                        SetFormData(formData, $"{name}.{item.Key}", item.Value);
                    }
                    break;
                case IEnumerable collection:
                    int index = 0;
                    foreach (var item in collection)
                    {
                        SetFormData(formData, $"{name}[{index++}]", item);
                    }
                    break;
                case IFile file:
                    formData.Add(name, file);
                    break;
                case IConvertible convertible:
                    formData.Add(name, new FormData(Convert.ChangeType(convertible, typeof(string), System.Globalization.CultureInfo.InvariantCulture)));
                    break;
                default:
                    var properties = data.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

                    foreach (var property in properties)
                    {
                        var value = property.GetValue(data);
                        if (value != null)
                        {
                            SetFormData(formData, $"{name}.{property.Name}", value);
                        }
                    }
                    break;
            }
        }
    }
}
