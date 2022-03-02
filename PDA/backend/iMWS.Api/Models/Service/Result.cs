using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.Service
{
    public class Result
    {
        public static ResultViewModel<T> Create<T>(bool success, string message, T data)
        {
            return new ResultViewModel<T>(success, message, data);
        }

        public static ResultViewModel Create(bool success, string message)
        {
            return new ResultViewModel(success, message);
        }
    }
}