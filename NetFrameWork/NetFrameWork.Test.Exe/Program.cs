using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrameWork.Common.Code;
using NetFrameWork.Common.Extension;
using NetFrameWork.Common.Utility;

namespace NetFrameWork.Test.Exe
{
    class Program
    {
        static void Main(string[] args)
        {
            //            var url = "http://localhost:58112/api/DeviceApi/QueryRelayStatusView";
            //            string deviceId = "123123";
            //            var res = new HttpHelper().HttpPost(url, deviceId,"",Encoding.UTF8, false,false,1000);

            //var id = KeyIdFactory.NewKeyId();

            byte b1 = 0x01;
            byte b2 = 0x02;
            byte b3 = 0x03;
            byte b4 = 0x04;
            byte[] req = new[] { b1, b2, b3, b4 };

            var url = "http://localhost:1786/Api/ByteTest";

            var t = new HttpHelper().HttpPost(url, "", req, Encoding.Default, false, false, 300000);

            int rt = 0;
        }
    }
}
