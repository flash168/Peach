namespace PeachPlayer.ToolExtend
{
    /// <summary>
    ///  常用转换器的静态引用
    /// 使用实例：Converter={x:Static local:XConverter.TrueToFalseConverter}
    /// </summary>
    public sealed class XConverter
    {
        /// <summary>
        /// bool取反
        /// </summary>
        public static IntToBoolConverter InverseBooleanConverter
        {
            get { return Singleton<IntToBoolConverter>.GetInstance(); }

        }




    }
}
