using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ZhongTool.Common
{
    public static class EnumHelper
    {
        /// <summary>
        /// Enum转换为数据列表
        /// </summary>
        /// <returns></returns>
        public static List<BindEnumItem> EnumToList<T>()
        {
            List<BindEnumItem> lstData = new List<BindEnumItem>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                BindEnumItem item = new BindEnumItem();
                object[] arrDesc = e.GetType().GetField(e.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (arrDesc?.Length > 0)
                {
                    DescriptionAttribute desc = arrDesc[0] as DescriptionAttribute;
                    item.Text = desc?.Description;
                }
                else
                {
                    item.Text = e.ToString();
                }

                item.Value = Convert.ToInt32(e);
                lstData.Add(item);
            }
            return lstData;
        }
    }

    public class BindEnumItem
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
}