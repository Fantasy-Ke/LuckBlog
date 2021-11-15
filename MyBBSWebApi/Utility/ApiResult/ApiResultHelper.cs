using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Utility.ApiResult
{
    public static class ApiResultHelper
    {
        public static ApiResult Success(dynamic data)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Total = 0,
                Msg = "操作成功"
            };
        }
        public static ApiResult Success(dynamic data,RefAsync<int> total)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Total = total,
                Msg = "操作成功"
            };
        }
        public static ApiResult Error(string msg)
        {
            return new ApiResult
            {
                Code = 500,
                Data = null,
                Total = 0,
                Msg = msg
            };
        }
    }
}
