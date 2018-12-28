﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary
{
    public sealed class ProxyPublishData
    {
        public string m_EventName;
        public string m_Group;
        public object[] m_Args;
    }
    public sealed class PublishSubscribeSystem
    {
        private class ReceiptInfo
        {
            public string name_;
            public string group_;
            public Delegate delegate_;
            public ReceiptInfo() { }
            public ReceiptInfo(string n, string g, Delegate d)
            {
                name_ = n;
                group_ = g;
                delegate_ = d;
            }
        }

        public object Subscribe(string ev_name, string group, MyAction subscriber) { return AddSubscriber(ev_name, group, subscriber); }

        public object Subscribe<T1>(string ev_name, string group, MyAction<T1> subscriber) { var d = BuildDelegate<T1>(subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe<T1, T2>(string ev_name, string group, MyAction<T1, T2> subscriber) { var d = BuildDelegate<T1, T2>(subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe<T1, T2, T3>(string ev_name, string group, MyAction<T1, T2, T3> subscriber) { var d = BuildDelegate<T1, T2, T3>(subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe<T1, T2, T3, T4>(string ev_name, string group, MyAction<T1, T2, T3, T4> subscriber) { var d = BuildDelegate<T1, T2, T3, T4>(subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe<T1, T2, T3, T4, T5>(string ev_name, string group, MyAction<T1, T2, T3, T4, T5> subscriber) { var d = BuildDelegate<T1, T2, T3, T4, T5>(subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe<T1, T2, T3, T4, T5, T6>(string ev_name, string group, MyAction<T1, T2, T3, T4, T5, T6> subscriber) { var d = BuildDelegate<T1, T2, T3, T4, T5, T6>(subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe<T1, T2, T3, T4, T5, T6, T7>(string ev_name, string group, MyAction<T1, T2, T3, T4, T5, T6, T7> subscriber) { var d = BuildDelegate<T1, T2, T3, T4, T5, T6, T7>(subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe<T1, T2, T3, T4, T5, T6, T7, T8>(string ev_name, string group, MyAction<T1, T2, T3, T4, T5, T6, T7, T8> subscriber) { var d = BuildDelegate<T1, T2, T3, T4, T5, T6, T7, T8>(subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string ev_name, string group, MyAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> subscriber) { var d = BuildDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(subscriber); return AddSubscriber(ev_name, group, d); }

        public object Subscribe(Type t1, string ev_name, string group, MyAction<object> subscriber) { var d = BuildDelegate(t1, subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe(Type t1, Type t2, string ev_name, string group, MyAction<object, object> subscriber) { var d = BuildDelegate(t1, t2, subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe(Type t1, Type t2, Type t3, string ev_name, string group, MyAction<object, object, object> subscriber) { var d = BuildDelegate(t1, t2, t3, subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe(Type t1, Type t2, Type t3, Type t4, string ev_name, string group, MyAction<object, object, object, object> subscriber) { var d = BuildDelegate(t1, t2, t3, t4, subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe(Type t1, Type t2, Type t3, Type t4, Type t5, string ev_name, string group, MyAction<object, object, object, object, object> subscriber) { var d = BuildDelegate(t1, t2, t3, t4, t5, subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe(Type t1, Type t2, Type t3, Type t4, Type t5, Type t6, string ev_name, string group, MyAction<object, object, object, object, object, object> subscriber) { var d = BuildDelegate(t1, t2, t3, t4, t5, t6, subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe(Type t1, Type t2, Type t3, Type t4, Type t5, Type t6, Type t7, string ev_name, string group, MyAction<object, object, object, object, object, object, object> subscriber) { var d = BuildDelegate(t1, t2, t3, t4, t5, t6, t7, subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe(Type t1, Type t2, Type t3, Type t4, Type t5, Type t6, Type t7, Type t8, string ev_name, string group, MyAction<object, object, object, object, object, object, object, object> subscriber) { var d = BuildDelegate(t1, t2, t3, t4, t5, t6, t7, t8, subscriber); return AddSubscriber(ev_name, group, d); }
        public object Subscribe(Type t1, Type t2, Type t3, Type t4, Type t5, Type t6, Type t7, Type t8, Type t9, string ev_name, string group, MyAction<object, object, object, object, object, object, object, object, object> subscriber) { var d = BuildDelegate(t1, t2, t3, t4, t5, t6, t7, t8, t9, subscriber); return AddSubscriber(ev_name, group, d); }
        
        public void Unsubscribe(object receipt)
        {
            ReceiptInfo r = receipt as ReceiptInfo;
			List<Delegate> list;
            if (null != r){
                Dictionary<string, List<Delegate>> dict;
                if (subscribers_.TryGetValue(r.group_, out dict) && dict.TryGetValue(r.name_, out list)) {
					if (list != null)
					{
						list.Remove(r.delegate_);
						if (list.Count == 0) {
							dict.Remove(r.name_);
							if (dict.Count <= 0)
								subscribers_.Remove(r.group_);
						}
					}
                }
            }
        }

        public void Publish(string ev_name, string group, params object[] parameters)
        {
            try {
                //LogSystem.Info("Publish {0} {1}", ev_name, group);

				List<Delegate> list;
                Dictionary<string, List<Delegate>> dict;
                if (subscribers_.TryGetValue(group, out dict) && dict.TryGetValue(ev_name, out list)) {
                    if (null == list) {
                        LogSystem.Error("Publish {0} {1}, Subscriber is null, Remove it", ev_name, group);
                        dict.Remove(ev_name);
                        if (dict.Count <= 0)
                            subscribers_.Remove(group);
                    } else if (list.Count > 0) {
                        if (list.Count > 1) {
                            //拷贝到临时列表，防止重入操作修改list
                            var temp = new List<Delegate>();
                            temp.AddRange(list);
                            for (int i = 0; i < temp.Count; ++i) {
                                temp[i].DynamicInvoke(parameters);
                            }
                        } else {
                            list[0].DynamicInvoke(parameters);
                        }
                    }
                }
            } catch (Exception ex) {
                if (null != ex.InnerException) {
                    ex = ex.InnerException;
                }
                LogSystem.Error("PublishSubscribe.Publish({0},{1}) exception:{2}\n{3}", ev_name, group, ex.Message, ex.StackTrace);
            }
        }

        private Delegate BuildDelegate(Type t1, MyAction<object> action)
        {
            return (MyAction<object>)((object p1) => {
                var a1 = Helper.ConvertTo(p1, t1);
                action(a1);
            });
        }
        private Delegate BuildDelegate(Type t1, Type t2, MyAction<object, object> action)
        {
            return (MyAction<object, object>)((object p1, object p2) => {
                var a1 = Helper.ConvertTo(p1, t1);
                var a2 = Helper.ConvertTo(p2, t2);
                action(a1, a2);
            });
        }
        private Delegate BuildDelegate(Type t1, Type t2, Type t3, MyAction<object, object, object> action)
        {
            return (MyAction<object, object, object>)((object p1, object p2, object p3) => {
                var a1 = Helper.ConvertTo(p1, t1);
                var a2 = Helper.ConvertTo(p2, t2);
                var a3 = Helper.ConvertTo(p3, t3);
                action(a1, a2, a3);
            });
        }
        private Delegate BuildDelegate(Type t1, Type t2, Type t3, Type t4, MyAction<object, object, object, object> action)
        {
            return (MyAction<object, object, object, object>)((object p1, object p2, object p3, object p4) => {
                var a1 = Helper.ConvertTo(p1, t1);
                var a2 = Helper.ConvertTo(p2, t2);
                var a3 = Helper.ConvertTo(p3, t3);
                var a4 = Helper.ConvertTo(p4, t4);
                action(a1, a2, a3, a4);
            });
        }
        private Delegate BuildDelegate(Type t1, Type t2, Type t3, Type t4, Type t5, MyAction<object, object, object, object, object> action)
        {
            return (MyAction<object, object, object, object, object>)((object p1, object p2, object p3, object p4, object p5) => {
                var a1 = Helper.ConvertTo(p1, t1);
                var a2 = Helper.ConvertTo(p2, t2);
                var a3 = Helper.ConvertTo(p3, t3);
                var a4 = Helper.ConvertTo(p4, t4);
                var a5 = Helper.ConvertTo(p5, t5);
                action(a1, a2, a3, a4, a5);
            });
        }
        private Delegate BuildDelegate(Type t1, Type t2, Type t3, Type t4, Type t5, Type t6, MyAction<object, object, object, object, object, object> action)
        {
            return (MyAction<object, object, object, object, object, object>)((object p1, object p2, object p3, object p4, object p5, object p6) => {
                var a1 = Helper.ConvertTo(p1, t1);
                var a2 = Helper.ConvertTo(p2, t2);
                var a3 = Helper.ConvertTo(p3, t3);
                var a4 = Helper.ConvertTo(p4, t4);
                var a5 = Helper.ConvertTo(p5, t5);
                var a6 = Helper.ConvertTo(p6, t6);
                action(a1, a2, a3, a4, a5, a6);
            });
        }
        private Delegate BuildDelegate(Type t1, Type t2, Type t3, Type t4, Type t5, Type t6, Type t7, MyAction<object, object, object, object, object, object, object> action)
        {
            return (MyAction<object, object, object, object, object, object, object>)((object p1, object p2, object p3, object p4, object p5, object p6, object p7) => {
                var a1 = Helper.ConvertTo(p1, t1);
                var a2 = Helper.ConvertTo(p2, t2);
                var a3 = Helper.ConvertTo(p3, t3);
                var a4 = Helper.ConvertTo(p4, t4);
                var a5 = Helper.ConvertTo(p5, t5);
                var a6 = Helper.ConvertTo(p6, t6);
                var a7 = Helper.ConvertTo(p7, t7);
                action(a1, a2, a3, a4, a5, a6, a7);
            });
        }
        private Delegate BuildDelegate(Type t1, Type t2, Type t3, Type t4, Type t5, Type t6, Type t7, Type t8, MyAction<object, object, object, object, object, object, object, object> action)
        {
            return (MyAction<object, object, object, object, object, object, object, object>)((object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8) => {
                var a1 = Helper.ConvertTo(p1, t1);
                var a2 = Helper.ConvertTo(p2, t2);
                var a3 = Helper.ConvertTo(p3, t3);
                var a4 = Helper.ConvertTo(p4, t4);
                var a5 = Helper.ConvertTo(p5, t5);
                var a6 = Helper.ConvertTo(p6, t6);
                var a7 = Helper.ConvertTo(p7, t7);
                var a8 = Helper.ConvertTo(p8, t8);
                action(a1, a2, a3, a4, a5, a6, a7, a8);
            });
        }
        private Delegate BuildDelegate(Type t1, Type t2, Type t3, Type t4, Type t5, Type t6, Type t7, Type t8, Type t9, MyAction<object, object, object, object, object, object, object, object, object> action)
        {
            return (MyAction<object, object, object, object, object, object, object, object, object>)((object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9) => {
                var a1 = Helper.ConvertTo(p1, t1);
                var a2 = Helper.ConvertTo(p2, t2);
                var a3 = Helper.ConvertTo(p3, t3);
                var a4 = Helper.ConvertTo(p4, t4);
                var a5 = Helper.ConvertTo(p5, t5);
                var a6 = Helper.ConvertTo(p6, t6);
                var a7 = Helper.ConvertTo(p7, t7);
                var a8 = Helper.ConvertTo(p8, t8);
                var a9 = Helper.ConvertTo(p9, t9);
                action(a1, a2, a3, a4, a5, a6, a7, a8, a9);
            });
        }
        private Delegate BuildDelegate<T1>(MyAction<T1> action)
        {
            return (MyAction<object>)((object p1) => {
                var a1 = (T1)Helper.ConvertTo(p1, typeof(T1));
                action(a1);
            });
        }
        private Delegate BuildDelegate<T1,T2>(MyAction<T1,T2> action)
        {
            return (MyAction<object,object>)((object p1,object p2) => {
                var a1 = (T1)Helper.ConvertTo(p1, typeof(T1));
                var a2 = (T2)Helper.ConvertTo(p2, typeof(T2));
                action(a1,a2);
            });
        }
        private Delegate BuildDelegate<T1, T2, T3>(MyAction<T1, T2, T3> action)
        {
            return (MyAction<object, object, object>)((object p1, object p2, object p3) => {
                var a1 = (T1)Helper.ConvertTo(p1, typeof(T1));
                var a2 = (T2)Helper.ConvertTo(p2, typeof(T2));
                var a3 = (T3)Helper.ConvertTo(p3, typeof(T3));
                action(a1, a2, a3);
            });
        }
        private Delegate BuildDelegate<T1, T2, T3, T4>(MyAction<T1, T2, T3, T4> action)
        {
            return (MyAction<object, object, object, object>)((object p1, object p2, object p3, object p4) => {
                var a1 = (T1)Helper.ConvertTo(p1, typeof(T1));
                var a2 = (T2)Helper.ConvertTo(p2, typeof(T2));
                var a3 = (T3)Helper.ConvertTo(p3, typeof(T3));
                var a4 = (T4)Helper.ConvertTo(p4, typeof(T4));
                action(a1, a2, a3, a4);
            });
        }
        private Delegate BuildDelegate<T1, T2, T3, T4, T5>(MyAction<T1, T2, T3, T4, T5> action)
        {
            return (MyAction<object, object, object, object, object>)((object p1, object p2, object p3, object p4, object p5) => {
                var a1 = (T1)Helper.ConvertTo(p1, typeof(T1));
                var a2 = (T2)Helper.ConvertTo(p2, typeof(T2));
                var a3 = (T3)Helper.ConvertTo(p3, typeof(T3));
                var a4 = (T4)Helper.ConvertTo(p4, typeof(T4));
                var a5 = (T5)Helper.ConvertTo(p5, typeof(T5));
                action(a1, a2, a3, a4, a5);
            });
        }
        private Delegate BuildDelegate<T1, T2, T3, T4, T5, T6>(MyAction<T1, T2, T3, T4, T5, T6> action)
        {
            return (MyAction<object, object, object, object, object, object>)((object p1, object p2, object p3, object p4, object p5, object p6) => {
                var a1 = (T1)Helper.ConvertTo(p1, typeof(T1));
                var a2 = (T2)Helper.ConvertTo(p2, typeof(T2));
                var a3 = (T3)Helper.ConvertTo(p3, typeof(T3));
                var a4 = (T4)Helper.ConvertTo(p4, typeof(T4));
                var a5 = (T5)Helper.ConvertTo(p5, typeof(T5));
                var a6 = (T6)Helper.ConvertTo(p6, typeof(T6));
                action(a1, a2, a3, a4, a5, a6);
            });
        }
        private Delegate BuildDelegate<T1, T2, T3, T4, T5, T6, T7>(MyAction<T1, T2, T3, T4, T5, T6, T7> action)
        {
            return (MyAction<object, object, object, object, object, object, object>)((object p1, object p2, object p3, object p4, object p5, object p6, object p7) => {
                var a1 = (T1)Helper.ConvertTo(p1, typeof(T1));
                var a2 = (T2)Helper.ConvertTo(p2, typeof(T2));
                var a3 = (T3)Helper.ConvertTo(p3, typeof(T3));
                var a4 = (T4)Helper.ConvertTo(p4, typeof(T4));
                var a5 = (T5)Helper.ConvertTo(p5, typeof(T5));
                var a6 = (T6)Helper.ConvertTo(p6, typeof(T6));
                var a7 = (T7)Helper.ConvertTo(p7, typeof(T7));
                action(a1, a2, a3, a4, a5, a6, a7);
            });
        }
        private Delegate BuildDelegate<T1, T2, T3, T4, T5, T6, T7, T8>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            return (MyAction<object, object, object, object, object, object, object, object>)((object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8) => {
                var a1 = (T1)Helper.ConvertTo(p1, typeof(T1));
                var a2 = (T2)Helper.ConvertTo(p2, typeof(T2));
                var a3 = (T3)Helper.ConvertTo(p3, typeof(T3));
                var a4 = (T4)Helper.ConvertTo(p4, typeof(T4));
                var a5 = (T5)Helper.ConvertTo(p5, typeof(T5));
                var a6 = (T6)Helper.ConvertTo(p6, typeof(T6));
                var a7 = (T7)Helper.ConvertTo(p7, typeof(T7));
                var a8 = (T8)Helper.ConvertTo(p8, typeof(T8));
                action(a1, a2, a3, a4, a5, a6, a7, a8);
            });
        }
        private Delegate BuildDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            return (MyAction<object, object, object, object, object, object, object, object, object>)((object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9) => {
                var a1 = (T1)Helper.ConvertTo(p1, typeof(T1));
                var a2 = (T2)Helper.ConvertTo(p2, typeof(T2));
                var a3 = (T3)Helper.ConvertTo(p3, typeof(T3));
                var a4 = (T4)Helper.ConvertTo(p4, typeof(T4));
                var a5 = (T5)Helper.ConvertTo(p5, typeof(T5));
                var a6 = (T6)Helper.ConvertTo(p6, typeof(T6));
                var a7 = (T7)Helper.ConvertTo(p7, typeof(T7));
                var a8 = (T8)Helper.ConvertTo(p8, typeof(T8));
                var a9 = (T9)Helper.ConvertTo(p9, typeof(T9));
                action(a1, a2, a3, a4, a5, a6, a7, a8,a9);
            });
        }

        private object AddSubscriber(string ev_name, string group, Delegate d)
        {
            Dictionary<string, List<Delegate>> dict;
            if (!subscribers_.TryGetValue(group, out dict)) {
                dict = new Dictionary<string, List<Delegate>>();
                subscribers_.Add(group, dict);
            }
            List<Delegate> source;
            if (dict.TryGetValue(ev_name, out source)) {
				source.Add(d);
            } else {
				source = new List<Delegate>();
				source.Add(d);
                dict.Add(ev_name, source);
            }
            return new ReceiptInfo(ev_name, group, d);
        }

        private Dictionary<string, Dictionary<string, List<Delegate>>> subscribers_ = new Dictionary<string, Dictionary<string, List<Delegate>>>();
    }
}