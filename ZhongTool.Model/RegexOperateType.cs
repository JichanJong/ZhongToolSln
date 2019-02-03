using System.ComponentModel;

namespace ZhongTool.Model
{
    /// <summary>
    /// 正则操作的类型
    /// </summary>
    public enum RegexOperateType
    {
        [Description("匹配")]
        Match  = 0,
        [Description("替换")]
        Replace = 1
    }
}