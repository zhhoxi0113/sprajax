using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Engine
{
    public class ConsoleSpiderStatusListener : SpiderStatusListener
    {
        #region SpiderStatusListener Members

        public void SetLastURL(string str)
        {
            System.Console.WriteLine("(Spider)LastURL: {0}", str);
        }

        public void AppendLogNote(string str)
        {
            System.Console.WriteLine("(Spider)LogNote: {0}", str);
        }

        public void SetElapsedTime(string str)
        {
            System.Console.WriteLine("(Spider)ElapsedTime: {0}", str);
        }

        public void SetProcessedCount(string str)
        {
            System.Console.WriteLine("(Spider)ProcessedCount: {0}", str);
        }

        #endregion
    }
}
