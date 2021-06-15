using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Editor
{
    public partial class JsBehaviourEditor : UnityEditor.Editor
    {
        private void ShowArrayProperty(Base.Runtime.PropertyInfo propertyInfo, Type arrayElementType)
        {
            if (!showArrayStatus.TryGetValue(propertyInfo.name, out var showArray))
            {
                showArrayStatus[propertyInfo.name] = false;
            }
            var newShowArray = EditorGUILayout.BeginFoldoutHeaderGroup(showArray, ObjectNames.NicifyVariableName(propertyInfo.name), EditorStyles.foldoutHeader,
                (rect) => { showArrayStatus[propertyInfo.name] = !showArrayStatus[propertyInfo.name]; });
            if (newShowArray != showArray)
            {
                showArrayStatus[propertyInfo.name] = newShowArray;
            }
            if (newShowArray)
            {
                ShowArrayElements(propertyInfo, arrayElementType);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void ShowArrayElements(Base.Runtime.PropertyInfo propertyInfo, Type arrayElementType)
        {
            if (arrayElementType.IsSubclassOf(typeof(UnityEngine.Object)) || arrayElementType == typeof(UnityEngine.Object))
            {
                ShowUEObjectElements(propertyInfo, arrayElementType);
            }
            else if (arrayElementType == typeof(Vector2))
            {
                ShowVector2Elements(propertyInfo, arrayElementType);
            }
            else if (arrayElementType == typeof(Vector3))
            {
                ShowVector3Elements(propertyInfo, arrayElementType);
            }
            else if (arrayElementType == typeof(System.Single))
            {
                ShowFloatElements(propertyInfo, arrayElementType);
            }
            else if (arrayElementType == typeof(System.Double))
            {
                ShowDoubleElements(propertyInfo, arrayElementType);
            }
            else if (arrayElementType == typeof(System.Int16) || arrayElementType == typeof(System.Int32) ||
                     arrayElementType == typeof(System.UInt16) || arrayElementType == typeof(System.UInt32))
            {
                ShowIntElements(propertyInfo, arrayElementType);
            }
            else if (arrayElementType == typeof(System.Int64) || arrayElementType == typeof(System.UInt64))
            {
                ShowLongElements(propertyInfo, arrayElementType);
            }
            else if (arrayElementType == typeof(System.String))
            {
                ShowStringElements(propertyInfo, arrayElementType);
            }
        }

        private void ShowUEObjectElements(Base.Runtime.PropertyInfo propertyInfo, Type arrayElementType)
        {
            var list = jsBehaviour.JsComponentProp.GetUEObjectList(propertyInfo.name);
            var newSize = EditorGUILayout.IntField(ObjectNames.NicifyVariableName("Size"), list.Data.Count);
            if (list.Data.Count != newSize)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                list.Resize(newSize);
            }
            for (var i = 0; i < list.Data.Count; ++i)
            {
                var newData = EditorGUILayout.ObjectField(ObjectNames.NicifyVariableName($"Element {i}"), list.Data[i], arrayElementType, true);
                if (newData != list.Data[i])
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    list.Data[i] = newData;
                }
            }
        }

        private void ShowVector2Elements(Base.Runtime.PropertyInfo propertyInfo, Type arrayElementType)
        {
            var list = jsBehaviour.JsComponentProp.GetVector2List(propertyInfo.name);
            var newSize = EditorGUILayout.IntField(ObjectNames.NicifyVariableName("Size"), list.Data.Count);
            if (list.Data.Count != newSize)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                list.Resize(newSize);
            }
            for (var i = 0; i < list.Data.Count; ++i)
            {
                var newData = EditorGUILayout.Vector2Field(ObjectNames.NicifyVariableName($"Element {i}"), list.Data[i]);
                if (newData != list.Data[i])
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    list.Data[i] = newData;
                }
            }
        }

        private void ShowVector3Elements(Base.Runtime.PropertyInfo propertyInfo, Type arrayElementType)
        {
            var list = jsBehaviour.JsComponentProp.GetVector3List(propertyInfo.name);
            var newSize = EditorGUILayout.IntField(ObjectNames.NicifyVariableName("Size"), list.Data.Count);
            if (list.Data.Count != newSize)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                list.Resize(newSize);
            }
            for (var i = 0; i < list.Data.Count; ++i)
            {
                var newData = EditorGUILayout.Vector3Field(ObjectNames.NicifyVariableName($"Element {i}"), list.Data[i]);
                if (newData != list.Data[i])
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    list.Data[i] = newData;
                }
            }
        }

        private void ShowFloatElements(Base.Runtime.PropertyInfo propertyInfo, Type arrayElementType)
        {
            var list = jsBehaviour.JsComponentProp.GetFloatList(propertyInfo.name);
            var newSize = EditorGUILayout.IntField(ObjectNames.NicifyVariableName("Size"), list.Data.Count);
            if (list.Data.Count != newSize)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                list.Resize(newSize);
            }
            for (var i = 0; i < list.Data.Count; ++i)
            {
                var newData = EditorGUILayout.FloatField(ObjectNames.NicifyVariableName($"Element {i}"), list.Data[i]);
                if (!Mathf.Approximately(newData, list.Data[i]))
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    list.Data[i] = newData;
                }
            }
        }

        private void ShowDoubleElements(Base.Runtime.PropertyInfo propertyInfo, Type arrayElementType)
        {
            var list = jsBehaviour.JsComponentProp.GetDoubleList(propertyInfo.name);
            var newSize = EditorGUILayout.IntField(ObjectNames.NicifyVariableName("Size"), list.Data.Count);
            if (list.Data.Count != newSize)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                list.Resize(newSize);
            }
            for (var i = 0; i < list.Data.Count; ++i)
            {
                var newData = EditorGUILayout.DoubleField(ObjectNames.NicifyVariableName($"Element {i}"), list.Data[i]);
                if (Math.Abs(newData - list.Data[i]) < double.Epsilon)
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    list.Data[i] = newData;
                }
            }
        }

        private void ShowIntElements(Base.Runtime.PropertyInfo propertyInfo, Type arrayElementType)
        {
            var list = jsBehaviour.JsComponentProp.GetIntList(propertyInfo.name);
            var newSize = EditorGUILayout.IntField(ObjectNames.NicifyVariableName("Size"), list.Data.Count);
            if (list.Data.Count != newSize)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                list.Resize(newSize);
            }
            for (var i = 0; i < list.Data.Count; ++i)
            {
                var newData = EditorGUILayout.IntField(ObjectNames.NicifyVariableName($"Element {i}"), list.Data[i]);
                if (newData != list.Data[i])
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    list.Data[i] = newData;
                }
            }
        }

        private void ShowLongElements(Base.Runtime.PropertyInfo propertyInfo, Type arrayElementType)
        {
            var list = jsBehaviour.JsComponentProp.GetLongList(propertyInfo.name);
            var newSize = EditorGUILayout.IntField(ObjectNames.NicifyVariableName("Size"), list.Data.Count);
            if (list.Data.Count != newSize)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                list.Resize(newSize);
            }
            for (var i = 0; i < list.Data.Count; ++i)
            {
                var newData = EditorGUILayout.LongField(ObjectNames.NicifyVariableName($"Element {i}"), list.Data[i]);
                if (newData != list.Data[i])
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    list.Data[i] = newData;
                }
            }
        }

        private void ShowStringElements(Base.Runtime.PropertyInfo propertyInfo, Type arrayElementType)
        {
            var list = jsBehaviour.JsComponentProp.GetStringList(propertyInfo.name);
            var newSize = EditorGUILayout.IntField(ObjectNames.NicifyVariableName("Size"), list.Data.Count);
            if (list.Data.Count != newSize)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                list.Resize(newSize);
            }
            for (var i = 0; i < list.Data.Count; ++i)
            {
                var newData = EditorGUILayout.TextField(ObjectNames.NicifyVariableName($"Element {i}"), list.Data[i]);
                if (newData != list.Data[i])
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    list.Data[i] = newData;
                }
            }
        }
    }
}
