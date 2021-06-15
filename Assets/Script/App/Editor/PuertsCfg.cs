/*
 * Tencent is pleased to support the open source community by making InjectFix available.
 * Copyright (C) 2019 THL A29 Limited, a Tencent company.  All rights reserved.
 * InjectFix is licensed under the MIT License, except for the third-party components listed in the file 'LICENSE' which may be subject to their corresponding license terms. 
 * This file is subject to the terms and conditions defined in file 'LICENSE', which is part of this source code package.
 */

using System.Collections.Generic;
using Puerts;
using System;
using System.Reflection;
using UnityEngine;

namespace App.Editor
{
    //1、配置类必须打[Configure]标签
    //2、必须放Editor目录
    [Configure]
    public class PuertsCfg
    {

        [Typing]
        static IEnumerable<Type> Typings
        {
            get
            {

                var types = new List<Type>();

                var namespaces = new HashSet<string>
                {
                    "System",
                    "System.IO",
                    "System.Net",
                    "System.Reflection",
                    "UnityEngine",
                    "UnityEngine.Networking",
                    "UnityEngine.ParticleSystem",
                    "UnityEngine.SceneManagement",
                    "UnityEngine.AI",
                    "UnityEngine.UI",
                    "UnityEngine.Events",
                    "UnityEngine.U2D",
                    "UnityEditor",
                    "Base.Mono",
                    "Base.Runtime",
                    "App.Mono",
                    "App.Runtime",
                    // TODO: 添加需要导出声明的命名空间
                };

                var ignored = new Dictionary<string, HashSet<string>>();
                var ignoredUnityEngineClasses = new HashSet<string>
                {
                    "ContextMenuItemAttribute",
                    "HashUnsafeUtilities",
                    "SpookyHash",
                    "ContextMenuItemAttribute",
                    "U",
                    // TODO: 添加需要忽略导出声明的类型
                };
                ignored.Add("UnityEngine", ignoredUnityEngineClasses);
                ignoredUnityEngineClasses = new HashSet<string>
                {
                    "ContextMenuItemAttribute",
                    // TODO: 添加需要忽略导出声明的类型
                };
                ignored.Add("UnityEditor", ignoredUnityEngineClasses);

                var registered = new Dictionary<string, HashSet<string>>();

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var name = assembly.GetName().Name;

                    foreach (var type in assembly.GetTypes())
                    {
                        if (!type.IsPublic) continue;
                        if (type.Name.Contains("<") || type.Name.Contains("*")) continue; // 忽略泛型，指针类型
                        if (type.Namespace == null || type.Name == null) continue;
                        if (registered.ContainsKey(type.Namespace) && registered[type.Namespace].Contains(type.Name))
                            continue; // 忽略重复的类
                        bool accept = namespaces.Contains(type.Namespace);
                        if (accept && ignored.ContainsKey(type.Namespace) &&
                            ignored[type.Namespace].Contains(type.Name)) continue;
                        if (accept)
                        {
                            types.Add(type);
                            if (!registered.ContainsKey(type.Namespace))
                            {
                                var classes = new HashSet<string>();
                                classes.Add(type.Name);
                                registered.Add(type.Namespace, classes);
                            }
                            else
                            {
                                registered[type.Namespace].Add((type.Name));
                            }
                        }
                    }
                }

                types.Add(typeof(System.Convert));
                types.Add(typeof(System.Text.Encoding));
                types.Add(typeof(UnityEngine.UI.Button.ButtonClickedEvent));
                types.Add(typeof(UnityEngine.EventSystems.EventSystem));
                types.Add(typeof(UnityEngine.EventSystems.StandaloneInputModule));
                types.Add(typeof(UnityEngine.EventSystems.BaseInput));
                types.Add(typeof(List<TMPro.TMP_FontAsset>));
                types.Add(typeof(Dictionary<string, string>));
                types.Add(typeof(KeyValuePair<string, string>));
                types.Add(typeof(Dictionary<string, string>.Enumerator));

                // TODO: 添加需要导出声明的类型

                return types;
            }
        }

        [Binding]
        static IEnumerable<Type> Bindings
        {
            get
            {
                var types = new List<Type>();
                var namespaces = new HashSet<string>
                {
                    "RuntimeInspectorNamespace"
                };
                Dictionary<string, HashSet<string>> ignored = new Dictionary<string, HashSet<string>>();
                // 忽略 tiny.EditorUtils 类
                HashSet<string> ignoredClasses = new HashSet<string>
                {
                    "EditorUtils"
                };
                ignored.Add("tiny", ignoredClasses);
                // TODO：在此处添加要忽略绑定的类型

                Dictionary<string, HashSet<string>> registered = new Dictionary<string, HashSet<string>>();
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var name = assembly.GetName().Name;
                    foreach (var type in assembly.GetTypes())
                    {
                        if (!type.IsPublic) continue;
                        if (type.Name.Contains("<") || type.Name.Contains("*")) continue; // 忽略泛型，指针类型
                        if (type.Namespace == null || type.Name == null) continue;
                        var accept = namespaces.Contains(type.Namespace);
                        if (accept && ignored.ContainsKey(type.Namespace) &&
                            ignored[type.Namespace].Contains(type.Name)) continue;
                        if (!accept) continue;
                        types.Add(type);
                        if (!registered.ContainsKey(type.Namespace))
                        {
                            var classes = new HashSet<string> { type.Name };
                            registered.Add(type.Namespace, classes);
                        }
                        else
                        {
                            registered[type.Namespace].Add((type.Name));
                        }
                    }
                }

                // 绑定 Unity常用类型
                types.Add(typeof(UnityEngine.Vector2));
                types.Add(typeof(UnityEngine.Vector3));
                types.Add(typeof(UnityEngine.Vector4));
                types.Add(typeof(UnityEngine.Quaternion));
                types.Add(typeof(UnityEngine.Color));
                types.Add(typeof(UnityEngine.Rect));
                types.Add(typeof(UnityEngine.Bounds));
                types.Add(typeof(UnityEngine.Ray));
                types.Add(typeof(UnityEngine.RaycastHit));
                types.Add(typeof(UnityEngine.Matrix4x4));
                types.Add(typeof(UnityEngine.Events.UnityEvent));

                types.Add(typeof(UnityEngine.Time));
                types.Add(typeof(UnityEngine.Transform));
                types.Add(typeof(UnityEngine.Object));
                types.Add(typeof(UnityEngine.GameObject));
                types.Add(typeof(UnityEngine.Component));
                types.Add(typeof(UnityEngine.Behaviour));
                types.Add(typeof(UnityEngine.MonoBehaviour));
                types.Add(typeof(UnityEngine.AudioClip));
                types.Add(typeof(UnityEngine.ParticleSystem.MainModule));
                types.Add(typeof(UnityEngine.AnimationClip));
                types.Add(typeof(UnityEngine.Animator));
                types.Add(typeof(UnityEngine.AnimationCurve));
                types.Add(typeof(UnityEngine.AndroidJNI));
                types.Add(typeof(UnityEngine.AndroidJNIHelper));
                types.Add(typeof(UnityEngine.Collider));
                types.Add(typeof(UnityEngine.Collision));
                types.Add(typeof(UnityEngine.Rigidbody));
                types.Add(typeof(UnityEngine.Screen));
                types.Add(typeof(UnityEngine.Texture));
                types.Add(typeof(UnityEngine.TextAsset));
                types.Add(typeof(UnityEngine.SystemInfo));
                types.Add(typeof(UnityEngine.Input));
                types.Add(typeof(UnityEngine.Mathf));

                types.Add(typeof(UnityEngine.Camera));
                types.Add(typeof(UnityEngine.ParticleSystem));
                types.Add(typeof(UnityEngine.AudioSource));
                types.Add(typeof(UnityEngine.AudioListener));
                types.Add(typeof(UnityEngine.Physics));
                types.Add(typeof(UnityEngine.SceneManagement.Scene));
                types.Add(typeof(UnityEngine.Networking.UnityWebRequest));
                return types;
            }
        }

        [Filter]
        static bool Filter(MemberInfo memberInfo)
        {
            var sig = memberInfo.ToString();
            if (sig.Contains("*"))
            {
                return true;
            }

            if (memberInfo.ReflectedType == null)
            {
                return false;
            }

            switch (memberInfo.ReflectedType.FullName)
            {
                case "UnityEngine.MonoBehaviour" when memberInfo.Name == "runInEditMode":
                case "UnityEngine.Input" when memberInfo.Name == "IsJoystickPreconfigured":
                case "UnityEngine.Texture" when memberInfo.Name == "imageContentsHash":
                    // TODO: 添加要忽略导出的类成员
                    return true;
                default:
                    return false;
            }
        }

        [BlittableCopy]
        static IEnumerable<Type> Blittables =>
            new List<Type>()
            {
                // 打开这个可以优化Vector3的GC，但需要开启unsafe编译
                typeof(Vector2),
                typeof(Vector3),
                typeof(Vector4),
                typeof(Quaternion),
                typeof(Color),
                typeof(Rect),
                typeof(Bounds),
                typeof(Ray),
                typeof(RaycastHit),
                typeof(Matrix4x4),
            };

        [Filter]
        static bool FilterMethods(System.Reflection.MemberInfo mb)
        {
            // 排除 MonoBehaviour.runInEditMode, 在 Editor 环境下可用发布后不存在
            return mb.DeclaringType == typeof(MonoBehaviour) && mb.Name == "runInEditMode";
        }

        [CodeOutputDirectoryAttribute]
        static string CodeOutputPath => UnityEngine.Application.dataPath + "/Script/App/Runtime/Gen/";
    }
}
