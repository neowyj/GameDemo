﻿using System;
using System.Collections.Generic;
using GameLibrary;
namespace StorySystem.CommonValues
{
    internal sealed class TimeValue : IStoryValue
    {
        public void InitFromDsl(Dsl.ISyntaxComponent param)
        {
            Dsl.FunctionData callData = param as Dsl.FunctionData;
            if (null != callData) {
            }
        }
        public IStoryValue Clone()
        {
            TimeValue val = new TimeValue();
            val.m_HaveValue = m_HaveValue;
            val.m_Value = m_Value;
            return val;
        }
        public void Evaluate(StoryInstance instance, StoryMessageHandler handler, BoxedValue iterator, BoxedValueList args)
        {
            m_HaveValue = false;

            m_Value = (int)TimeUtility.GetLocalMilliseconds();
            m_HaveValue = true;
        }
        public void Analyze(StoryInstance instance)
        {
        }
        public bool HaveValue
        {
            get
            {
                return m_HaveValue;
            }
        }
        public BoxedValue Value
        {
            get
            {
                return m_Value;
            }
        }

        private bool m_HaveValue;
        private BoxedValue m_Value;
    }
}
