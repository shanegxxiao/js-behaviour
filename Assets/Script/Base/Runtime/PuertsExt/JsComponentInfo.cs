using System;
using System.Collections.Generic;

namespace Base.Runtime
{
    [Serializable]
    public class ComponentInfo
    {
        public string name;
        public string category;
        public Dictionary<string, PropertyInfo> properties;
#if UNITY_EDITOR
        public string tsFilePath;
#endif
    }

    [Serializable]
    public class PropertyInfo
    {
        public string name;
        public string type;
        public bool isArray;
        public bool editable;
    }
}