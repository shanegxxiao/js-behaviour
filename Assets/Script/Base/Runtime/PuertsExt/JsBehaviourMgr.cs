using System.Collections.Generic;

namespace Base.Runtime
{
    public class JsBehaviourMgr: Singleton<JsBehaviourMgr>
    {
        /// <summary>
        /// 添加JsBehaviour到容器中，使用实例ID作为键添加到内部字典中
        /// </summary>
        /// <param name="jsb">JsBehaviour组件</param>
        public void Add(JsBehaviour jsb)
        {
            if (!jsb)
            {
                return;
            }
            instId2Jsb[jsb.GetInstanceID()] = jsb;
        }

        /// <summary>
        /// 根据InstanceID获取JsBehaviour
        /// </summary>
        /// <param name="instanceId">实例ID</param>
        /// <returns></returns>
        public JsBehaviour Get(int instanceId)
        {
            return instId2Jsb.TryGetValue(instanceId, out var jsb) ? jsb : null;
        }

        /// <summary>
        /// 移除instanceId对应的JsBehaviour
        /// </summary>
        /// <param name="instanceId">JsBehaviour的实例ID</param>
        public void Remove(int instanceId)
        {
            instId2Jsb.Remove(instanceId);
        }

        /// <summary>
        /// 为了Typescript脚本生成引用
        /// </summary>
        public static JsBehaviourMgr Instance => Inst;

        private readonly Dictionary<int, JsBehaviour> instId2Jsb = new Dictionary<int, JsBehaviour>();
    }
}