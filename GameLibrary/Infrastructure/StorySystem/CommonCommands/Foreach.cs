﻿using System;
using System.Collections;
using System.Collections.Generic;
namespace StorySystem.CommonCommands
{
    /// <summary>
    /// foreach(v1,v2,v3)
    /// {
    ///   createnpc($$);
    ///   wait(100);
    /// };
    /// </summary>
    internal sealed class ForeachCommand : AbstractStoryCommand
    {
        protected override IStoryCommand CloneCommand()
        {
            ForeachCommand retCmd = new ForeachCommand();
            retCmd.m_LocalInfoIndex = m_LocalInfoIndex;
            for (int i = 0; i < m_LoadedIterators.Count; i++) {
                retCmd.m_LoadedIterators.Add(m_LoadedIterators[i].Clone());
            }
            for (int i = 0; i < m_LoadedCommands.Count; i++) {
                retCmd.m_LoadedCommands.Add(m_LoadedCommands[i].Clone());
            }
            retCmd.IsCompositeCommand = true;
            return retCmd;
        }
        protected override void ResetState()
        {
        }
        protected override void Evaluate(StoryInstance instance, StoryMessageHandler handler, object iterator, object[] args)
        {
            var localInfos = handler.LocalInfoStack.Peek();
            var localInfo = localInfos.GetLocalInfo(m_LocalInfoIndex) as LocalInfo;
            if (localInfo.Iterators.Count <= 0 && localInfo.List.Count > 0) {
                for (int i = 0; i < localInfo.List.Count; i++) {
                    localInfo.List[i].Evaluate(instance, handler, iterator, args);
                }
                for (int i = 0; i < localInfo.List.Count; i++) {
                    localInfo.Iterators.Enqueue(localInfo.List[i].Value);
                }
            }
        }
        protected override bool ExecCommand(StoryInstance instance, StoryMessageHandler handler, long delta, object iterator, object[] args)
        {
            var localInfos = handler.LocalInfoStack.Peek();
            var localInfo = localInfos.GetLocalInfo(m_LocalInfoIndex) as LocalInfo;
            if (null == localInfo) {
                localInfo = new LocalInfo { List = new List<IStoryValue>() };
                for (int i = 0; i < m_LoadedIterators.Count; ++i) {
                    localInfo.List.Add(m_LoadedIterators[i].Clone());
                }
                localInfos.SetLocalInfo(m_LocalInfoIndex, localInfo);
            }
            if (!handler.PeekRuntime().CompositeReentry) {
                Evaluate(instance, handler, iterator, args);
            }
            bool ret = true;
            while (ret) {
                if (localInfo.Iterators.Count > 0) {
                    Prepare(handler.RuntimeStack);
                    var runtime = handler.PeekRuntime();
                    runtime.Iterator = localInfo.Iterators.Dequeue();
                    runtime.Arguments = args;
                    ret = true;
                    //没有wait之类命令直接执行
                    runtime.Tick(instance, handler, delta);
                    if (runtime.CommandQueue.Count == 0) {
                        handler.PopRuntime();
                    } else {
                        //遇到wait命令，跳出执行，之后直接在StoryMessageHandler里执行栈顶的命令队列（降低开销）
                        break;
                    }
                } else {
                    ret = false;
                }
            }
            return ret;
        }
        protected override void Load(Dsl.FunctionData functionData)
        {
            m_LocalInfoIndex = StoryCommandManager.Instance.AllocLocalInfoIndex();
            Dsl.CallData callData = functionData.Call;
            if (null != callData) {
                for (int i = 0; i < callData.GetParamNum(); ++i) {
                    Dsl.ISyntaxComponent param = callData.GetParam(i);
                    StoryValue val = new StoryValue();
                    val.InitFromDsl(param);
                    m_LoadedIterators.Add(val);
                }
                for (int i = 0; i < functionData.Statements.Count; i++) {
                    IStoryCommand cmd = StoryCommandManager.Instance.CreateCommand(functionData.Statements[i]);
                    if (null != cmd)
                        m_LoadedCommands.Add(cmd);
                }
            }
            IsCompositeCommand = true;
        }
        private void Prepare(StoryRuntimeStack runtimeStack)
        {
            var runtime = StoryRuntime.New();
            runtimeStack.Push(runtime);
            var queue = runtime.CommandQueue;
            foreach (IStoryCommand cmd in queue) {
                cmd.Reset();
            }
            queue.Clear();
            for (int i = 0; i < m_LoadedCommands.Count; i++) {
                IStoryCommand cmd = m_LoadedCommands[i];
                if (null != cmd.LeadCommand)
                    queue.Enqueue(cmd.LeadCommand);
                queue.Enqueue(cmd);
            }
        }

        private sealed class LocalInfo
        {
            internal Queue<object> Iterators = new Queue<object>();
            internal List<IStoryValue> List = null;
        }

        private int m_LocalInfoIndex;
        private List<IStoryValue> m_LoadedIterators = new List<IStoryValue>();
        private List<IStoryCommand> m_LoadedCommands = new List<IStoryCommand>();
    }
    /// <summary>
    /// looplist(list)
    /// {
    ///   createnpc($$);
    ///   wait(100);
    /// };
    /// </summary>
    internal sealed class LoopListCommand : AbstractStoryCommand
    {
        protected override IStoryCommand CloneCommand()
        {
            LoopListCommand retCmd = new LoopListCommand();
            retCmd.m_LocalInfoIndex = m_LocalInfoIndex;
            retCmd.m_LoadedList = m_LoadedList.Clone();
            for (int i = 0; i < m_LoadedCommands.Count; i++) {
                retCmd.m_LoadedCommands.Add(m_LoadedCommands[i].Clone());
            }
            retCmd.IsCompositeCommand = true;
            return retCmd;
        }
        protected override void ResetState()
        {
        }
        protected override void Evaluate(StoryInstance instance, StoryMessageHandler handler, object iterator, object[] args)
        {
            var localInfos = handler.LocalInfoStack.Peek();
            var localInfo = localInfos.GetLocalInfo(m_LocalInfoIndex) as LocalInfo;
            if (localInfo.Iterators.Count <= 0) {
                localInfo.List.Evaluate(instance, handler, iterator, args);
                foreach (object obj in localInfo.List.Value) {
                    localInfo.Iterators.Enqueue(obj);
                }
            }
        }
        protected override bool ExecCommand(StoryInstance instance, StoryMessageHandler handler, long delta, object iterator, object[] args)
        {
            var localInfos = handler.LocalInfoStack.Peek();
            var localInfo = localInfos.GetLocalInfo(m_LocalInfoIndex) as LocalInfo;
            if (null == localInfo) {
                localInfo = new LocalInfo { List = m_LoadedList.Clone() };
                localInfos.SetLocalInfo(m_LocalInfoIndex, localInfo);
            }
            if (!handler.PeekRuntime().CompositeReentry) {
                Evaluate(instance, handler, iterator, args);
            }
            bool ret = true;
            while (ret) {
                if (localInfo.Iterators.Count > 0) {
                    Prepare(handler.RuntimeStack);
                    var runtime = handler.PeekRuntime();
                    runtime.Iterator = localInfo.Iterators.Dequeue();
                    runtime.Arguments = args;
                    ret = true;
                    //没有wait之类命令直接执行
                    runtime.Tick(instance, handler, delta);
                    if (runtime.CommandQueue.Count == 0) {
                        handler.PopRuntime();
                    } else {
                        //遇到wait命令，跳出执行，之后直接在StoryMessageHandler里执行栈顶的命令队列（降低开销）
                        break;
                    }
                } else {
                    ret = false;
                }
            }
            return ret;
        }
        protected override void Load(Dsl.FunctionData functionData)
        {
            m_LocalInfoIndex = StoryCommandManager.Instance.AllocLocalInfoIndex();
            Dsl.CallData callData = functionData.Call;
            if (null != callData) {
                if (callData.GetParamNum() > 0) {
                    m_LoadedList.InitFromDsl(callData.GetParam(0));
                }
                for (int i = 0; i < functionData.Statements.Count; i++) {
                    IStoryCommand cmd = StoryCommandManager.Instance.CreateCommand(functionData.Statements[i]);
                    if (null != cmd)
                        m_LoadedCommands.Add(cmd);
                }
            }
            IsCompositeCommand = true;
        }
        private void Prepare(StoryRuntimeStack runtimeStack)
        {
            var runtime = StoryRuntime.New();
            runtimeStack.Push(runtime);
            var queue = runtime.CommandQueue;
            foreach (IStoryCommand cmd in queue) {
                cmd.Reset();
            }
            queue.Clear();
            for (int i = 0; i < m_LoadedCommands.Count; i++) {
                IStoryCommand cmd = m_LoadedCommands[i];
                if (null != cmd.LeadCommand)
                    queue.Enqueue(cmd.LeadCommand);
                queue.Enqueue(cmd);
            }
        }

        private sealed class LocalInfo
        {
            internal Queue<object> Iterators = new Queue<object>();
            internal IStoryValue<IEnumerable> List = null;
        }

        private int m_LocalInfoIndex;
        private IStoryValue<IEnumerable> m_LoadedList = new StoryValue<IEnumerable>();
        private List<IStoryCommand> m_LoadedCommands = new List<IStoryCommand>();
    }
    /// <summary>
    /// loop(count)
    /// {
    ///   createnpc($$);
    ///   wait(100);
    /// };
    /// </summary>
    internal sealed class LoopCommand : AbstractStoryCommand
    {
        protected override IStoryCommand CloneCommand()
        {
            LoopCommand retCmd = new LoopCommand();
            retCmd.m_LocalInfoIndex = m_LocalInfoIndex;
            retCmd.m_LoadedCount = m_LoadedCount.Clone();
            for (int i = 0; i < m_LoadedCommands.Count; i++) {
                retCmd.m_LoadedCommands.Add(m_LoadedCommands[i].Clone());
            }
            retCmd.IsCompositeCommand = true;
            return retCmd;
        }
        protected override void ResetState()
        {
        }
        protected override void Evaluate(StoryInstance instance, StoryMessageHandler handler, object iterator, object[] args)
        {
            var localInfos = handler.LocalInfoStack.Peek();
            var localInfo = localInfos.GetLocalInfo(m_LocalInfoIndex) as LocalInfo;
            localInfo.Count.Evaluate(instance, handler, iterator, args);
        }
        protected override bool ExecCommand(StoryInstance instance, StoryMessageHandler handler, long delta, object iterator, object[] args)
        {
            var localInfos = handler.LocalInfoStack.Peek();
            var localInfo = localInfos.GetLocalInfo(m_LocalInfoIndex) as LocalInfo;
            if (null == localInfo) {
                localInfo = new LocalInfo { Count = m_LoadedCount.Clone(), CurCount = 0 };
                localInfos.SetLocalInfo(m_LocalInfoIndex, localInfo);
            }
            if (!handler.PeekRuntime().CompositeReentry) {
                Evaluate(instance, handler, iterator, args);
            }
            bool ret = true;
            while (ret) {
                if (localInfo.CurCount < localInfo.Count.Value) {
                    Prepare(handler.RuntimeStack);
                    var runtime = handler.PeekRuntime();
                    runtime.Iterator = localInfo.CurCount;
                    runtime.Arguments = args;
                    ++localInfo.CurCount;
                    ret = true;
                    //没有wait之类命令直接执行
                    runtime.Tick(instance, handler, delta);
                    if (runtime.CommandQueue.Count == 0) {
                        handler.PopRuntime();
                    } else {
                        //遇到wait命令，跳出执行，之后直接在StoryMessageHandler里执行栈顶的命令队列（降低开销）
                        break;
                    }
                } else {
                    ret = false;
                }
            }
            return ret;
        }
        protected override void Load(Dsl.FunctionData functionData)
        {
            m_LocalInfoIndex = StoryCommandManager.Instance.AllocLocalInfoIndex();
            Dsl.CallData callData = functionData.Call;
            if (null != callData) {
                if (callData.GetParamNum() > 0) {
                    Dsl.ISyntaxComponent param = callData.GetParam(0);
                    m_LoadedCount.InitFromDsl(param);
                }
                for (int i = 0; i < functionData.Statements.Count; i++) {
                    IStoryCommand cmd = StoryCommandManager.Instance.CreateCommand(functionData.Statements[i]);
                    if (null != cmd)
                        m_LoadedCommands.Add(cmd);
                }
            }
            IsCompositeCommand = true;
        }
        private void Prepare(StoryRuntimeStack runtimeStack)
        {
            var runtime = StoryRuntime.New();
            runtimeStack.Push(runtime);
            var queue = runtime.CommandQueue;
            foreach (IStoryCommand cmd in queue) {
                cmd.Reset();
            }
            queue.Clear();
            for (int i = 0; i < m_LoadedCommands.Count; i++) {
                IStoryCommand cmd = m_LoadedCommands[i];
                if (null != cmd.LeadCommand)
                    queue.Enqueue(cmd.LeadCommand);
                queue.Enqueue(cmd);
            }
        }

        private sealed class LocalInfo
        {
            internal IStoryValue<int> Count;
            internal int CurCount;
        }

        private int m_LocalInfoIndex;
        private IStoryValue<int> m_LoadedCount = new StoryValue<int>();
        private List<IStoryCommand> m_LoadedCommands = new List<IStoryCommand>();
    }
}
