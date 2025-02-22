﻿using System.Collections.Generic;
using System;
using UnityEngine;

namespace GameLibrary
{
    public sealed class ObjectKdTree
    {
        public const int c_MaxLeafSize = 4;
        public sealed class KdTreeData
        {
            public UnityEngine.GameObject Object;
            public Vector3 Position;
            public float Radius;

            internal float MaxX;
            internal float MinX;
            internal float MaxZ;
            internal float MinZ;
            internal bool Indexed;

            public KdTreeData(UnityEngine.GameObject obj, float radius)
            {
                CopyFrom(obj, radius);
            }
            public void CopyFrom(UnityEngine.GameObject obj, float radius)
            {
                if (null != obj) {
                    Object = obj;
                    Position = obj.transform.position;
                    Radius = radius;
                    MaxX = Position.x + Radius;
                    MinX = Position.x - Radius;
                    MaxZ = Position.z + Radius;
                    MinZ = Position.z - Radius;
                    Indexed = false;
                } else {
                    Object = null;
                    Position = new Vector3();
                    Radius = 0;
                    MaxX = MinX = 0;
                    MaxZ = MinZ = 0;
                    Indexed = false;
                }
            }
        }

        private struct KdTreeNode
        {
            internal int m_Begin;
            internal int m_End;
            internal int m_Left;
            internal int m_Right;
            internal float m_MaxX;
            internal float m_MaxZ;
            internal float m_MinX;
            internal float m_MinZ;
        }

        public void FullBuild(IList<UnityEngine.GameObject> objs, float radius)
        {
            BeginBuild(objs.Count);
            for (int ix = 0; ix < objs.Count; ++ix) {
                AddObjForBuild(objs[ix], radius);
            }
            EndBuild();
        }
        public void Clear()
        {
            m_ObjectNum = 0;
        }
        public void BeginBuild(int count)
        {
            if (null == m_Objects || m_Objects.Length < count) {
                m_Objects = new KdTreeData[count * 2];
            }
            m_ObjectNum = 0;
        }
        public void AddObjForBuild(UnityEngine.GameObject obj, float radius)
        {
            if (m_ObjectNum >= m_Objects.Length)
                return;
            if (null == m_Objects[m_ObjectNum])
                m_Objects[m_ObjectNum] = new KdTreeData(obj, radius);
            else
                m_Objects[m_ObjectNum].CopyFrom(obj, radius);
            ++m_ObjectNum;
        }
        public void EndBuild()
        {
            if (m_ObjectNum > 0) {
                if (null == m_KdTree || m_KdTree.Length < 3 * m_ObjectNum) {
                    m_KdTree = new KdTreeNode[3 * m_ObjectNum];
                    for (int i = 0; i < m_KdTree.Length; ++i) {
                        m_KdTree[i] = new KdTreeNode();
                    }
                }
                m_MaxNodeNum = 2 * m_ObjectNum;
                BuildImpl();
            }
        }

        public void QueryWithAction(float x, float y, float z, float range, MyAction<float, KdTreeData> visitor)
        {
            QueryWithAction(new Vector3(x, y, z), range, visitor);
        }

        public void QueryWithAction(UnityEngine.GameObject obj, float range, MyAction<float, KdTreeData> visitor)
        {
            QueryWithAction(obj.transform.position, range, visitor);
        }
        public void QueryWithAction(Vector3 pos, float range, MyAction<float, KdTreeData> visitor)
        {
            if (null != m_KdTree && m_ObjectNum > 0 && m_KdTree.Length > 0) {
                float rangeSq = Sqr(range);
				QueryImpl(pos, range, rangeSq, visitor);
			}
        }

        public void QueryWithFunc(float x, float y, float z, float range, MyFunc<float, KdTreeData, bool> visitor)
        {
            QueryWithFunc(new Vector3(x, y, z), range, visitor);
        }

        public void QueryWithFunc(UnityEngine.GameObject obj, float range, MyFunc<float, KdTreeData, bool> visitor)
        {
            QueryWithFunc(obj.transform.position, range, visitor);
        }

        public void QueryWithFunc(Vector3 pos, float range, MyFunc<float, KdTreeData, bool> visitor)
        {
            if (null != m_KdTree && m_ObjectNum > 0 && m_KdTree.Length > 0) {
                float rangeSq = Sqr(range);
                QueryImpl(pos, range, rangeSq, visitor);
            }
        }

        public void VisitTreeWithAction(MyAction<float, float, float, float, int, int, KdTreeData[]> visitor)
        {
            if (null != m_KdTree && m_ObjectNum > 0 && m_KdTree.Length > 0) {
                VisitTreeImpl(visitor);
            }
		}

        public void VisitTreeWithFunc(MyFunc<float, float, float, float, int, int, KdTreeData[], bool> visitor)
        {
            if (null != m_KdTree && m_ObjectNum > 0 && m_KdTree.Length > 0) {
                VisitTreeImpl(visitor);
            }
        }

        private void BuildImpl()
        {
            int nextUnusedNode = 1;
            m_BuildStack.Push(0);
            m_BuildStack.Push(m_ObjectNum);
            m_BuildStack.Push(0);
            while (m_BuildStack.Count >= 3) {
                int begin = m_BuildStack.Pop(); //待分类数据对象开始位置
                int end = m_BuildStack.Pop();   //待分类数据对象结束位置的后一个位置
                int node = m_BuildStack.Pop();  //kdtree上用来构造新结点的位置

                KdTreeData obj0 = m_Objects[begin];
                float minX = obj0.MinX;
                float maxX = obj0.MaxX;
                float minZ = obj0.MinZ;
                float maxZ = obj0.MaxZ;
                for (int i = begin + 1; i < end; ++i) {
                    KdTreeData obj = m_Objects[i];
                    float newMaxX = obj.MaxX;
                    float newMinX = obj.MinX;
                    float newMaxZ = obj.MaxZ;
                    float newMinZ = obj.MinZ;
                    if (minX > newMinX) minX = newMinX;
                    if (maxX < newMaxX) maxX = newMaxX;
                    if (minZ > newMinZ) minZ = newMinZ;
                    if (maxZ < newMaxZ) maxZ = newMaxZ;
                }
                m_KdTree[node].m_MinX = minX;
                m_KdTree[node].m_MaxX = maxX;
                m_KdTree[node].m_MinZ = minZ;
                m_KdTree[node].m_MaxZ = maxZ;

                if (end - begin > c_MaxLeafSize) {
                    //kdtree上2个子结点的位置预留
                    m_KdTree[node].m_Left = nextUnusedNode;
                    ++nextUnusedNode;
                    m_KdTree[node].m_Right = nextUnusedNode;
                    ++nextUnusedNode;

                    bool isVertical = (maxX - minX > maxZ - minZ);
                    float splitValue = (isVertical ? 0.5f * (maxX + minX) : 0.5f * (maxZ + minZ));

                    int begin0 = begin;
                    int left = begin;
                    int right = end;

                    //接下来，变量涵义如下：
                    //begin0为当前结点上挂的数据对象的起始位置
                    //begin为当前结点上挂的数据对象的结束位置的后一个位置，同时也是左子树的数据对象的起始位置
                    //left为左子树的数据对象的结束位置的后一个位置，也是待分类数据对象的起始位置
                    //right为当前已确定的右子树数据对象的起始位置
                    //end为右子树数据对象结束位置的后一个位置

                    bool canSplit = false;
                    while (left < right) {
                        while (left < right) {
                            KdTreeData obj = m_Objects[left];
                            if ((isVertical ? obj.MaxX : obj.MaxZ) < splitValue) {
                                //obj为左子树上的数据对象,标记要拆分子树
                                ++left;
                                canSplit = true;
                            } else if ((isVertical ? obj.MinX : obj.MinZ) <= splitValue) {
                                //obj为当前结点上的数据对象，后续要调整begin的数据与begin位置
                                obj.Indexed = true;
                                break;
                            } else {
                                break;
                            }
                        }
                        while (left < right) {
                            KdTreeData obj = m_Objects[right - 1];
                            if ((isVertical ? obj.MinX : obj.MinZ) > splitValue) {
                                //obj为右子树上的数据对象，这里不需要标记拆分
                                --right;
                            } else if ((isVertical ? obj.MaxX : obj.MaxZ) >= splitValue) {
                                //obj为当前结点上的数据对象，后续要调整begin的数据与begin位置
                                obj.Indexed = true;
                                break;
                            } else {
                                break;
                            }
                        }

                        if (left < right) {
                            if (m_Objects[left].Indexed || m_Objects[right - 1].Indexed) {
                                if (m_Objects[left].Indexed) {
                                    KdTreeData tmp = m_Objects[begin];
                                    m_Objects[begin] = m_Objects[left];
                                    m_Objects[left] = tmp;
                                    ++begin;
                                    ++left;
                                    canSplit = true;
                                    //将数据对象挂到当前结点上(数据交换到begin位置)，begin后移一个位置，left也后移一个位置
                                }
                                if (left < right && m_Objects[right - 1].Indexed) {
                                    KdTreeData tmp = m_Objects[begin];
                                    m_Objects[begin] = m_Objects[right - 1];
                                    m_Objects[right - 1] = m_Objects[left];
                                    m_Objects[left] = tmp;
                                    ++begin;
                                    ++left;
                                    canSplit = true;
                                    //将数据对象挂到当前结点上(数据交换到begin位置)，left位置的数据对象放到right-1（继续处理），begin后移一个位置，left也后移一个位置
                                }
                                //处理完要挂接的数据后，继续处理（可能left或right-1位置有一处是不合分类标准的数据）
                            } else {
                                KdTreeData tmp = m_Objects[left];
                                m_Objects[left] = m_Objects[right - 1];
                                m_Objects[right - 1] = tmp;
                                ++left;
                                --right;
                                canSplit = true;
                                //left与right-1位置都是不符合分类标准的数据，交换数据后继续处理
                            }
                        }
                    }

                    if (canSplit) {
                        m_KdTree[node].m_Begin = begin0;
                        m_KdTree[node].m_End = begin;

                        if (left > begin) {
                            m_BuildStack.Push(m_KdTree[node].m_Left);
                            m_BuildStack.Push(left);
                            m_BuildStack.Push(begin);
                        }

                        if (end > left) {
                            m_BuildStack.Push(m_KdTree[node].m_Right);
                            m_BuildStack.Push(end);
                            m_BuildStack.Push(left);
                        }
                    } else {
                        m_KdTree[node].m_Begin = begin0;
                        m_KdTree[node].m_End = begin0;
                        m_KdTree[node].m_Left = 0;
                        m_KdTree[node].m_Right = 0;
                        nextUnusedNode -= 2;
                    }
                } else {
                    m_KdTree[node].m_Begin = begin;
                    m_KdTree[node].m_End = end;
                    m_KdTree[node].m_Left = 0;
                    m_KdTree[node].m_Right = 0;
                }
            }
        }

        private void QueryImpl(Vector3 pos, float range, float rangeSq, MyAction<float, KdTreeData> visitor)
        {
            m_QueryStack.Push(0);
            while (m_QueryStack.Count > 0) {
                int node = m_QueryStack.Pop();
                int begin = m_KdTree[node].m_Begin;
                int end = m_KdTree[node].m_End;
                int left = m_KdTree[node].m_Left;
                int right = m_KdTree[node].m_Right;

                if (end > begin) {
                    for (int i = begin; i < end; ++i) {
                        KdTreeData obj = m_Objects[i];
                        if (Geometry.RectangleOverlapRectangle(pos.x - range, pos.z - range, pos.x + range, pos.z + range, obj.MinX, obj.MinZ, obj.MaxX, obj.MaxZ)) {
                            float distSq = Geometry.DistanceSquare(pos, obj.Position);
                            visitor(distSq, obj);
                        }
                    }
                }

                float minX = m_KdTree[node].m_MinX;
                float minZ = m_KdTree[node].m_MinZ;
                float maxX = m_KdTree[node].m_MaxX;
                float maxZ = m_KdTree[node].m_MaxZ;

                bool isVertical = (maxX - minX > maxZ - minZ);
                float splitValue = (isVertical ? 0.5f * (maxX + minX) : 0.5f * (maxZ + minZ));

                if ((isVertical ? pos.x + range : pos.z + range) < splitValue) {
                    if (left > 0)
                        m_QueryStack.Push(left);
                } else if ((isVertical ? pos.x - range : pos.z - range) < splitValue) {
                    if (left > 0)
                        m_QueryStack.Push(left);
                    if (right > 0)
                        m_QueryStack.Push(right);
                } else {
                    if (right > 0)
                        m_QueryStack.Push(right);
                }
            }
        }
        private void QueryImpl(Vector3 pos, float range, float rangeSq, MyFunc<float, KdTreeData, bool> visitor)
        {
            m_QueryStack.Push(0);
            while (m_QueryStack.Count > 0) {
                int node = m_QueryStack.Pop();
                int begin = m_KdTree[node].m_Begin;
                int end = m_KdTree[node].m_End;
                int left = m_KdTree[node].m_Left;
                int right = m_KdTree[node].m_Right;

                if (end > begin) {
                    for (int i = begin; i < end; ++i) {
                        KdTreeData obj = m_Objects[i];
                        if (Geometry.RectangleOverlapRectangle(pos.x - range, pos.z - range, pos.x + range, pos.z + range, obj.MinX, obj.MinZ, obj.MaxX, obj.MaxZ)) {
                            float distSq = Geometry.DistanceSquare(pos, obj.Position);
                            if (!visitor(distSq, obj)) {
                                m_QueryStack.Clear();
                                return;
                            }
                        }
                    }
                }

                float minX = m_KdTree[node].m_MinX;
                float minZ = m_KdTree[node].m_MinZ;
                float maxX = m_KdTree[node].m_MaxX;
                float maxZ = m_KdTree[node].m_MaxZ;

                bool isVertical = (maxX - minX > maxZ - minZ);
                float splitValue = (isVertical ? 0.5f * (maxX + minX) : 0.5f * (maxZ + minZ));

                if ((isVertical ? pos.x + range : pos.z + range) < splitValue) {
                    if (left > 0)
                        m_QueryStack.Push(left);
                } else if ((isVertical ? pos.x - range : pos.z - range) <= splitValue) {
                    if (left > 0)
                        m_QueryStack.Push(left);
                    if (right > 0)
                        m_QueryStack.Push(right);
                } else {
                    if (right > 0)
                        m_QueryStack.Push(right);
                }
            }
        }

        private void VisitTreeImpl(MyAction<float, float, float, float, int, int, KdTreeData[]> visitor)
        {
            m_QueryStack.Push(0);
            while (m_QueryStack.Count > 0) {
                int node = m_QueryStack.Pop();

                int begin = m_KdTree[node].m_Begin;
                int end = m_KdTree[node].m_End;
                int left = m_KdTree[node].m_Left;
                int right = m_KdTree[node].m_Right;

                float minX = m_KdTree[node].m_MinX;
                float minZ = m_KdTree[node].m_MinZ;
                float maxX = m_KdTree[node].m_MaxX;
                float maxZ = m_KdTree[node].m_MaxZ;

                bool isVertical = (maxX - minX > maxZ - minZ);
                if (isVertical) {
                    float splitValue = 0.5f * (maxX + minX);
                    visitor(splitValue, minZ, splitValue, maxZ, begin, end, m_Objects);
                } else {
                    float splitValue = 0.5f * (maxZ + minZ);
                    visitor(minX, splitValue, maxX, splitValue, begin, end, m_Objects);
                }

                if (left > 0)
                    m_QueryStack.Push(left);
                if (right > 0)
                    m_QueryStack.Push(right);
            }
        }
        private void VisitTreeImpl(MyFunc<float, float, float, float, int, int, KdTreeData[], bool> visitor)
        {
            m_QueryStack.Push(0);
            while (m_QueryStack.Count > 0) {
                int node = m_QueryStack.Pop();

                int begin = m_KdTree[node].m_Begin;
                int end = m_KdTree[node].m_End;
                int left = m_KdTree[node].m_Left;
                int right = m_KdTree[node].m_Right;

                float minX = m_KdTree[node].m_MinX;
                float minZ = m_KdTree[node].m_MinZ;
                float maxX = m_KdTree[node].m_MaxX;
                float maxZ = m_KdTree[node].m_MaxZ;

                bool isVertical = (maxX - minX > maxZ - minZ);
                if (isVertical) {
                    float splitValue = 0.5f * (maxX + minX);
                    if (!visitor(splitValue, minZ, splitValue, maxZ, begin, end, m_Objects)) {
                        m_QueryStack.Clear();
                        return;
                    }
                } else {
                    float splitValue = 0.5f * (maxZ + minZ);
                    if (!visitor(minX, splitValue, maxX, splitValue, begin, end, m_Objects)) {
                        m_QueryStack.Clear();
                        return;
                    }
                }

                if (left > 0)
                    m_QueryStack.Push(left);
                if (right > 0)
                    m_QueryStack.Push(right);
            }
        }

        private static float Sqr(float v)
        {
            return v * v;
        }

        private static float CalcSquareDistToRectangle(float distMinX, float distMaxX, float distMinZ, float distMaxZ)
        {
            float ret = 0;
            if (distMinX > 0) ret += distMinX * distMinX;
            if (distMaxX > 0) ret += distMaxX * distMaxX;
            if (distMinZ > 0) ret += distMinZ * distMinZ;
            if (distMaxZ > 0) ret += distMaxZ * distMaxZ;
            return ret;
        }

        private KdTreeData[] m_Objects = null;
        private int m_ObjectNum = 0;
        private KdTreeNode[] m_KdTree = null;
        private int m_MaxNodeNum = 0;
        private Stack<int> m_BuildStack = new Stack<int>(4096);
        private Stack<int> m_QueryStack = new Stack<int>(4096);
    }
}
