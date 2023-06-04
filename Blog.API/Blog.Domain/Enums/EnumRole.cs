
namespace Blog.Domain.Enums
{
    using System.ComponentModel;
    /// <summary>
    /// 角色类型
    /// </summary>
    public enum EnumRole : short
    {
        [Description("Normal")]
        Normal = 0,
        [Description("Trainner")]
        Trainner = 1,
        [Description("Trainne")]
        Trainne = 2,
        [Description("LineManager")]
        LineManager = 3,
        [Description("SystemAdmin")]
        SystemAdmin = 4,
        [Description("TrainingManager")]
        TrainingManager = 5,
        [Description("AgentAdmin")]
        AgentAdmin = 6,
        [Description("Qualification")]
        Qualification = 7,
        [Description("Captain")]
        Captain = 8,
        [Description("Judges")]
        Judges = 9,
        [Description("JudgeLeader")]
        JudgeLeader = 10,
        [Description("Review")]
        Review = 11
    }
}