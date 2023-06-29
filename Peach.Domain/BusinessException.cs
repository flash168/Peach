namespace Peach.Domain
{
    public class BusinessException : Exception
    {
        public int Code { get; set; }

        public BusinessException(string message, int code = 400) : base(message)
        {

        }
    }
}