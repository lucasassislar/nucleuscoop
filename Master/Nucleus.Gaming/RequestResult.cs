using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class RequestResult
    {
        public dynamic BaseData { get; protected set; }

        public bool Finished { get; protected set; }
        public bool Success { get; protected set; }

        public string LogData { get; protected set; }
        public object AdditionalData { get; protected set; }
    }

    public class RequestResult<T> : RequestResult
    {
        private T data;
        public T Data
        {
            get { return data; }
        }

        public RequestResult()
        {

        }

        public RequestResult(T data)
        {
            this.data = data;
            BaseData = data;
        }

        public RequestResult(T data, bool isSucess)
            : this(data)
        {
            this.Success = isSucess;
        }

        public void SetData(T data)
        {
            this.data = data;
        }

        public void SetStatus(bool success)
        {
            this.Finished = true;
            this.Success = success;
        }

        public void SetAdditionalData(object data)
        {
            this.AdditionalData = data;
        }

        //public void SetSuccess(bool success)
        //{
        //    this.Success = success;
        //}

        //public void SetFailed(bool failed)
        //{
        //    this.Failed = failed;
        //}

        public void SetLogData(string logData)
        {
            LogData = logData;
        }

        public void LogLine(string line)
        {
            LogData += Environment.NewLine + line;
        }

        public void LogLine(string line, params object[] args)
        {
            LogData += Environment.NewLine + string.Format(line, args);
        }
    }
}
