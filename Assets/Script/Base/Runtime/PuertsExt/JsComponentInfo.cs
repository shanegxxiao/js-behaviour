using System;
using System.Collections.Generic;

namespace Base.Runtime
{
    [Serializable]
    public class ComponentInfo
    {
        public string name;
        public string path;
        public Dictionary<string, PropertyInfo> properties;
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