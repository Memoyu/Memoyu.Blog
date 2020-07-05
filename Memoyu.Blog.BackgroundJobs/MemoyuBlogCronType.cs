/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.BackgroundJobs
*   文件名称 ：MemoyuBlogCronType
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-05 12:11:20
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;

namespace Memoyu.Blog.BackgroundJobs
{
    public class CronType
    {
        /// <summary>
        /// 周期性为分钟的任务
        /// </summary>
        /// <param name="interval">执行周期的间隔，默认为每分钟一次</param>
        /// <returns></returns>
        public static string Minute(int interval = 1)
        {
            return "1 0/" + interval + " * * * ? ";//1 0/1 * * * ?
        }

        /// <summary>
        /// 周期性为小时的任务
        /// </summary>
        /// <param name="minute">第几分钟开始，默认为第一分钟</param>
        /// <param name="interval">执行周期的间隔，默认为每小时一次</param>
        /// <returns></returns>
        public static string Hour(int minute = 1, int interval = 1)
        {
            return "1 " + minute + " 0/" + interval + " * * ? ";//1 1 0/1 * * ?
        }

        /// <summary>
        /// 周期性为天的任务
        /// </summary>
        /// <param name="hour">第几小时开始，默认从1点开始</param>
        /// <param name="minute">第几分钟开始，默认从第1分钟开始</param>
        /// <param name="interval">执行周期的间隔，默认为每天一次</param>
        /// <returns></returns>
        public static string Day(int hour = 1, int minute = 1, int interval = 1)
        {
            return "1 " + minute + " " + hour + " 1/" + interval + " * ? ";
        }

        /// <summary>
        /// 周期性为周的任务
        /// </summary>
        /// <param name="dayOfWeek">星期几开始，默认从星期一点开始</param>
        /// <param name="hour">第几小时开始，默认从1点开始</param>
        /// <param name="minute">第几分钟开始，默认从第1分钟开始</param>
        /// <returns></returns>
        public static string Week(DayOfWeek dayOfWeek = DayOfWeek.Monday, int hour = 1, int minute = 1)
        {
            return Cron.Weekly(dayOfWeek, hour, minute);
        }

        /// <summary>
        /// 周期性为月的任务
        /// </summary>
        /// <param name="day">几号开始，默认从一号开始</param>
        /// <param name="hour">第几小时开始，默认从1点开始</param>
        /// <param name="minute">第几分钟开始，默认从第1分钟开始</param>
        /// <returns></returns>
        public static string Month(int day = 1, int hour = 1, int minute = 1)
        {
            return Cron.Monthly(day, hour, minute);
        }

        /// <summary>
        /// 周期性为年的任务
        /// </summary>
        /// <param name="month">几月开始，默认从一月开始</param>
        /// <param name="day">几号开始，默认从一号开始</param>
        /// <param name="hour">第几小时开始，默认从1点开始</param>
        /// <param name="minute">第几分钟开始，默认从第1分钟开始</param>
        /// <returns></returns>
        public static string Year(int month = 1, int day = 1, int hour = 1, int minute = 1)
        {
            return Cron.Yearly(month, day, hour, minute);
        }
    }
}
