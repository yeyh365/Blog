using System.ComponentModel;
namespace Blog.Domain.Enums
{
    public enum EnumTagType : short
    {
        /// <summary>
        /// 课程
        /// </summary>
        [Description("课程")]
        Course = 1,

        /// <summary>
        /// 学员
        /// </summary>
        [Description("学员")]
        Trainee = 2,
    }

    /// <summary>
    /// 教师资质类型
    /// </summary>
    public enum EnumCertificateType : short
    {
        /// <summary>
        /// 已获取
        /// </summary>
        [Description("已获取")]
        Acquired = 1,

        /// <summary>
        /// 已审批
        /// </summary>
        [Description("已审批")]
        Approved = 2,

        /// <summary>
        /// 未获取
        /// </summary>
        [Description("未获取")]
        NoAcquired = 3
    }
}