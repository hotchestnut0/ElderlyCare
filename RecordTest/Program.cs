using iFlyRecorder;

namespace RecordTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OnlineRecognition iFlyRecord = new("appid = 35775cb6, work_dir = .");
            iFlyRecord.StartRecord();
            iFlyRecord.ResultArrived += IFlyRecord_ResultArrived;

            Console.WriteLine("按任意键停止");
            Console.ReadKey();

            iFlyRecord.StopRecord();
        }

        private static void IFlyRecord_ResultArrived(string result, bool isLast)
        {
            Console.WriteLine("识别结果：" + result);
        }
    }
}
