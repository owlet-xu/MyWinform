using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using D2SPlatform.Core;

namespace SSQ
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                bool debug = false;
                Log.GetInstance().Start(Common.GetSelfPath() + "Logs\\client_", debug ? Log.Level.Debug : Log.Level.Error);
                //连接数据库
                AccessConnectionManager.Config("ssq", Common.GetSelfPath() + "ssq.mdb");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            catch (Exception ex)
            {
                Log.GetInstance().WriteLog(ex.Message, Log.Level.Error);
                Log.GetInstance().WriteLog(ex.StackTrace, Log.Level.Error);
            }
            finally
            {
                Log.GetInstance().Stop();
            }
        }
    }
}
