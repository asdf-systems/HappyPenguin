//AlmostLogical Software - http://www.almostlogical.com - support@almostlogical.com
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

[CustomEditor(typeof(Transform))]
public class PPInspector : Editor
{
    private PPTransformInspector transformInspector = new PPTransformInspector();
    private static List<PPCurrentContext> allContexts = new List<PPCurrentContext>();
    private static PPCurrentContext context = null; //this is the last used context

    public static void OpenCloseDropdown()
    {
        if (context != null && context.GameObj!=null && Selection.activeGameObject == context.GameObj)
        {
            context.GameObjectSetting.Expanded = !context.GameObjectSetting.Expanded;
            EditorUtility.SetDirty(context.GameObj); //this updates Inspector GUI
        }
    }

    public static void PersistSelectedGameObject()
    {
        if (context != null && context.GameObj != null && Selection.activeGameObject == context.GameObj)
        {
            if (!context.GameObjectSetting.SaveAll)
            {
                context.GameObjectSetting.SaveAll = true;
                context.GameObjectSetting.ComponentSettings.ForEach(s => s.IsSavingSettings = true);
            }
            else
            {
                context.GameObjectSetting.SaveAll = false;
                context.GameObjectSetting.ComponentSettings.ForEach(s => s.IsSavingSettings = false);
            }
            context.GameObjectSetting.Expanded = true;
            EditorUtility.SetDirty(context.GameObj); //this updates Inspector GUI
        }
    }

    public static void SetPersistanceForAllExistingContextObjects(string typeName,bool isSaveSetting)
    {
        foreach (PPCurrentContext currContext in allContexts)
        {
            if (currContext.GameObj != null)
            {
                foreach (PPComponentSetting compSettings in currContext.GameObjectSetting.ComponentSettings)
                {
                    if (compSettings.ComponentName == typeName)
                    {
                        compSettings.IsSavingSettings = isSaveSetting;
                        //if current context is changed and the PlayModePersist panel is open, update the checkboxes
                        if (currContext.GameObj == context.GameObj && context.GameObjectSetting.Expanded)
                        {
                            EditorUtility.SetDirty(context.GameObj);
                        }
                        
                        break;
                    }
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlaying || EditorApplication.isPaused)
        {
            if (context == null)
            {
                context = new PPCurrentContext();
                allContexts.Add(context);
                context.SetContext(target as Transform);
            }
            else if (context.GameObj != ((Transform)target).gameObject)
            {
                bool isFound = false;
                for (int n = 0; n < allContexts.Count; n++)
                {
                    if (allContexts[n].GameObj == ((Transform)target).gameObject)
                    {
                        isFound = true;
                        context = allContexts[n];
                        break;
                    }
                }
                if (!isFound)
                {
                    context = new PPCurrentContext();
                    allContexts.Add(context);
                    context.SetContext(target as Transform);
                }
            }
        }

        if (context != null && null != context.GameObj)
        {
            PrefabType contextPrefabType = EditorUtility.GetPrefabType(context.GameObj);
            if (contextPrefabType != PrefabType.Prefab && contextPrefabType != PrefabType.ModelPrefab)
            {
                if (EditorApplication.isPlaying || EditorApplication.isPaused)
                {
                    PPGOSetting setting = context.GameObjectSetting;

                    setting.Expanded = EditorGUILayout.Foldout(setting.Expanded, "PlayModePersist");

                    if (setting.Expanded)
                    {
                        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("", GUILayout.Width(150f));

                        if (setting.ComponentSettings.Any(s => !s.IsSavingSettings))
                        {
                            setting.SaveAll = false;
                        }

                        bool wasSaveAll = setting.SaveAll;

                        setting.SaveAll = GUILayout.Toggle(setting.SaveAll, "Save All");

                        EditorGUILayout.EndHorizontal();

                        GUILayout.Box("", GUILayout.Height(1f), GUILayout.ExpandWidth(true));

                        if (setting.SaveAll && !wasSaveAll)
                        {
                            setting.ComponentSettings.ForEach(s => s.IsSavingSettings = true);
                        }
                        else if (!setting.SaveAll && wasSaveAll)
                        {
                            setting.ComponentSettings.ForEach(s => s.IsSavingSettings = false);
                        }

                        foreach (PPComponentSetting childSetting in setting.ComponentSettings)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Label(childSetting.ComponentName, GUILayout.Width(150f));

                            childSetting.IsSavingSettings = GUILayout.Toggle(childSetting.IsSavingSettings, "Save");

                            EditorGUILayout.EndHorizontal();
                        }

                        EditorGUILayout.EndVertical();
                    }

                    EditorApplication.playmodeStateChanged = Application_PlaymodeStateChanged;
                }
            }
            transformInspector.DrawDefaultInspector(target as Transform);
        }
        else
        {
            transformInspector.DrawDefaultInspector(target as Transform);
        }
    }

    private void Application_PlaymodeStateChanged()
    {
        if (EditorApplication.isPlaying || EditorApplication.isPaused)
        {
            AddAllContextsWithDefaults();
            for (int n = 0; n < allContexts.Count; n++)
            {
                allContexts[n].GameObjectSetting.StoreAllSelectedSettings();
            }
        }
        else
        {
            for (int n = 0; n < allContexts.Count; n++)
            {
                allContexts[n].GameObjectSetting.RestoreAllSelectedSettings();
            }

            allContexts.Clear();        
        }
    }

    private void AddAllContextsWithDefaults()
    {
        System.Type t;
        Assembly asm;

        foreach (string defaultTypeStr in PPLocalStorageManager.Defaults)
        {
            string[] parts = defaultTypeStr.Split('.');
            if (parts.Length == 1)
            {
                asm =  typeof(PPAssemblyLocator).Assembly;
                if (asm != null)
                {
                    t = asm.GetType(defaultTypeStr);
                    if (t != null)
                    {
                        AddMissingGameObjectsByType(t);
                    }
                }
            }
            else if (parts.Length==2)
            {
                asm = typeof(Component).Assembly;
                if (asm != null)
                {
                    t = asm.GetType(parts[0] + "." +parts[1]);
                    if (t != null)
                    {
                        AddMissingGameObjectsByType(t);
                    }
                }
            }
        }
    }

    private void AddMissingGameObjectsByType(System.Type t)
    {
        bool isFound;
        GameObject tempGO;
        Object[] objs = Object.FindObjectsOfType(t);
        PPCurrentContext newContext;

        foreach (Object obj in objs)
        {
            isFound = false;
            if (obj as Component != null && ((Component)obj).gameObject != null)
            {
                tempGO = ((Component)obj).gameObject;

                foreach (PPCurrentContext currContext in allContexts)
                {
                    if (currContext.GameObj == tempGO)
                    {
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    newContext = new PPCurrentContext();
                    allContexts.Add(newContext);
                    newContext.SetContext(tempGO.transform);
                }
            }
        }
    }
}