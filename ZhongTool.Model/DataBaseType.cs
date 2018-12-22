using System.ComponentModel;

namespace ZhongTool.Model
{
    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// SqlServer
        /// </summary>
        [Description("SqlServer")]
        SqlServer = 0,
        /// <summary>
        /// MySql
        /// </summary>
        [Description("MySql")]
        MySql = 1,
        /// <summary>
        /// Oracle
        /// </summary>
        [Description("Oracle")]
        Oracle = 2,
        /// <summary>
        /// Access
        /// </summary>
        [Description("Access")]
        Access = 3
    }
}