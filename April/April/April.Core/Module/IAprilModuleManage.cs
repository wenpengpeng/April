// 文件名：IAprilModuleManage.cs
// 
// 创建标识：温朋朋 2018-05-08 15:13
// 
// 修改标识：温朋朋2018-05-08 15:13
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Core.Module
{
    public interface IAprilModuleManage
    {
        /// <summary>
        ///     初始化Modules
        /// </summary>
        /// <param name="startupModule"></param>
        void InitModules(Type startupModule);
        /// <summary>
        ///     加载Modules
        /// </summary>
        /// <param name="startupModule"></param>
        void LoadModules(Type startupModule);
    }
}