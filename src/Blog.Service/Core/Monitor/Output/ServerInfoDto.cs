namespace Blog.Service.Core.Monitor.Output
{
    public class ServerInfoDto
    {
        /// <summary>
        /// 环境变量
        /// </summary>
        public string EnvironmentName { get; set; } = null!;
        /// <summary>
        /// 系统架构
        /// </summary>
        public string OsArchitecture { get; set; } = null!;
        /// <summary>
        /// ContentRootPath
        /// </summary>
        public string ContentRootPath { get; set; } = null!;
        /// <summary>
        /// WebRootPath
        /// </summary>
        public string WebRootPath { get; set; } = null!;
        /// <summary>
        /// .NET Core版本
        /// </summary>
        public string FrameworkDescription { get; set; } = null!;
        /// <summary>
        /// 内存占用
        /// </summary>
        public string MemoryFootprint { get; set; } = null!;
        /// <summary>
        /// 启动时间
        /// </summary>
        public string WorkingTime { get; set; } = null!;
    }
}
