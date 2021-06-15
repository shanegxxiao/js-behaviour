using System;

namespace Base.Runtime
{
    [Serializable]
    public partial class JsComponentProp
    {
        public void Clear()
        {
            ClearPropElement();
            ClearPropList();
        }

        public Type GetType(string typeName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var type = assembly.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            } 
            return null;
        }

        public bool IsUEObject(string typeName)
        {
            var propertyType = GetType(typeName);
            if (propertyType == null)
            {
                return false;
            }
            return propertyType.IsSubclassOf(typeof(UnityEngine.Object)) || propertyType == typeof(UnityEngine.Object);
        }
    }
}