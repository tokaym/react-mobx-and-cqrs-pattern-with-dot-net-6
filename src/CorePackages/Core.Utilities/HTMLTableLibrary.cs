using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Core.Utilities;
public static class HTMLTableLibrary
{
    public static string CreateWithDatas<T>(List<T> model)
    {
        var sb = new StringBuilder();
        if (model.Count > 0)
        {
            //sb.Append(GetTableStyle());
            sb.Append("<table>");
            sb.Append("<thead><tr>");
            foreach (var prop in typeof(T).GetProperties())
            {
                var attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), true);
                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)attributes[0];
                sb.Append("<td> " + descriptionAttribute.Description + "</td>");
            }
            sb.Append("</tr></thead><tbody>");
            foreach (var item in model)
            {
                sb.Append("<tr>");
                foreach (var prop in typeof(T).GetProperties())
                {
                    string value = "";
                    Type type = item.GetType();
                    if (type != null)
                    {
                        PropertyInfo propinf = type.GetProperty(prop.Name);
                        if (propinf != null)
                        {
                            object val = propinf.GetValue(item, null);
                            if (val != null)
                                value = val.ToString();
                        }
                    }
                    sb.Append("<td> " + value + "</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");
        }

        return sb.ToString();
    }

    public static string GetTableStyle()
    {
        string style = "<style> table {border-collapse: collapse;font-family: Tahoma, Geneva, sans-serif;}" +
                        "table td {padding: 15px;}" +
                        "table thead td {background-color: #54585d;color: #ffffff;font-weight: bold;font-size: 13px;border: 1px solid #54585d;}" +
                        "table tbody td {color: #636363;border: 1px solid #dddfe1;}" +
                        "table tbody tr {background-color: #f9fafb;}" +
                        "table tbody tr:nth-child(odd) {background-color: #ffffff;} </style>";

        return style;
    }
}