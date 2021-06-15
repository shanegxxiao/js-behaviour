using UnityEngine;

namespace Base.Runtime
{
    public partial class JsComponentProp
    {
        public void Set(string name, IntList value)
        {
            intListProperties[name] = value;
        }
        public IntList GetIntList(string name)
        {
            if (!intListProperties.TryGetValue(name, out var value))
            {
                value = new IntList();
                intListProperties[name] = value;
            }
            return value;
        }

        public void Set(string name, LongList value)
        {
            longListProperties[name] = value;
        }
        public LongList GetLongList(string name)
        {
            if (!longListProperties.TryGetValue(name, out var value))
            {
                value = new LongList();
                longListProperties[name] = value;
            }
            return value;
        }

        public void Set(string name, FloatList value)
        {
            floatListProperties[name] = value;
        }
        public FloatList GetFloatList(string name)
        {
            if (!floatListProperties.TryGetValue(name, out var value))
            {
                value = new FloatList();
                floatListProperties[name] = value;
            }
            return value;
        }

        public void Set(string name, DoubleList value)
        {
            doubleListProperties[name] = value;
        }
        public DoubleList GetDoubleList(string name)
        {
            if (!doubleListProperties.TryGetValue(name, out var value))
            {
                value = new DoubleList();
                doubleListProperties[name] = value;
            }
            return value;
        }

        public void Set(string name, StringList value)
        {
            stringListProperties[name] = value;
        }
        public StringList GetStringList(string name)
        {
            if (!stringListProperties.TryGetValue(name, out var value))
            {
                value = new StringList();
                stringListProperties[name] = value;
            }
            return value;
        }

        public void Set(string name, UEObjectList value)
        {
            ueObjectListProperties[name] = value;
        }
        public UEObjectList GetUEObjectList(string name)
        {
            if (!ueObjectListProperties.TryGetValue(name, out var value))
            {
                value = new UEObjectList();
                ueObjectListProperties[name] = value;
            }
            return value;
        }

        public void Set(string name, Vector2List value)
        {
            vector2ListProperties[name] = value;
        }
        public Vector2List GetVector2List(string name)
        {
            if (!vector2ListProperties.TryGetValue(name, out var value))
            {
                value = new Vector2List();
                vector2ListProperties[name] = value;
            }
            return value;
        }

        public void Set(string name, Vector3List value)
        {
            vector3ListProperties[name] = value;
        }
        public Vector3List GetVector3List(string name)
        {
            if (!vector3ListProperties.TryGetValue(name, out var value))
            {
                value = new Vector3List();
                vector3ListProperties[name] = value;
            }
            return value;
        }

        private void ClearPropList()
        {
            intListProperties.Clear();
            longListProperties.Clear();
            floatListProperties.Clear();
            doubleListProperties.Clear();
            stringListProperties.Clear();
            ueObjectListProperties.Clear();
        }

        [SerializeField]
        private SDictionaryStringIntList intListProperties = new SDictionaryStringIntList();
        [SerializeField]
        private SDictionaryStringLongList longListProperties = new SDictionaryStringLongList();
        [SerializeField]
        private SDictionaryStringFloatList floatListProperties = new SDictionaryStringFloatList();
        [SerializeField]
        private SDictionaryStringDoubleList doubleListProperties = new SDictionaryStringDoubleList();
        [SerializeField]
        private SDictionaryStringStringList stringListProperties = new SDictionaryStringStringList();
        [SerializeField]
        private SDictionaryStringUEObjectList ueObjectListProperties = new SDictionaryStringUEObjectList();
        [SerializeField]
        private SDictionaryStringVector2List vector2ListProperties = new SDictionaryStringVector2List();
        [SerializeField]
        private SDictionaryStringVector3List vector3ListProperties = new SDictionaryStringVector3List();
    }
}