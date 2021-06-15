namespace Base.Runtime
{
    public static class UnityEngineObjectExt
    {
        /// <summary>
        /// 用于在Typescript中判断UnityEngine对象是否为空，参考链接：https://github.com/Tencent/puerts/blob/master/doc/unity/faq.md#getcomponent%E5%9C%A8cs%E4%B8%BAnull%E4%BD%86%E5%9C%A8js%E8%B0%83%E7%94%A8%E5%8D%B4%E4%B8%8D%E4%B8%BAnull%E4%B8%BA%E4%BB%80%E4%B9%88
        /// </summary>
        /// <param name="obj">继承自UnityEngine.Object的对象</param>
        /// <returns>是否对象为空</returns>
        public static bool IsNull(this UnityEngine.Object obj)
        {
            return obj == null;
        }
    }
}