
namespace Blog.Core.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System.Reflection;

    public enum Enum
    {
        /// <summary>
        /// 人员
        /// </summary>
        [Description("人员")]
        User = 0,
        /// <summary>
        /// 学员
        /// </summary>
        [Description("学员")]
        Trainee = 1,
        /// <summary>
        /// 老师
        /// </summary>
        [Description("老师")]
        Trainer = 2
    }
    public enum ConfigType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        [Description("字符串")]
        String = 1,
        /// <summary>
        /// 实体
        /// </summary>
        [Description("实体")]
        Entity = 2,
        /// <summary>
        /// 列表
        /// </summary>
        [Description("列表")]
        List = 3
    }
    public enum EnumLogType
    {
        /// <summary>
        /// 访问日志
        /// </summary>
        [Description("访问日志")]
        Info = 1,
        /// <summary>
        /// 报错日志
        /// </summary>
        [Description("报错日志")]
        Error = 2,
    }

    /// <summary>
    /// 代办
    /// </summary>
    public enum EnumInterventionStatus
    {
        /// <summary>
        /// 代办
        /// </summary>
        [Description("代办")]
        Task = 1,
        /// <summary>
        /// 已办
        /// </summary>
        [Description("已办")]
        Approve = 2,
    }

    /// <summary>
    /// 初筛状态
    /// </summary>
    public enum EnumDetectionResult
    {
        /// <summary>
        /// 有反应
        /// </summary>
        [Description("有反应")]
        Reactive = 1,
        /// <summary>
        /// 无反应
        /// </summary>
        [Description("无反应")]
        NoReactive = 0,
        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]
        Invalid = 2,
    }
}
