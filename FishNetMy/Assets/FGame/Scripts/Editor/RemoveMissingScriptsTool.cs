using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class RemoveMissingScriptsTool : EditorWindow
{
    private bool removeInScenes = true;
    private bool removeInPrefabs = true;
    private bool removeInProjectPrefabs = true;
    private bool showLogs = true;

    [MenuItem("Tools/删除Missing脚本工具")]
    public static void ShowWindow()
    {
        GetWindow<RemoveMissingScriptsTool>("删除Missing脚本");
    }

    private void OnGUI()
    {
        GUILayout.Label("删除所有Missing脚本", EditorStyles.boldLabel);
        GUILayout.Space(10);

        removeInScenes = EditorGUILayout.Toggle("删除当前场景中的Missing脚本", removeInScenes);
        removeInPrefabs = EditorGUILayout.Toggle("删除Assets中预制件的Missing脚本", removeInPrefabs);
        removeInProjectPrefabs = EditorGUILayout.Toggle("删除项目中所有预制件的Missing脚本", removeInProjectPrefabs);
        showLogs = EditorGUILayout.Toggle("显示详细日志", showLogs);

        GUILayout.Space(10);

        if (GUILayout.Button("删除所有Missing脚本", GUILayout.Height(40)))
        {
            if (EditorUtility.DisplayDialog("确认",
                "确定要删除所有Missing脚本吗？此操作不可撤销！\n\n" +
                "注意：建议先备份项目！",
                "确认删除", "取消"))
            {
                RemoveAllMissingScripts();
            }
        }

        GUILayout.Space(10);
        EditorGUILayout.HelpBox("注意：此操作会修改场景和预制件文件，建议先备份项目！", MessageType.Warning);
    }

    private void RemoveAllMissingScripts()
    {
        int totalRemoved = 0;
        int sceneCount = 0;
        int prefabCount = 0;

        // 删除当前打开场景中的Missing脚本
        if (removeInScenes)
        {
            int removed = RemoveMissingScriptsInOpenScenes();
            totalRemoved += removed;
            sceneCount = removed;
        }

        // 删除项目中的所有预制件中的Missing脚本
        if (removeInPrefabs && removeInProjectPrefabs)
        {
            int removed = RemoveMissingScriptsInAllPrefabs();
            totalRemoved += removed;
            prefabCount = removed;
        }

        // 保存所有修改
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // 显示结果
        if (showLogs)
        {
            Debug.Log($"删除完成！总共删除了 {totalRemoved} 个Missing脚本。\n" +
                     $"场景中删除了 {sceneCount} 个，预制件中删除了 {prefabCount} 个。");
        }

        EditorUtility.DisplayDialog("完成", $"删除完成！\n\n总共删除了 {totalRemoved} 个Missing脚本。", "确定");
    }

    private int RemoveMissingScriptsInOpenScenes()
    {
        int count = 0;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                GameObject[] rootObjects = scene.GetRootGameObjects();
                foreach (GameObject rootObject in rootObjects)
                {
                    count += RemoveMissingScriptsRecursive(rootObject, true);
                }

                // 标记场景为已修改
                EditorSceneManager.MarkSceneDirty(scene);
            }
        }

        // 保存场景修改
        EditorSceneManager.SaveOpenScenes();

        if (showLogs)
        {
            Debug.Log($"已删除 {count} 个场景中的Missing脚本");
        }

        return count;
    }

    private int RemoveMissingScriptsInAllPrefabs()
    {
        int count = 0;
        List<string> modifiedPrefabs = new List<string>();

        // 查找所有预制件
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        int total = prefabGuids.Length;

        for (int i = 0; i < total; i++)
        {
            // 显示进度条
            if (EditorUtility.DisplayCancelableProgressBar(
                "删除预制件中的Missing脚本",
                $"处理预制件 {i + 1}/{total}",
                (float)i / total))
            {
                break;
            }

            string prefabPath = AssetDatabase.GUIDToAssetPath(prefabGuids[i]);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (prefab != null)
            {
                int removed = RemoveMissingScriptsInPrefab(prefab);
                if (removed > 0)
                {
                    count += removed;
                    modifiedPrefabs.Add(prefabPath);

                    if (showLogs)
                    {
                        Debug.Log($"预制件 {prefab.name} 删除了 {removed} 个Missing脚本");
                    }
                }
            }
        }

        // 保存所有修改过的预制件
        foreach (string path in modifiedPrefabs)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab != null)
            {
                PrefabUtility.SavePrefabAsset(prefab);
            }
        }

        EditorUtility.ClearProgressBar();

        if (showLogs)
        {
            Debug.Log($"已删除 {count} 个预制件中的Missing脚本");
        }

        return count;
    }

    private int RemoveMissingScriptsInPrefab(GameObject prefab)
    {
        int count = 0;

        // 获取预制件的所有实例ID，用于后续比较
        Dictionary<GameObject, List<Component>> originalComponents = new Dictionary<GameObject, List<Component>>();

        // 收集所有对象及其组件
        Transform[] allTransforms = prefab.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allTransforms)
        {
            if (child != null)
            {
                GameObject obj = child.gameObject;
                originalComponents[obj] = new List<Component>(obj.GetComponents<Component>());
            }
        }

        // 再次遍历，检测并删除Missing脚本
        foreach (Transform child in allTransforms)
        {
            if (child != null)
            {
                GameObject obj = child.gameObject;
                List<Component> components = originalComponents[obj];

                // 检查是否有Missing脚本（null组件）
                bool hasMissing = false;
                for (int i = 0; i < components.Count; i++)
                {
                    if (components[i] == null)
                    {
                        hasMissing = true;
                        break;
                    }
                }

                if (hasMissing)
                {
                    // 使用GameObjectUtility来移除Missing脚本
                    int removed = RemoveMissingScriptsFromGameObject(obj);
                    count += removed;
                }
            }
        }

        return count;
    }

    private int RemoveMissingScriptsRecursive(GameObject obj, bool isRoot)
    {
        int count = 0;

        // 处理当前对象
        count += RemoveMissingScriptsFromGameObject(obj);

        // 递归处理子对象
        foreach (Transform child in obj.transform)
        {
            count += RemoveMissingScriptsRecursive(child.gameObject, false);
        }

        return count;
    }

    private int RemoveMissingScriptsFromGameObject(GameObject obj)
    {
        if (obj == null) return 0;

        int count = 0;

        try
        {
            // 使用UnityEngine.Object.DestroyImmediate方法来删除Missing脚本
            Component[] components = obj.GetComponents<Component>();
            bool modified = false;

            // 从后向前遍历，因为删除会影响索引
            for (int i = components.Length - 1; i >= 0; i--)
            {
                if (components[i] == null)
                {
                    // 这个方法可以安全地删除Missing脚本
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(obj);
                    modified = true;
                    count++;
                    break; // RemoveMonoBehavioursWithMissingScript会删除所有Missing脚本，所以只需调用一次
                }
            }

            if (modified)
            {
                EditorUtility.SetDirty(obj);
            }
        }
        catch (System.Exception e)
        {
            if (showLogs)
            {
                Debug.LogWarning($"处理对象 {obj.name} 时出错: {e.Message}");
            }
        }

        return count;
    }

    // 添加一个快速清除当前选中对象的Missing脚本的功能
    [MenuItem("GameObject/删除选中对象的Missing脚本", false, 0)]
    private static void RemoveMissingScriptsFromSelected()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        int totalRemoved = 0;

        foreach (GameObject obj in selectedObjects)
        {
            int removed = RemoveMissingScriptsFromObjectRecursive(obj);
            totalRemoved += removed;
        }

        if (totalRemoved > 0)
        {
            AssetDatabase.SaveAssets();
            Debug.Log($"已从选中的对象中删除 {totalRemoved} 个Missing脚本");
        }
        else
        {
            Debug.Log("选中的对象中没有找到Missing脚本");
        }
    }

    private static int RemoveMissingScriptsFromObjectRecursive(GameObject obj)
    {
        int count = 0;

        // 处理当前对象
        var tool = CreateInstance<RemoveMissingScriptsTool>();
        int removed = tool.RemoveMissingScriptsFromGameObject(obj);
        count += removed;

        // 处理子对象
        foreach (Transform child in obj.transform)
        {
            count += RemoveMissingScriptsFromObjectRecursive(child.gameObject);
        }

        return count;
    }

    [MenuItem("GameObject/删除选中对象的Missing脚本", true)]
    private static bool ValidateRemoveMissingScriptsFromSelected()
    {
        return Selection.gameObjects.Length > 0;
    }
}