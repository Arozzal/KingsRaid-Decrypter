using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections;
using System.Collections.Generic;

namespace MiniJSON
{
    // Token: 0x02001E28 RID: 7720
    public class JsonPool
    {
        // Token: 0x17002E22 RID: 11810
        // (get) Token: 0x0600FB53 RID: 64339 RVA: 0x0038E8F2 File Offset: 0x0038CCF2
        public static JsonPool instance
        {
            get
            {
                JsonPool.CreateInstance();
                return JsonPool._instance;
            }
        }

        // Token: 0x0600FB54 RID: 64340 RVA: 0x0038E900 File Offset: 0x0038CD00
        public static void CreateInstance()
        {
            if (JsonPool._instance != null)
            {
                return;
            }
            object sync = JsonPool.Sync;
            lock (sync)
            {
                if (JsonPool._instance == null)
                {
                    JsonPool._instance = new JsonPool();
                }
            }
        }

        // Token: 0x0600FB55 RID: 64341 RVA: 0x0038E954 File Offset: 0x0038CD54
        public List<object> CreateList()
        {
            object listQueue = this._listQueue;
            lock (listQueue)
            {
                if (this._listQueue.Count > 0)
                {
                    return this._listQueue.Dequeue();
                }
            }
            return new List<object>();
        }

        // Token: 0x0600FB56 RID: 64342 RVA: 0x0038E9B4 File Offset: 0x0038CDB4
        public List<object> CreateList(int capacity)
        {
            List<object> list = this.CreateList();
            if (list.Capacity < capacity)
            {
                list.Capacity = capacity;
            }
            return list;
        }

        // Token: 0x0600FB57 RID: 64343 RVA: 0x0038E9DC File Offset: 0x0038CDDC
        public Dictionary<string, object> CreateDic(int capacity)
        {
            object dicQueue = this._dicQueue;
            lock (dicQueue)
            {
                if (this._dicQueue.Count > 0)
                {
                    return this._dicQueue.Dequeue();
                }
            }
            return new Dictionary<string, object>(capacity);
        }

        // Token: 0x0600FB58 RID: 64344 RVA: 0x0038EA40 File Offset: 0x0038CE40
        private void ReleaseList(IList list)
        {
            List<object> list2 = list as List<object>;
            if (list2 == null)
            {
                return;
            }
            object listQueue = this._listQueue;
            lock (listQueue)
            {
                this._listQueue.Enqueue(list2);
            }
        }

        // Token: 0x0600FB59 RID: 64345 RVA: 0x0038EA90 File Offset: 0x0038CE90
        private void ReleaseDic(IDictionary dic)
        {
            Dictionary<string, object> dictionary = dic as Dictionary<string, object>;
            if (dictionary == null)
            {
                return;
            }
            object dicQueue = this._dicQueue;
            lock (dicQueue)
            {
                this._dicQueue.Enqueue(dictionary);
            }
        }

        // Token: 0x0600FB5A RID: 64346 RVA: 0x0038EAE0 File Offset: 0x0038CEE0
        public void Release(object value)
        {
            IList list = value as IList;
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    object value2 = list[i];
                    this.Release(value2);
                }
                list.Clear();
                this.ReleaseList(list);
                return;
            }
            IDictionary dictionary = value as IDictionary;
            if (dictionary != null)
            {
                IEnumerator enumerator = dictionary.Values.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        object value3 = enumerator.Current;
                        this.Release(value3);
                    }
                }
                finally
                {
                    IDisposable disposable;
                    if ((disposable = (enumerator as IDisposable)) != null)
                    {
                        disposable.Dispose();
                    }
                }
                dictionary.Clear();
                this.ReleaseDic(dictionary);
                return;
            }
        }

        // Token: 0x0600FB5B RID: 64347 RVA: 0x0038EBA4 File Offset: 0x0038CFA4
        public void OnSceneChanged()
        {
            object listQueue = this._listQueue;
            lock (listQueue)
            {
                this._listQueue.Clear();
            }
            object dicQueue = this._dicQueue;
            lock (dicQueue)
            {
                this._dicQueue.Clear();
            }
        }

        // Token: 0x04008C6B RID: 35947
        private static object Sync = new object();

        // Token: 0x04008C6C RID: 35948
        private static JsonPool _instance;

        // Token: 0x04008C6D RID: 35949
        private Queue<List<object>> _listQueue = new Queue<List<object>>();

        // Token: 0x04008C6E RID: 35950
        private Queue<Dictionary<string, object>> _dicQueue = new Queue<Dictionary<string, object>>();
    }
}
