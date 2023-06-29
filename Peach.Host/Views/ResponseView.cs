namespace Peach.Host.Views
{

    /// <summary>
    /// 相应模型
    /// </summary>
    public class ResponseView
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object? Data { get; set; }

        public ResponseView(int code, string? message = null, object? data = null)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
