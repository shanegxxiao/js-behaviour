using UnityEngine;

namespace Base.Runtime
{
    public partial class JsComponentProp
    {
        public void Set(string name, int value)
        {
            intProperties[name] = value;
        }
        public int GetInt(string name)
        {
            intProperties.TryGetValue(name, out var value);
            return value;
        }

        public void Set(string name, long value)
        {
            longProperties[name] = value;
        }
        public long GetLong(string name)
        {
            longProperties.TryGetValue(name, out var value);
            return value;
        }

        public void Set(string name, float value)
        {
            floatProperties[name] = value;
        }
        public float GetFloat(string name)
        {
            floatProperties.TryGetValue(name, out var value);
            return value;
        }

        public void Set(string name, double value)
        {
            doubleProperties[name] = value;
        }
        public double GetDouble(string name)
        {
            doubleProperties.TryGetValue(name, out var value);
            return value;
        }

        public void Set(string name, string value)
        {
            stringProperties[name] = value;
        }
        public string GetString(string name)
        {
            stringProperties.TryGetValue(name, out var value);
            return value;
        }

        public void Set(string name, UnityEngine.Object value)
        {
            ueObjectProperties[name] = value;
        }
        public UnityEngine.Object GetUEObject(string name)
        {
            ueObjectProperties.TryGetValue(name, out var value);
            return value;
        }

        public void Set(string name, Vector2 value)
        {
            vector2Properties[name] = value;
        }
        public Vector2 GetVector2(string name)
        {
            vector2Properties.TryGetValue(name, out var value);
            return value;
        }

        public void Set(string name, Vector3 value)
        {
            vector3Properties[name] = value;
        }
        public Vector3 GetVector3(string name)
        {
            vector3Properties.TryGetValue(name, out var value);
            return value;
        }

        private void ClearPropElement()
        {
            intProperties.Clear();
            longProperties.Clear();
            floatProperties.Clear();
            doubleProperties.Clear();
            stringProperties.Clear();
            ueObjectProperties.Clear();
        }

        [SerializeField]
        private SDictionaryStringInt intProperties = new SDictionaryStringInt();
        [SerializeField]
        private SDictionaryStringLong longProperties = new SDictionaryStringLong();
        [SerializeField]
        private SDictionaryStringFloat floatProperties = new SDictionaryStringFloat();
        [SerializeField]
        private SDictionaryStringDouble doubleProperties = new SDictionaryStringDouble();
        [SerializeField]
        private SDictionaryStringString stringProperties = new SDictionaryStringString();
        [SerializeField]
        private SDictionaryStringUEObject ueObjectProperties = new SDictionaryStringUEObject();
        [SerializeField]
        private SDictionaryStringVector2 vector2Properties = new SDictionaryStringVector2();
        [SerializeField]
        private SDictionaryStringVector3 vector3Properties = new SDictionaryStringVector3();
    }
}