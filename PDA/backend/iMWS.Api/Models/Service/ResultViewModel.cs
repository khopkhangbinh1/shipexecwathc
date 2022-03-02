using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace iMWS.Api.Models.Service
{
    public class ResultViewModel<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public ResultViewModel(bool success, string message, T data)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }
    }

    public class ResultViewModel : ResultViewModel<object>
    {
        public ResultViewModel(bool success, string message) : base(success, message, null)
        {
        }
    }
}