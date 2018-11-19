// 文件名：AppAuthorize.cs
// 
// 创建标识：温朋朋 2018-06-21 15:34
// 
// 修改标识：温朋朋2018-06-21 15:34
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace April.Application.Authorizations.Entity
{
    public class AppAuthorize
    {
        /// <summary>
        /// AppAuthorize
        /// </summary>
        public AppAuthorize()
        {
            AppMethodAuthorizes = new List<AppMethodAuthorize>();
        }

        /// <summary>
        /// 操作码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// AppMethodAuthorizes
        /// </summary>
        public List<AppMethodAuthorize> AppMethodAuthorizes { get; set; }
    }
}