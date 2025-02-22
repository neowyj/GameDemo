﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GameLibrary.GmCommands;
using GameLibrary.Story;
using GameLibrary;

namespace GameLibrary
{
    public partial class SceneSystem
    {
        private void TickAi(long curTime, long deltaTime)
        {
            for (int i = m_EntitiesForAi.Count - 1; i >= 0; --i) {
                var info = m_EntitiesForAi[i];                
                if (info.GetAIEnable()) {
                    var aiStateInfo = info.GetAiStateInfo();
                    switch (aiStateInfo.CurState) {
                        case (int)PredefinedAiStateId.MoveCommand:
                            OnAiMoveCommand(info, deltaTime);
                            break;
                        case (int)PredefinedAiStateId.WaitCommand:
                            OnAiWaitCommand(info, deltaTime);
                            break;
                        case (int)PredefinedAiStateId.Idle:
                        default:
                            if (null != aiStateInfo.AiStoryInstanceInfo) {
                                var storyInstance = aiStateInfo.AiStoryInstanceInfo.m_StoryInstance;
                                if (null != storyInstance) {
                                    storyInstance.Tick(curTime);
                                }
                            }
                            break;
                    }
                }
            }
        }
        private void OnAiInitDslLogic(EntityInfo npc)
        {
            AiStateInfo aiInfo = npc.GetAiStateInfo();
            string storyId = aiInfo.AiLogic;
            string storyFile = aiInfo.AiParam[0];
            if (!string.IsNullOrEmpty(storyId) && !string.IsNullOrEmpty(storyFile)) {
                aiInfo.HomePos = npc.GetMovementStateInfo().GetPosition3D();
                aiInfo.ChangeToState((int)PredefinedAiStateId.Idle);
                aiInfo.AiStoryInstanceInfo = ClientStorySystem.Instance.NewAiStoryInstance(storyId, string.Empty, storyFile);
                if (null != aiInfo.AiStoryInstanceInfo) {
                    aiInfo.AiStoryInstanceInfo.m_StoryInstance.SetVariable("@objid", npc.GetId());
                    aiInfo.AiStoryInstanceInfo.m_StoryInstance.Start();
                }
            }
            m_EntitiesForAi.Add(npc);
        }
        private void OnAiDestroy(EntityInfo npc)
        {
            m_EntitiesForAi.Remove(npc);
            var aiStateInfo = npc.GetAiStateInfo();
            var aiStoryInfo = aiStateInfo.AiStoryInstanceInfo;
            if (null != aiStoryInfo) {
                aiStoryInfo.Recycle();
                aiStateInfo.AiStoryInstanceInfo = null;
            }
        }
        private void OnAiMoveCommand(EntityInfo npc, long deltaTime)
        {
            if (!npc.IsDead()) {
                DoMoveCommandState(npc, deltaTime);
            }
        }
        private void OnAiWaitCommand(EntityInfo npc, long deltaTime)
        {
            if (npc.GetMovementStateInfo().IsMoving) {
                AiStopPursue(npc);
            }
        }
        
        private static void DoMoveCommandState(EntityInfo npc, long deltaTime)
        {
            //执行状态处理
            AiData_ForMoveCommand data = GetAiDataForMoveCommand(npc);
            if (null == data) return;

            if (!data.IsFinish) {
                if (WayPointArrived(npc, data)) {
                    UnityEngine.Vector3 targetPos = new UnityEngine.Vector3();
                    MoveToNext(npc, data, ref targetPos);
                    if (!data.IsFinish) {
                        AiPursue(npc, targetPos);
                    }
                } else {
                    UnityEngine.Vector3 targetPos = data.WayPoints[data.Index];
                    AiPursue(npc, targetPos);
                }
            }

            //判断是否状态结束并执行相应处理
            if (data.IsFinish) {
                if (!string.IsNullOrEmpty(data.Event)) {
                    ClientStorySystem.Instance.SendMessage(data.Event, npc.GetId(), npc.GetUnitId());
                }
                AiStopPursue(npc);
                npc.GetAiStateInfo().ChangeToState((int)PredefinedAiStateId.Idle);
            }
        }
        private static void MoveToNext(EntityInfo charObj, AiData_ForMoveCommand data, ref UnityEngine.Vector3 targetPos)
        {
            if (++data.Index >= data.WayPoints.Count) {
                data.IsFinish = true;
                return;
            }

            var move_info = charObj.GetMovementStateInfo();
            targetPos = data.WayPoints[data.Index];
            charObj.GetAiStateInfo().TargetPosition = targetPos;
            float rot = Geometry.GetYRadian(move_info.GetPosition3D(), targetPos);
            move_info.SetWantedFaceDir(rot);
        }
        private static bool WayPointArrived(EntityInfo charObj, AiData_ForMoveCommand data)
        {
            bool ret = false;
            var move_info = charObj.GetMovementStateInfo();
            float powDistDest = Geometry.DistanceSquare(move_info.GetPosition3D(), charObj.GetAiStateInfo().TargetPosition);
            if (powDistDest <= 1f) {
                ret = true;
            }
            return ret;
        }
        private static AiData_ForMoveCommand GetAiDataForMoveCommand(EntityInfo npc)
        {
            AiData_ForMoveCommand data = npc.GetAiStateInfo().AiDatas.GetData<AiData_ForMoveCommand>();
            return data;
        }
        private static void AiPursue(EntityInfo npc, UnityEngine.Vector3 target)
        {
            EntityViewModel npcView = SceneSystem.Instance.EntityViewManager.GetEntityViewById(npc.GetId());
            npcView.MoveTo(target.x, target.y, target.z);
        }
        private static void AiStopPursue(EntityInfo npc)
        {
            EntityViewModel npcView = SceneSystem.Instance.EntityViewManager.GetEntityViewById(npc.GetId());
            npcView.StopMove();
        }
    }
}
