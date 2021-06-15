using System;
using UnityEngine;

namespace Base.Runtime
{
    public class JsBehaviour : MonoBehaviour
    {
        protected void Awake()
        {
            JsBehaviourMgr.Instance.Add(this);
            if (string.IsNullOrEmpty(JsComponentName))
            {
                return;
            }
            jsComponentId = JsEnvRuntime.Inst.JsEnv.Eval<int>($"app.compInstMgr.newComponent('{JsComponentName}')");
            if (jsComponentId < 0)
            {
                return;
            }
            jsAwake = JsEnvRuntime.Inst.JsEnv.Eval<Action>($"app.compInstMgr.getComponentMethod({jsComponentId}, 'Awake')");
            jsStart = JsEnvRuntime.Inst.JsEnv.Eval<Action>($"app.compInstMgr.getComponentMethod({jsComponentId}, 'Start')");
            jsOnEnable = JsEnvRuntime.Inst.JsEnv.Eval<Action>($"app.compInstMgr.getComponentMethod({jsComponentId}, 'OnEnable')");
            jsOnDisable = JsEnvRuntime.Inst.JsEnv.Eval<Action>($"app.compInstMgr.getComponentMethod({jsComponentId}, 'OnDisable')");
            jsOnDestroy = JsEnvRuntime.Inst.JsEnv.Eval<Action>($"app.compInstMgr.getComponentMethod({jsComponentId}, 'OnDestroy')");

            jsBindProperty = JsEnvRuntime.Inst.JsEnv.Eval<Action<int>>($"app.compInstMgr.getComponentMethod({jsComponentId}, 'BindProperty')");
            jsUnbindProperty = JsEnvRuntime.Inst.JsEnv.Eval<Action>($"app.compInstMgr.getComponentMethod({jsComponentId}, 'UnbindProperty')");

            jsBindProperty?.Invoke(GetInstanceID());
            jsAwake?.Invoke();
        }

        protected void Start()
        {
            jsStart?.Invoke();
        }

        protected void OnEnable()
        {
            jsOnEnable?.Invoke();
        }

        protected void OnDisable()
        {
            jsOnDisable?.Invoke();
        }

        protected void OnDestroy()
        {
            jsOnDestroy?.Invoke();
            jsUnbindProperty?.Invoke();
            jsAwake = null;
            jsStart = null;
            jsOnEnable = null;
            jsOnDisable = null;
            jsOnDestroy = null;
            JsEnvRuntime.Inst.JsEnv.Eval($"app.compInstMgr.delComponent({jsComponentId})");
            JsBehaviourMgr.Instance.Remove(GetInstanceID());
        }

        [SerializeField] public string JsComponentName;
        [SerializeField] public JsComponentProp JsComponentProp;

        /// <summary>
        /// js组件实例ID
        /// </summary>
        private int jsComponentId;

        private Action jsAwake;
        private Action jsStart;
        private Action jsOnEnable;
        private Action jsOnDisable;
        private Action jsOnDestroy;

        private Action<int> jsBindProperty;
        private Action jsUnbindProperty;
    }
}