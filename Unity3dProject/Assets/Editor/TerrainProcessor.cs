﻿using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.Animations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GameLibrary;

public sealed class TerrainEditWindow : EditorWindow
{
    [MenuItem("工具/地形处理")]
    internal static void InitWindow()
    {
        TerrainEditWindow window = (TerrainEditWindow)EditorWindow.GetWindow(typeof(TerrainEditWindow));
        window.Init();
        window.Show();
    }

    private void Init()
    {
        GenerateTerrainsInfo();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("选中地形信息")) {
            GenerateTerrainsInfo();
        }
        if (GUILayout.Button("选择地形处理脚本")) {
            SelectDsl();
        }
        if (GUILayout.Button("处理选中场景地形")) {
            ProcessSelectedSceneNodes();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("列表文件", GUILayout.Width(60));
        GUILayout.TextField(m_ListFile, 1024);
        if (GUILayout.Button("选择", GUILayout.Width(60))) {
            string path = EditorUtility.SaveFilePanel("请选择要保存对象列表的文件", string.Empty, "objlist", "txt");
            if (!string.IsNullOrEmpty(path)) {
                m_ListFile = TerrainEditUtility.PathToAssetPath(path);
            }
        }
        EditorGUILayout.EndHorizontal();

        if (m_Samplers.Count > 0) {
            foreach (var pair in m_Samplers) {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(pair.Key, GUILayout.Width(60));
                GUILayout.TextField(pair.Value, 1024);
                if (GUILayout.Button("选择", GUILayout.Width(60))) {
                    SelectImage(pair.Key, pair.Value);
                }
                EditorGUILayout.EndHorizontal();
            }
            if (m_EditedSamplers.Count > 0) {
                foreach (var pair in m_EditedSamplers) {
                    m_Samplers[pair.Key] = pair.Value;
                }
                m_EditedSamplers.Clear();
            }
        }

        m_Pos = EditorGUILayout.BeginScrollView(m_Pos, true, true);
        EditorGUILayout.TextArea(m_Text);
        EditorGUILayout.EndScrollView();
    }

    private void GenerateTerrainsInfo()
    {
        StringBuilder sb = new StringBuilder();
        foreach (GameObject obj in Selection.gameObjects) {
            TerrainEditUtility.GenerateInfo(sb, obj);
        }
        m_Text = sb.ToString();
    }

    private void SelectDsl()
    {
        string path = EditorUtility.OpenFilePanel("请选择要执行的dsl文件", string.Empty, "dsl");
        if (!string.IsNullOrEmpty(path) && File.Exists(path)) {
            Dsl.DslFile file = new Dsl.DslFile();
            if (file.Load(path, (string msg) => { Debug.Log(msg); })) {
                m_DslFile = file;

                m_Samplers.Clear();
                foreach(var info in m_DslFile.DslInfos) {
                    var first = info.First;
                    foreach(var comp in first.Statements) {
                        var callData = comp as Dsl.CallData;
                        string id = callData.GetId();
                        if (id == "sampler") {
                            string key = callData.GetParamId(0);
                            string val = callData.GetParamId(1);

                            if (File.Exists(val)) {
                                m_Samplers[key] = val;
                            } else {
                                m_Samplers[key] = string.Empty;
                            }
                        } else if (id == "cache") {
                            string key = callData.GetParamId(0);
                            int w = int.Parse(callData.GetParamId(1));
                            int h = int.Parse(callData.GetParamId(2));
                            m_Caches.Add(key, new Size(w, h));
                        } else if (id == "list") {
                            m_ListFile = callData.GetParamId(0);
                        }
                    }
                }
            } else {
                m_DslFile = null;
                m_Samplers.Clear();
            }
        }
    }

    private void SelectImage(string key, string val)
    {
        string path = EditorUtility.OpenFilePanelWithFilters("请选择要图形文件", val, new[] { "Image files", "png,jpg,tga,bmp", "All files", "*" });
        if (!string.IsNullOrEmpty(path) && File.Exists(path)) {
            path = TerrainEditUtility.PathToAssetPath(path);
            m_EditedSamplers[key] = path;
        }
    }

    private void ProcessSelectedSceneNodes()
    {
        Dictionary<string, Color32[,]> samplers = new Dictionary<string, Color32[,]>();
        foreach (var pair in m_Samplers) {
            var bitmap = AssetDatabase.LoadAssetAtPath<Texture2D>(pair.Value);
            if (null != bitmap) {
                var colors = new Color32[bitmap.width, bitmap.height];
                for (int i = 0; i < bitmap.width; ++i) {
                    for (int j = 0; j < bitmap.height; ++j) {
                        Color c = bitmap.GetPixel(i, j);
                        colors[i, j] = c;
                    }
                }
                samplers.Add(pair.Key, colors);
            }
        }
        List<ObjectInfo> objects = new List<ObjectInfo>();
        foreach (GameObject obj in Selection.gameObjects) {
            TerrainEditUtility.Process(obj, m_DslFile, samplers, m_Caches, objects);
            Debug.Log("handle " + obj.name);
        }
        if (objects.Count > 0) {
            var root = GameObject.Find("objs");
            ObjectDetector objDetector;
            if (null == root) {
                root = new GameObject("objs");
                objDetector = root.AddComponent<ObjectDetector>();
            } else {
                objDetector = root.GetComponent<ObjectDetector>();
            }

            List<UnityEngine.GameObject> objList = new List<GameObject>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[");
            int ct = objects.Count;
            for (int i = 0; i < ct; ++i) {
                var objInfo = objects[i];
                string name = string.Format("obj{0}", i);
                sb.Append("[");
                sb.AppendFormat("\"{0}\", {1}, {2}, {3}", name, objInfo.X, objInfo.Y, objInfo.Z);
                if (i < ct - 1) {
                    sb.AppendLine("],");
                } else {
                    sb.AppendLine("]");
                }

                var prefab = Resources.Load(objInfo.Prefab);
                var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                obj.name = name;
                obj.transform.SetParent(root.transform);
                obj.transform.SetPositionAndRotation(new Vector3(objInfo.X, objInfo.Y, objInfo.Z), Quaternion.identity);
                objList.Add(obj);

                if (TerrainEditUtility.DisplayCancelableProgressBar("创建场景对象", i + 1, ct))
                    break;
            }
            sb.AppendLine("]");
            var list = sb.ToString();
            var file = TerrainEditUtility.AssetPathToPath(m_ListFile);
            File.WriteAllText(file, list);
            objDetector.Objects = objList.ToArray();
            objDetector.MessageName = "notify_player";
            EditorUtility.ClearProgressBar();
        }
        EditorUtility.DisplayDialog("提示", "处理完成", "ok");
    }

    private Vector2 m_Pos = Vector2.zero;
    private string m_Text = string.Empty;
    private string m_ListFile = string.Empty;
    private Dsl.DslFile m_DslFile = null;
    private Dictionary<string, string> m_Samplers = new Dictionary<string, string>();
    private Dictionary<string, string> m_EditedSamplers = new Dictionary<string, string>();
    private Dictionary<string, Size> m_Caches = new Dictionary<string, Size>();
}

internal class DetailInfo
{
    internal int X;
    internal int Y;
    internal int Val;
}

internal struct Size
{
    internal int Width;
    internal int Height;

    internal Size(int w, int h)
    {
        Width = w;
        Height = h;
    }
}

internal class ObjectInfo
{
    internal float X;
    internal float Y;
    internal float Z;
    internal string Prefab;
}

internal static class TerrainEditUtility
{
    internal static int MaxObjectCount
    {
        get { return s_MaxObjectCount; }
    }
    internal static void DisplayProgressBar(string title, int resultCount, int curCount, int totalCount)
    {
        DisplayProgressBar(title, resultCount, curCount, totalCount, true);
    }
    internal static void DisplayProgressBar(string title, int resultCount, int curCount, int totalCount, bool batch)
    {
        if (batch && totalCount > 1000) {
            if (curCount % 10 == 0) {
                EditorUtility.DisplayProgressBar(title, string.Format("{0} in {1}/{2}", resultCount, curCount, totalCount), curCount * 1.0f / totalCount);
            }
        } else {
            EditorUtility.DisplayProgressBar(title, string.Format("{0} in {1}/{2}", resultCount, curCount, totalCount), curCount * 1.0f / totalCount);
        }
    }

    internal static void DisplayProgressBar(string title, int curCount, int totalCount)
    {
        DisplayProgressBar(title, curCount, totalCount, true);
    }
    internal static void DisplayProgressBar(string title, int curCount, int totalCount, bool batch)
    {
        if (batch && totalCount > 1000) {
            if (curCount % 10 == 0) {
                EditorUtility.DisplayProgressBar(title, string.Format("{0}/{1}", curCount, totalCount), curCount * 1.0f / totalCount);
            }
        } else {
            EditorUtility.DisplayProgressBar(title, string.Format("{0}/{1}", curCount, totalCount), curCount * 1.0f / totalCount);
        }
    }

    internal static bool DisplayCancelableProgressBar(string title, int resultCount, int curCount, int totalCount)
    {
        return DisplayCancelableProgressBar(title, resultCount, curCount, totalCount, true);
    }
    internal static bool DisplayCancelableProgressBar(string title, int resultCount, int curCount, int totalCount, bool batch)
    {
        if (batch && totalCount > 1000) {
            if (curCount % 10 == 0) {
                return EditorUtility.DisplayCancelableProgressBar(title, string.Format("{0} in {1}/{2}", resultCount, curCount, totalCount), curCount * 1.0f / totalCount);
            }
        } else {
            return EditorUtility.DisplayCancelableProgressBar(title, string.Format("{0} in {1}/{2}", resultCount, curCount, totalCount), curCount * 1.0f / totalCount);
        }
        return false;
    }

    internal static bool DisplayCancelableProgressBar(string title, int curCount, int totalCount)
    {
        return DisplayCancelableProgressBar(title, curCount, totalCount, true);
    }
    internal static bool DisplayCancelableProgressBar(string title, int curCount, int totalCount, bool batch)
    {
        if (batch && totalCount > 1000) {
            if (curCount % 10 == 0) {
                return EditorUtility.DisplayCancelableProgressBar(title, string.Format("{0}/{1}", curCount, totalCount), curCount * 1.0f / totalCount);
            }
        } else {
            return EditorUtility.DisplayCancelableProgressBar(title, string.Format("{0}/{1}", curCount, totalCount), curCount * 1.0f / totalCount);
        }
        return false;
    }
    internal static void GenerateInfo(StringBuilder sb, GameObject root)
    {
        var terrain = root.GetComponent<Terrain>();
        if (null != terrain) {
            WriteTerrainInfo(sb, 0, terrain);
        }
    }
    internal static void Process(GameObject root, Dsl.DslFile file, Dictionary<string, Color32[,]> samplers, Dictionary<string, Size> cacheInfos, List<ObjectInfo> objects)
    {
        if (null != file) {
            List<TreeInstance> trees = new List<TreeInstance>();
            Dictionary<string, int[,]> caches = new Dictionary<string, int[,]>();
            foreach (var pair in cacheInfos) {
                caches.Add(pair.Key, new int[pair.Value.Width, pair.Value.Height]);
            }
            var terrain = root.GetComponent<Terrain>();
            var terrainData = terrain.terrainData;
            var datas = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
            var alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
            int alphanum = alphamaps.GetLength(2);
            int[] layers = terrainData.GetSupportedLayers(0, 0, terrainData.detailWidth, terrainData.detailHeight);
            Dictionary<int, int[,]> details = new Dictionary<int, int[,]>();
            foreach (int layer in layers) {
                var ds = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, layer);
                details.Add(layer, ds);
            }
            var calc = new Expression.DslCalculator();
            calc.Init();
            calc.Register("getheight", new Expression.ExpressionFactoryHelper<GetHeightExp>());
            calc.Register("getalphamap", new Expression.ExpressionFactoryHelper<GetAlphamapExp>());
            calc.Register("getalpha", new Expression.ExpressionFactoryHelper<GetAlphaExp>());
            calc.Register("setalpha", new Expression.ExpressionFactoryHelper<SetAlphaExp>());
            calc.Register("getdetail", new Expression.ExpressionFactoryHelper<GetDetailExp>());
            calc.Register("samplered", new Expression.ExpressionFactoryHelper<SampleRedExp>());
            calc.Register("samplegreen", new Expression.ExpressionFactoryHelper<SampleGreenExp>());
            calc.Register("sampleblue", new Expression.ExpressionFactoryHelper<SampleBlueExp>());
            calc.Register("samplealpha", new Expression.ExpressionFactoryHelper<SampleAlphaExp>());
            calc.Register("getcache", new Expression.ExpressionFactoryHelper<GetCacheExp>());
            calc.Register("setcache", new Expression.ExpressionFactoryHelper<SetCacheExp>());
            calc.Register("addtree", new Expression.ExpressionFactoryHelper<AddTreeExp>());
            calc.Register("addobject", new Expression.ExpressionFactoryHelper<AddObjectExp>());
            calc.SetGlobalVariable("samplers", samplers);
            calc.SetGlobalVariable("caches", caches);
            calc.SetGlobalVariable("trees", trees);
            calc.SetGlobalVariable("objects", objects);
            calc.SetGlobalVariable("heightscalex", terrainData.heightmapScale.x);
            calc.SetGlobalVariable("heightscaley", terrainData.heightmapScale.y);
            calc.SetGlobalVariable("heightscalez", terrainData.heightmapScale.z);
            calc.SetGlobalVariable("heights", datas);
            calc.SetGlobalVariable("alphamaps", alphamaps);
            calc.SetGlobalVariable("alphanum", alphanum);
            calc.SetGlobalVariable("details", details);
            calc.SetGlobalVariable("height", 0.0f);
            calc.SetGlobalVariable("alphas", new float[alphanum]);
            calc.SetGlobalVariable("detail", 0);
            bool resetTrees = false;
            bool canContinue = true;
            foreach (var info in file.DslInfos) {
                bool check=false;
                int num = info.GetFunctionNum();
                if (num >= 2) {
                    string firstId = info.First.GetId();
                    if(firstId=="input"){
                        check = true;
                        for (int i = 1; i < info.GetFunctionNum(); ++i) {
                            string id = info.GetFunctionId(i);
                            if (id == "height" || id == "alphamap" || id == "detail") {
                            } else {
                                check = false;
                                break;
                            }
                        }
                    }
                }
                if(!check){
                    canContinue = false;
                    Debug.LogErrorFormat("error script:{0}, {1}", info.GetLine(), info.ToScriptString(false));
                }
            }
            if (canContinue) {
                int ix = 0;
                foreach (var info in file.DslInfos) {
                    for (int i = 1; i < info.GetFunctionNum(); ++i) {
                        calc.LoadDsl(ix.ToString(), info.GetFunction(i));
                        ++ix;
                    }
                }
                int ix2 = 0;
                foreach (var info in file.DslInfos) {
                    for (int i = 1; i < info.GetFunctionNum(); ++i) {
                        ProcessWithDsl(info.First, info.GetFunctionId(i), datas, alphamaps, details, calc, ix2.ToString(), ref resetTrees);
                        ++ix2;
                    }
                }
            }
            terrainData.SetHeights(0, 0, datas);
            terrainData.SetAlphamaps(0, 0, alphamaps);
            foreach (var pair in details) {
                terrainData.SetDetailLayer(0, 0, pair.Key, pair.Value);
            }
            if (resetTrees) {
                terrainData.treeInstances = trees.ToArray();
            } else {
                trees.AddRange(terrainData.treeInstances);
                terrainData.treeInstances = trees.ToArray();
            }
        }
    }

    private static void ProcessWithDsl(Dsl.FunctionData funcData, string type, float[,] datas, float[, ,] alphamaps, Dictionary<int, int[,]> details, Expression.DslCalculator calc, string proc, ref bool resetTrees)
    {
        if (null == funcData)
            return;
        if (null != funcData) {
            if (type == "height") {
                foreach (var comp in funcData.Statements) {
                    var callData = comp as Dsl.CallData;
                    if (null != callData) {
                        string id = callData.GetId();
                        if (id == "resettrees") {
                            resetTrees = bool.Parse(callData.GetParamId(0));
                        } else if (id == "maxcount") {
                            s_MaxObjectCount = int.Parse(callData.GetParamId(0));
                        } else if (id == "rect") {
                            int x = int.Parse(callData.GetParamId(0));
                            int y = int.Parse(callData.GetParamId(1));
                            int w = int.Parse(callData.GetParamId(2));
                            int h = int.Parse(callData.GetParamId(3));
                            ProcessHeights(datas, calc, proc, x, y, w, h);
                        } else if (id == "circle") {
                            int x = int.Parse(callData.GetParamId(0));
                            int y = int.Parse(callData.GetParamId(1));
                            int r = int.Parse(callData.GetParamId(2));
                            ProcessHeights(datas, calc, proc, x, y, r);
                        }
                    }
                }
            } else if (type == "alphamap") {
                int alphanum = alphamaps.GetLength(2);
                foreach (var comp in funcData.Statements) {
                    var callData = comp as Dsl.CallData;
                    if (null != callData) {
                        string id = callData.GetId();
                        if (id == "resettrees") {
                            resetTrees = bool.Parse(callData.GetParamId(0));
                        } else if (id == "maxcount") {
                            s_MaxObjectCount = int.Parse(callData.GetParamId(0));
                        } else if (id == "rect") {
                            int x = int.Parse(callData.GetParamId(0));
                            int y = int.Parse(callData.GetParamId(1));
                            int w = int.Parse(callData.GetParamId(2));
                            int h = int.Parse(callData.GetParamId(3));
                            ProcessAlphamaps(alphamaps, calc, proc, x, y, w, h);
                        } else if (id == "circle") {
                            int x = int.Parse(callData.GetParamId(0));
                            int y = int.Parse(callData.GetParamId(1));
                            int r = int.Parse(callData.GetParamId(2));
                            ProcessAlphamaps(alphamaps, calc, proc, x, y, r);
                        }
                    }
                }
            } else if (type == "detail") {
                foreach (var comp in funcData.Statements) {
                    var callData = comp as Dsl.CallData;
                    if (null != callData) {
                        string id = callData.GetId();
                        if (id == "resettrees") {
                            resetTrees = bool.Parse(callData.GetParamId(0));
                        } else if (id == "maxcount") {
                            s_MaxObjectCount = int.Parse(callData.GetParamId(0));
                        } else if (id == "rect") {
                            int x = int.Parse(callData.GetParamId(0));
                            int y = int.Parse(callData.GetParamId(1));
                            int w = int.Parse(callData.GetParamId(2));
                            int h = int.Parse(callData.GetParamId(3));
                            ProcessDetails(details, calc, proc, x, y, w, h);
                        } else if (id == "circle") {
                            int x = int.Parse(callData.GetParamId(0));
                            int y = int.Parse(callData.GetParamId(1));
                            int r = int.Parse(callData.GetParamId(2));
                            ProcessDetails(details, calc, proc, x, y, r);
                        }
                    }
                }
            }
        }
    }

    private static void ProcessHeights(float[,] datas, Expression.DslCalculator calc, string proc, int x, int y, int w, int h)
    {
        for (int ix = 0; ix < w; ++ix) {
            for (int iy = 0; iy < h; ++iy) {
                int xi = x + ix;
                int yi = y + iy;
                calc.SetGlobalVariable("height", datas[yi, xi]);
                calc.Calc(proc, xi, yi);
                datas[yi, xi] = (float)Convert.ChangeType(calc.GetGlobalVariable("height"), typeof(float));
            }

            if (DisplayCancelableProgressBar("生成高度与对象数据", ix * h, w * h))
                goto quit;
        }
        quit:
        EditorUtility.ClearProgressBar();
    }
    private static void ProcessHeights(float[,] datas, Expression.DslCalculator calc, string proc, int cx, int cy, int r)
    {
        int x = cx - r;
        int y = cy - r;
        int w = r * 2;
        int h = r * 2;
        int r2 = r * r;
        for (int ix = 0; ix < w; ++ix) {
            for (int iy = 0; iy < h; ++iy) {
                int xi = x + ix;
                int yi = y + iy;
                int dx = xi - cx;
                int dy = yi - cy;
                if (dx * dx + dy * dy <= r2) {
                    calc.SetGlobalVariable("height", datas[yi, xi]);
                    calc.Calc(proc, xi, yi);
                    datas[yi, xi] = (float)Convert.ChangeType(calc.GetGlobalVariable("height"), typeof(float));
                }
            }

            if (DisplayCancelableProgressBar("生成高度与对象数据", ix * h, w * h))
                goto quit;
        }
        quit:
        EditorUtility.ClearProgressBar();
    }
    private static void ProcessAlphamaps(float[,,] alphamaps, Expression.DslCalculator calc, string proc, int x, int y, int w, int h)
    {
        int alphanum = alphamaps.GetLength(2);
        for (int ix = 0; ix < w; ++ix) {
            for (int iy = 0; iy < h; ++iy) {
                int xi = x + ix;
                int yi = y + iy;
                float[] alphas = calc.GetGlobalVariable("alphas") as float[];
                for (int i = 0; i < alphanum; ++i) {
                    alphas[i] = alphamaps[xi, yi, i];
                }
                var v = calc.Calc(proc, xi, yi);
                for (int i = 0; i < alphanum; ++i) {
                    alphamaps[xi, yi, i] = alphas[i];
                }
            }

            if (DisplayCancelableProgressBar("生成alphamap数据", ix * h, w * h))
                goto quit;
        }
        quit:
        EditorUtility.ClearProgressBar();
    }
    private static void ProcessAlphamaps(float[,,] alphamaps, Expression.DslCalculator calc, string proc, int cx, int cy, int r)
    {
        int alphanum = alphamaps.GetLength(2);
        int x = cx - r;
        int y = cy - r;
        int w = r * 2;
        int h = r * 2;
        int r2 = r * r;
        for (int ix = 0; ix < w; ++ix) {
            for (int iy = 0; iy < h; ++iy) {
                int xi = x + ix;
                int yi = y + iy;
                int dx = xi - cx;
                int dy = yi - cy;
                if (dx * dx + dy * dy <= r2) {
                    float[] alphas = calc.GetGlobalVariable("alphas") as float[];
                    for (int i = 0; i < alphanum; ++i) {
                        alphas[i] = alphamaps[xi, yi, i];
                    }
                    var v = calc.Calc(proc, xi, yi);
                    for (int i = 0; i < alphanum; ++i) {
                        alphamaps[xi, yi, i] = alphas[i];
                    }
                }
            }

            if (DisplayCancelableProgressBar("生成alphamap数据", ix * h, w * h))
                goto quit;
        }
        quit:
        EditorUtility.ClearProgressBar();
    }
    private static void ProcessDetails(Dictionary<int, int[,]> details, Expression.DslCalculator calc, string proc, int x, int y, int w, int h)
    {
        for (int ix = 0; ix < w; ++ix) {
            for (int iy = 0; iy < h; ++iy) {
                int xi = x + ix;
                int yi = y + iy;
                foreach (var pair in details) {
                    int layer = pair.Key;
                    var detail = pair.Value[xi, yi];
                    calc.SetGlobalVariable("detail", detail);
                    calc.Calc(proc, xi, yi, layer);
                    pair.Value[xi, yi] = (int)Convert.ChangeType(calc.GetGlobalVariable("detail"), typeof(int));
                }
            }

            if (DisplayCancelableProgressBar("生成detail数据", ix * h, w * h))
                goto quit;
        }
        quit:
        EditorUtility.ClearProgressBar();
    }
    private static void ProcessDetails(Dictionary<int, int[,]> details, Expression.DslCalculator calc, string proc, int cx, int cy, int r)
    {
        int x = cx - r;
        int y = cy - r;
        int w = r * 2;
        int h = r * 2;
        int r2 = r * r;
        for (int ix = 0; ix < w; ++ix) {
            for (int iy = 0; iy < h; ++iy) {
                int xi = x + ix;
                int yi = y + iy;
                int dx = xi - cx;
                int dy = yi - cy;
                if (dx * dx + dy * dy <= r2) {
                    foreach (var pair in details) {
                        int layer = pair.Key;
                        var detail = pair.Value[xi, yi];
                        calc.SetGlobalVariable("detail", detail);
                        calc.Calc(proc, xi, yi, layer);
                        pair.Value[xi, yi] = (int)Convert.ChangeType(calc.GetGlobalVariable("detail"), typeof(int));
                    }
                }
            }

            if (DisplayCancelableProgressBar("生成detail数据", ix * h, w * h))
                goto quit;
        }
        quit:
        EditorUtility.ClearProgressBar();
    }

    private static void WriteTerrainInfo(StringBuilder sb, int indent, Terrain terrain)
    {
        var data = terrain.terrainData;
        AppendLine(sb, "{0}terrain(\"{1}/{2}\")", GetIndent(indent), terrain.name, data.name);
        AppendLine(sb, "{0}{{", GetIndent(indent));
        ++indent;

        AppendLine(sb, "{0}size{1};", GetIndent(indent), data.size);
        AppendLine(sb, "{0}thickness({1});", GetIndent(indent), data.thickness);
        AppendLine(sb, "{0}basemapresolution({1});", GetIndent(indent), data.baseMapResolution);

        AppendLine(sb, "{0}heightmap(size({1}, {2}), resolution({3}), scale{4});", GetIndent(indent), data.heightmapWidth, data.heightmapHeight, data.heightmapResolution, data.heightmapScale);

        AppendLine(sb, "{0}alphamap(size({1}, {2}), resolution({3}), layers({4}))", GetIndent(indent), data.alphamapWidth, data.alphamapHeight, data.alphamapResolution, data.alphamapLayers);
        AppendLine(sb, "{0}{{", GetIndent(indent));
        ++indent;
        foreach(var sp in data.splatPrototypes) {
            AppendLine(sb, "{0}splat(texture(\"{1}\", {2}, {3}), normalmap{4}, tilesize({5}), tileoffset({6}), specular({7}), metallic({8}), smoothness({9}));", GetIndent(indent), sp.texture.name, sp.texture.width, sp.texture.height, null != sp.normalMap ? string.Format("(\"{1}\", {2}, {3})", sp.normalMap.name, sp.normalMap.width, sp.normalMap.height) : "()", sp.tileSize, sp.tileOffset, sp.specular, sp.metallic, sp.smoothness);
        }
        --indent;
        AppendLine(sb, "{0}}};", GetIndent(indent));

        AppendLine(sb, "{0}detail(size({1}, {2}), resolution({3}))", GetIndent(indent), data.detailWidth, data.detailHeight, data.detailResolution);
        AppendLine(sb, "{0}{{", GetIndent(indent));
        ++indent;
        foreach(var dp in data.detailPrototypes) {
            AppendLine(sb, "{0}prototype(texture(\"{1}\", {2}, {3}), rendermode({4}), usemesh({5}), minsize({6}, {7}), maxsize({8}, {9}), noisespread({10}), bendfactor({11}), healthcolor({12}), drycolor({13}));", GetIndent(indent), dp.prototypeTexture.name, dp.prototypeTexture.width, dp.prototypeTexture.height, dp.renderMode, dp.usePrototypeMesh, dp.minWidth, dp.minHeight, dp.maxWidth, dp.maxHeight, dp.noiseSpread, dp.bendFactor, dp.healthyColor, dp.dryColor);
        }
        foreach (var layer in data.GetSupportedLayers(0, 0, data.detailWidth, data.detailHeight)) {
            AppendLine(sb, "{0}layer({1})", GetIndent(indent), layer);
            AppendLine(sb, "{0}{{", GetIndent(indent));
            ++indent;
            var details = data.GetDetailLayer(0, 0, data.detailWidth, data.detailHeight, layer);
            var infos = new List<DetailInfo>();
            for(int x = 0; x < data.detailWidth; ++x) {
                for(int y = 0; y < data.detailHeight; ++y) {
                    int v = details[x, y];
                    if (v != 0) {
                        infos.Add(new DetailInfo { X = x, Y = y, Val = v });
                    }
                }
            }
            for (int i = 0; i < 10; ++i) {
                int ix = UnityEngine.Random.Range(0, infos.Count);
                var v = infos[ix];
                AppendLine(sb, "{0}detail(x({1}), y({2}), val({3}));", GetIndent(indent), v.X, v.Y, v.Val);
            }
            --indent;
            AppendLine(sb, "{0}}};", GetIndent(indent));
        }
        --indent;
        AppendLine(sb, "{0}}};", GetIndent(indent));

        AppendLine(sb, "{0}tree({1})", GetIndent(indent), data.treeInstanceCount);
        AppendLine(sb, "{0}{{", GetIndent(indent));
        ++indent;
        foreach (var tp in data.treePrototypes) {
            AppendLine(sb, "{0}prototype(prefab(\"{1}\"), bendfactor({2}));", GetIndent(indent), tp.prefab, tp.bendFactor);
        }
        int totalNum = data.treeInstanceCount;
        for (int i = 0; i < 10 && i < totalNum; ++i) {
            int ix = UnityEngine.Random.Range(0, totalNum);
            var inst = data.GetTreeInstance(ix);
            AppendLine(sb, "{0}{1}:treeinstance(proto({2}), position({3}), rotation({4}), scale({5}, {6}), color({7}), lightmapcolor({8}));", GetIndent(indent), ix, inst.prototypeIndex, inst.position, inst.rotation, inst.widthScale, inst.heightScale, inst.color, inst.lightmapColor);
        }
        --indent;
        AppendLine(sb, "{0}}};", GetIndent(indent));

        AppendLine(sb, "{0}wavinggrass", GetIndent(indent));
        AppendLine(sb, "{0}{{", GetIndent(indent));
        ++indent;
        AppendLine(sb, "{0}amount({1});", GetIndent(indent), data.wavingGrassAmount);
        AppendLine(sb, "{0}speed({1});", GetIndent(indent), data.wavingGrassSpeed);
        AppendLine(sb, "{0}strength({1});", GetIndent(indent), data.wavingGrassStrength);
        AppendLine(sb, "{0}tint({1});", GetIndent(indent), data.wavingGrassTint);
        --indent;
        AppendLine(sb, "{0}}};", GetIndent(indent));

        --indent;
        AppendLine(sb, "{0}}};", GetIndent(indent));
    }

    private static void AppendLine(StringBuilder sb, string format, params object[] args)
    {
        sb.AppendFormat(format, args);
        sb.AppendLine();
    }

    internal static bool IsPathMatch(string path, string filter)
    {
        string ext = Path.GetExtension(path);
        if (ext == ".meta") {
            return false;
        }
        List<string> infos;
        if (!s_PathMatchInfos.TryGetValue(filter, out infos)) {
            string[] filters = filter.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            infos = new List<string>(filters);
            s_PathMatchInfos.Add(filter, infos);
        }
        string fileName = Path.GetFileName(path);
        bool match = true;
        int startIx = 0;
        for (int i = 0; i < infos.Count; ++i) {
            var info = infos[i];
            var ix = fileName.IndexOf(info, startIx, StringComparison.CurrentCultureIgnoreCase);
            if (ix >= 0) {
                startIx = ix + info.Length;
            } else {
                match = false;
                break;
            }
        }
        return match;
    }
    internal static bool IsAssetPath(string path)
    {
        string rootPath = Application.dataPath.Replace('\\', '/');
        path = path.Replace('\\', '/');
        if (path.StartsWith(rootPath)) {
            return true;
        } else {
            return false;
        }
    }
    internal static string PathToAssetPath(string path)
    {
        return FilePathToRelativePath(path);
    }
    internal static string AssetPathToPath(string assetPath)
    {
        return RelativePathToFilePath(assetPath);
    }
    internal static string FilePathToRelativePath(string path)
    {
        string rootPath = GetRootPath();
        path = path.Replace('\\', '/');
        if (path.StartsWith(rootPath)) {
            path = path.Substring(rootPath.Length);
        }
        return path;
    }
    internal static string RelativePathToFilePath(string path)
    {
        string rootPath = GetRootPath();
        path = path.Replace('\\', '/');
        return rootPath + path;
    }
    internal static string GetRootPath()
    {
        const string c_AssetsDir = "Assets";
        if (string.IsNullOrEmpty(s_RootPath)) {
            s_RootPath = Application.dataPath.Replace('\\', '/');
            if (s_RootPath.EndsWith(c_AssetsDir))
                s_RootPath = s_RootPath.Substring(0, s_RootPath.Length - c_AssetsDir.Length);
        }
        return s_RootPath;
    }
    private static GameObject FindRoot(GameObject obj)
    {
        GameObject ret = null;
        var trans = obj.transform;
        while (null != trans && !(trans is RectTransform)) {
            ret = trans.gameObject;
            trans = trans.parent;
        }
        return ret;
    }
    private static UnityEngine.Object LoadAssetByPathAndName(string path, string name)
    {
        var objs = AssetDatabase.LoadAllAssetsAtPath(path);
        foreach (var obj in objs) {
            if (obj.name == name)
                return obj;
        }
        return null;
    }

    private static string GetIndent(int indent)
    {
        return c_IndentString.Substring(0, indent);
    }
    private static string IndentScript(string indent, string scp)
    {
        string[] lines = scp.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        for (int ix = 0; ix < lines.Length; ++ix) {
            lines[ix] = string.Format("{0}{1}", indent, lines[ix]);
        }
        return string.Join("\r\n", lines);
    }

    private static int s_MaxObjectCount = 2048;
    private static Dictionary<string, List<string>> s_PathMatchInfos = new Dictionary<string, List<string>>();
    private static string s_RootPath = string.Empty;
    private const string c_IndentString = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";
}

internal class GetHeightExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = null;
        if (operands.Count >= 2) {
            var datas = Calculator.GetGlobalVariable("heights") as float[,];
            var x = ToLong(operands[0]);
            var y = ToLong(operands[1]);
            r = datas[y, x];
        }
        return r;
    }
}
internal class GetAlphamapExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = null;
        if (operands.Count >= 3) {
            var datas = Calculator.GetGlobalVariable("alphamaps") as float[, ,];
            var x = ToLong(operands[0]);
            var y = ToLong(operands[1]);
            var ix = ToLong(operands[2]);
            r = datas[x, y, ix];
        }
        return r;
    }
}
internal class GetAlphaExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = null;
        if (operands.Count >= 1) {
            var datas = Calculator.GetGlobalVariable("alphas") as float[];
            var ix = ToLong(operands[0]);
            r = datas[ix];
        }
        return r;
    }
}
internal class SetAlphaExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = null;
        if (operands.Count >= 2) {
            var datas = Calculator.GetGlobalVariable("alphas") as float[];
            var ix = ToLong(operands[0]);
            var v = ToDouble(operands[1]);
            datas[ix] = (float)v;
            r = v;
        }
        return r;
    }
}
internal class GetDetailExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = null;
        if (operands.Count >= 3) {
            var datas = Calculator.GetGlobalVariable("details") as Dictionary<int, int[,]>;
            var x = ToLong(operands[0]);
            var y = ToLong(operands[1]);
            var ix = ToLong(operands[2]);
            int[,] v;
            if (datas.TryGetValue((int)ix, out v)) {
                r = v[x, y];
            }
        }
        return r;
    }
}
internal class SampleRedExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = 0;
        if (operands.Count >= 3) {
            var datas = Calculator.GetGlobalVariable("samplers") as Dictionary<string, Color32[,]>;
            var key = operands[0] as string;
            var x = ToLong(operands[1]);
            var y = ToLong(operands[2]);
            Color32[,] colors;
            if (datas.TryGetValue(key, out colors)) {
                if (x >= 0 && x < colors.GetLength(0)) {
                    if (y >= 0 && y < colors.GetLength(1)) {
                        r = (int)colors[x, y].r;
                    }
                }
            }
        }
        return r;
    }
}
internal class SampleGreenExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = 0;
        if (operands.Count >= 3) {
            var datas = Calculator.GetGlobalVariable("samplers") as Dictionary<string, Color32[,]>;
            var key = operands[0] as string;
            var x = ToLong(operands[1]);
            var y = ToLong(operands[2]);
            Color32[,] colors;
            if (datas.TryGetValue(key, out colors)) {
                if (x >= 0 && x < colors.GetLength(0)) {
                    if (y >= 0 && y < colors.GetLength(1)) {
                        r = (int)colors[x, y].g;
                    }
                }
            }
        }
        return r;
    }
}
internal class SampleBlueExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = 0;
        if (operands.Count >= 3) {
            var datas = Calculator.GetGlobalVariable("samplers") as Dictionary<string, Color32[,]>;
            var key = operands[0] as string;
            var x = ToLong(operands[1]);
            var y = ToLong(operands[2]);
            Color32[,] colors;
            if (datas.TryGetValue(key, out colors)) {
                if (x >= 0 && x < colors.GetLength(0)) {
                    if (y >= 0 && y < colors.GetLength(1)) {
                        r = (int)colors[x, y].b;
                    }
                }
            }
        }
        return r;
    }
}
internal class SampleAlphaExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = 0;
        if (operands.Count >= 3) {
            var datas = Calculator.GetGlobalVariable("samplers") as Dictionary<string, Color32[,]>;
            var key = operands[0] as string;
            var x = ToLong(operands[1]);
            var y = ToLong(operands[2]);
            Color32[,] colors;
            if (datas.TryGetValue(key, out colors)) {
                if (x >= 0 && x < colors.GetLength(0)) {
                    if (y >= 0 && y < colors.GetLength(1)) {
                        r = (int)colors[x, y].a;
                    }
                }
            }
        }
        return r;
    }
}
internal class GetCacheExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = 0;
        if (operands.Count >= 3) {
            var datas = Calculator.GetGlobalVariable("caches") as Dictionary<string, int[,]>;
            var key = operands[0] as string;
            var x = ToLong(operands[1]);
            var y = ToLong(operands[2]);
            int[,] vals;
            if (datas.TryGetValue(key, out vals)) {
                if (x >= 0 && x < vals.GetLength(0)) {
                    if (y >= 0 && y < vals.GetLength(1)) {
                        r = vals[x, y];
                    }
                }
            }
        }
        return r;
    }
}
internal class SetCacheExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = 0;
        if (operands.Count >= 4) {
            var datas = Calculator.GetGlobalVariable("caches") as Dictionary<string, int[,]>;
            var key = operands[0] as string;
            var x = ToLong(operands[1]);
            var y = ToLong(operands[2]);
            var v = ToLong(operands[3]);
            int[,] vals;
            if (datas.TryGetValue(key, out vals)) {
                if (x >= 0 && x < vals.GetLength(0)) {
                    if (y >= 0 && y < vals.GetLength(1)) {
                        vals[x, y] = (int)v;
                    }
                }
            }
            r = v;
        }
        return r;
    }
}
internal class AddTreeExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = 0;
        if (operands.Count >= 9) {
            var trees = Calculator.GetGlobalVariable("trees") as List<TreeInstance>;
            var ix = ToLong(operands[0]);
            var x = ToDouble(operands[1]);
            var y = ToDouble(operands[2]);
            var z = ToDouble(operands[3]);
            var rot = ToDouble(operands[4]);
            var w_scale = ToDouble(operands[5]);
            var h_scale = ToDouble(operands[6]);
            var color = (uint)ToLong(operands[7]);
            var lightmap = (uint)ToLong(operands[8]);
            if (null != trees) {
                Color32 c = new Color32((byte)((color & 0xff000000) >> 24), (byte)((color & 0xff0000) >> 16), (byte)((color & 0xff00) >> 8), (byte)(color & 0xff));
                Color32 l = new Color32((byte)((lightmap & 0xff000000) >> 24), (byte)((lightmap & 0xff0000) >> 16), (byte)((lightmap & 0xff00) >> 8), (byte)(lightmap & 0xff));
                trees.Add(new TreeInstance { prototypeIndex = (int)ix, position = new Vector3((float)x, (float)y, (float)z), widthScale = (float)w_scale, heightScale = (float)h_scale, rotation = (float)rot, color = c, lightmapColor = l });
            }
            r = ix;
        }
        return r;
    }
}
internal class AddObjectExp : Expression.SimpleExpressionBase
{
    protected override object OnCalc(IList<object> operands)
    {
        object r = 0;
        if (operands.Count >= 4) {
            var objects = Calculator.GetGlobalVariable("objects") as List<ObjectInfo>;
            var x = ToFloat(operands[0]);
            var y = ToFloat(operands[1]);
            var z = ToFloat(operands[2]);
            var m = ToString(operands[3]);
            if (null != objects && objects.Count < TerrainEditUtility.MaxObjectCount) {
                objects.Add(new ObjectInfo { X = x, Y = y, Z = z, Prefab = m });
            }
            r = m;
        }
        return r;
    }
}