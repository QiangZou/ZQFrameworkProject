using System.Collections.Generic;
using ZQFramwork;

namespace ZQFramwork
{
    public class EventBase
    {

        public delegate void EventFun(System.Object varData);

        Dictionary<int, List<EventFun>> events = new Dictionary<int, List<EventFun>>();

        List<EventFun> deletesEvents = new List<EventFun>();

        ~EventBase()
        {
            events = null;
            deletesEvents = null;
        }



        /// <summary>
        /// 注册事件.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="varFun"></param>
        public void RegisterEventReceiver(int index, EventFun varFun)
        {
            List<EventFun> eventFun = null;

            if (events.TryGetValue(index, out eventFun))
            {
                eventFun.Add(varFun);
            }
            else
            {
                eventFun = new List<EventFun>();

                eventFun.Add(varFun);

                events.Add(index, eventFun);
            }
        }

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="varData"></param>
        public void DispatchEvents(int index, System.Object varData)
        {
            if (events.ContainsKey(index) == false)
            {
                return;
            }

            List<EventFun> eventsFun = events[index];

            if (eventsFun == null)
            {
                return;
            }


            //清理为null的事件.
            deletesEvents.Clear();

            for (int i = 0; i < eventsFun.Count; i++)
            {
                if (eventsFun[i] == null)
                {
                    deletesEvents.Add(eventsFun[i]);
                }
            }

            for (int i = 0; i < deletesEvents.Count; i++)
            {
                eventsFun.Remove(deletesEvents[i]);
            }


            for (int i = 0; i < eventsFun.Count; i++)
            {
                eventsFun[i](varData);
            }

        }

        /// <summary>
        /// 取消注册事件监听.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="varFun"></param>
        public void UnRegisterEventReceiver(int index, EventFun varFun)
        {
            if (events.ContainsKey(index))
            {
                events[index].Remove(varFun);
            }
        }


    }

}
