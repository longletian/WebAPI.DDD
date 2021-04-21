using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application
{
    /// <summary>
    /// 响应实体
    /// </summary>
    public class ReturnResponseBase
    {
        public ReturnResponseBase()
        {
            MsgCode = 0;
            Message = "访问异常";

        }
        public int MsgCode { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }

    public class ReturnResponseBase<T>
    {
        public int MsgCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
