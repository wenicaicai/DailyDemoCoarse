using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RequestsPerSecond
{
    /// <summary>
    /// 前提：Log型数据，不需要实时更新
    /// 记录用户观看视频日志记录
    /// 借用缓存技术，每隔30秒上传一次
    /// 任务队列更新数据库
    /// </summary>
    public class SixThousandPerSecond
    {
        public IEnumerable<int> ConvertToProgressArray(string processString)
        {
            if (string.IsNullOrEmpty(processString))
                throw new Exception("processString is null or Empty.");

            var strStrSpan = processString.AsSpan();

            List<int> set = new List<int>();
            return set;
        }
    }
}
