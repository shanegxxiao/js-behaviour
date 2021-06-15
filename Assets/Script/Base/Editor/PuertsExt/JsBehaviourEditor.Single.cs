using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Editor
{
    public partial class JsBehaviourEditor : UnityEditor.Editor
    {
        private void ShowSingleProperty(Base.Runtime.PropertyInfo propertyInfo, Type propertyType)
        {
            if (propertyType.IsSubclassOf(typeof(UnityEngine.Object)) || propertyType == typeof(UnityEngine.Object))
            {
                ShowObjectField(propertyInfo, propertyType);
            }
            else if (propertyType == typeof(Vector2))
            {
                ShowVector2Field(propertyInfo, propertyType);
            }
            else if (propertyType == typeof(Vector3))
            {
                ShowVector3Field(propertyInfo, propertyType);
            }
            else if (propertyType == typeof(System.Single))
            {
                ShowSingleField(propertyInfo, propertyType);
            }
            else if (propertyType == typeof(System.Double))
            {
                ShowDoubleField(propertyInfo, propertyType);
            }
            else if (propertyType == typeof(System.Int16) || propertyType == typeof(System.Int32) ||
                     propertyType == typeof(System.UInt16) || propertyType == typeof(System.UInt32))
            {
                ShowIntField(propertyInfo, propertyType);
            }
            else if (propertyType == typeof(System.Int64) || propertyType == typeof(System.UInt64))
            {
                ShowLongField(propertyInfo, propertyType);
            }
            else if (propertyType == typeof(System.String))
            {
                ShowStringField(propertyInfo, propertyType);
            }
        }

        private void ShowObjectField(Base.Runtime.PropertyInfo propertyInfo, Type propertyType)
        {
            var oldValue = jsBehaviour.JsComponentProp.GetUEObject(propertyInfo.name);
            var newValue = EditorGUILayout.ObjectField(ObjectNames.NicifyVariableName(propertyInfo.name), oldValue, propertyType, true);
            if (oldValue != newValue)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                jsBehaviour.JsComponentProp.Set(propertyInfo.name, newValue);
            }
        }

        private void ShowVector2Field(Base.Runtime.PropertyInfo propertyInfo, Type propertyType)
        {
            var oldValue = jsBehaviour.JsComponentProp.GetVector2(propertyInfo.name);
            var newValue = EditorGUILayout.Vector2Field(ObjectNames.NicifyVariableName(propertyInfo.name), oldValue);
            if (oldValue != newValue)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                jsBehaviour.JsComponentProp.Set(propertyInfo.name, newValue);
            }
        }

        private void ShowVector3Field(Base.Runtime.PropertyInfo propertyInfo, Type propertyType)
        {
            var oldValue = jsBehaviour.JsComponentProp.GetVector3(propertyInfo.name);
            var newValue = EditorGUILayout.Vector3Field(ObjectNames.NicifyVariableName(propertyInfo.name), oldValue);
            if (oldValue != newValue)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                jsBehaviour.JsComponentProp.Set(propertyInfo.name, newValue);
            }
        }

        private void ShowSingleField(Base.Runtime.PropertyInfo propertyInfo, Type propertyType)
        {
            var oldValue = jsBehaviour.JsComponentProp.GetFloat(propertyInfo.name);
            var newValue = EditorGUILayout.FloatField(ObjectNames.NicifyVariableName(propertyInfo.name), oldValue);
            if (!Mathf.Approximately(oldValue, newValue))
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                jsBehaviour.JsComponentProp.Set(propertyInfo.name, newValue);
            }
        }

        private void ShowDoubleField(Base.Runtime.PropertyInfo propertyInfo, Type propertyType)
        {
            var oldValue = jsBehaviour.JsComponentProp.GetDouble(propertyInfo.name);
            var newValue = EditorGUILayout.DoubleField(ObjectNames.NicifyVariableName(propertyInfo.name), oldValue);
            if (Math.Abs(oldValue - newValue) < double.Epsilon)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                jsBehaviour.JsComponentProp.Set(propertyInfo.name, newValue);
            }
        }

        private void ShowIntField(Base.Runtime.PropertyInfo propertyInfo, Type propertyType)
        {
            var oldValue = jsBehaviour.JsComponentProp.GetInt(propertyInfo.name);
            var newValue = EditorGUILayout.IntField(ObjectNames.NicifyVariableName(propertyInfo.name), oldValue);
            if (oldValue != newValue)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                jsBehaviour.JsComponentProp.Set(propertyInfo.name, newValue);
            }
        }

        private void ShowLongField(Base.Runtime.PropertyInfo propertyInfo, Type propertyType)
        {
            var oldValue = jsBehaviour.JsComponentProp.GetInt(propertyInfo.name);
            var newValue = EditorGUILayout.LongField(ObjectNames.NicifyVariableName(propertyInfo.name), oldValue);
            if (oldValue != newValue)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                jsBehaviour.JsComponentProp.Set(propertyInfo.name, newValue);
            }
        }

        private void ShowStringField(Base.Runtime.PropertyInfo propertyInfo, Type propertyType)
        {
            var oldValue = jsBehaviour.JsComponentProp.GetString(propertyInfo.name);
            var newValue = EditorGUILayout.TextField(ObjectNames.NicifyVariableName(propertyInfo.name), oldValue);
            if (oldValue != newValue)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                jsBehaviour.JsComponentProp.Set(propertyInfo.name, newValue);
            }
        }
    }
}
