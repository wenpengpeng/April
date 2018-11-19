// 文件名：BusinessHelper.cs
// 
// 创建标识：温朋朋 2018-06-21 16:58
// 
// 修改标识：温朋朋2018-06-21 16:58
// 
// ------------------------------------------------------------------------------
namespace April.Application.Commons
{
    public static class BusinessHelper
    {
        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            var returnStr = "";
            for (var i = 0; i < strLong; i++)
            {
                returnStr += str;
            }

            return returnStr;
        }
    }
}