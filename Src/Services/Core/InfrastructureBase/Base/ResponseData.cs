namespace InfrastructureBase
{
    /// <summary>
    /// 响应实体
    /// </summary>
    public class ResponseData
    {
        public ResponseData()
        {
            MsgCode = 1;
            Message = "访问异常";
        }
        public int MsgCode { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public long? TimeStamp { get; set; }
    }


    public class ResponseData<T> where  T:class
    {
        public int MsgCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public long? TimeStamp { get; set; }
    }
}
