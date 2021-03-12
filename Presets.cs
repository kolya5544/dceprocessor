using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DCEprocessor
{
    public class Presets
    {
        public static List<Message> FindStr(List<Message> messages, string toFind)
        {
            List<Message> mlist = new List<Message>();
            foreach (Message m in messages)
            {
                if (m.Content.ToLower().Contains(toFind.ToLower())) { mlist.Add(m); }
            }
            return mlist;
        }

        public static int CalcStr(List<Message> messages, string toFind)
        {
            int am = 0;
            foreach (Message m in messages)
            {
                if (m.Content.ToLower().Contains(toFind.ToLower())) { am++; }
            }
            return am;
        }

        public static List<Message> ByTime(List<Message> messages, long ts, long maxDev)
        {
            List<Message> mlist = new List<Message>();
            foreach (Message m in messages)
            {
                if (Math.Abs(m.UnixTime - ts) < maxDev) { mlist.Add(m); }
            }
            return mlist;
        }
    }
}
