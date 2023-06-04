using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Helper
{

    public class Demo
    {
        public static string GetInfo(int type)
        {
            StackTrace stackTrace = new StackTrace(true);
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            switch (type)
            {
                case 1:
                    //获取当前方法的命名空间
                    return MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                case 2:
                    //获取当前方法的类名称，包括命名空间
                    return MethodBase.GetCurrentMethod().DeclaringType.FullName;
                case 3:
                    //获取当前方法名称
                    return MethodBase.GetCurrentMethod().Name;
                case 4:
                    //获取父方法的命名空间
                    return methodBase.DeclaringType.Namespace;
                case 5:
                    //获取父方法的类名，不包括命名空间
                    return methodBase.DeclaringType.Name;
                case 6:
                    //获取父方法名称
                    return methodBase.Name;
            }
            return null;
        }
    }

}
