using System;
using System.Collections.Generic;
using Base.Runtime;
using Puerts;
using UnityEngine;

namespace Base.Editor
{
    public class JsEnvEditor : IDisposable
    {
        public JsEnvEditor()
        {
            try
            {
                ParseJsBehaviourInfos();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void Tick()
        {
            jsEnv.Tick();
        }

        public void Dispose()
        {
            jsEnv.Dispose();
        }

        /// <summary>
        /// 获取指定名字的组件信息
        /// </summary>
        /// <param name="componentName">组件名</param>
        /// <returns>组件信息</returns>
        public ComponentInfo GetJsComponentInfo(string componentName)
        {
            if (string.IsNullOrEmpty(componentName))
            {
                return null;
            }
            return componentInfos.TryGetValue(componentName, out var componentInfo) ? componentInfo : null;
        }

        /// <summary>
        /// 所有的组件信息
        /// </summary>
        public Dictionary<string, ComponentInfo> ComponentInfos => componentInfos;

        private void ParseJsBehaviourInfos()
        {
            componentInfos = JsComponentInfoParser.GetAllComponentInfo();
        }

        private readonly JsEnv jsEnv = new JsEnv(new JsLoaderEditor());
        private Dictionary<string, ComponentInfo> componentInfos;
    }
}
