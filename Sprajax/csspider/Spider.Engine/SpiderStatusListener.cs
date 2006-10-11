using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Engine
{
    public interface SpiderStatusListener
    {
        void SetLastURL(string str);

        void AppendLogNote(string str);

        void SetElapsedTime(string str);

        void SetProcessedCount(string str);
    }
}
