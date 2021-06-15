using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Base.Runtime
{
    /// <summary>
    /// 可以序列化的Dictionary，需要泛型实例化使用
    /// </summary>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        /// <summary>
        /// 序列化前将字典数据保存到List中
        /// </summary>
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (var pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        /// <summary>
        /// 反序列化后将List中的数据恢复到字典
        /// </summary>
        public void OnAfterDeserialize()
        {
            this.Clear();

            if (keys.Count != values.Count)
                throw new Exception($"There are {keys.Count} keys and {values.Count} values after deserialization. Make sure that both key and value types are serializable.");

            for (var i = 0; i < keys.Count; i++)
                Add(keys[i], values[i]);
        }

        [SerializeField]
        private List<TKey> keys = new List<TKey>();

        [SerializeField]
        private List<TValue> values = new List<TValue>();
    }


    [Serializable] public class SDictionaryStringInt : SerializableDictionary<string, int> { }
    [Serializable] public class SDictionaryStringLong : SerializableDictionary<string, long> { }
    [Serializable] public class SDictionaryStringFloat : SerializableDictionary<string, float> { }
    [Serializable] public class SDictionaryStringDouble : SerializableDictionary<string, double> { }
    [Serializable] public class SDictionaryStringString : SerializableDictionary<string, string> { }
    [Serializable] public class SDictionaryStringUEObject : SerializableDictionary<string, UnityEngine.Object> { }
    [Serializable] public class SDictionaryStringVector2 : SerializableDictionary<string, Vector2> { }
    [Serializable] public class SDictionaryStringVector3 : SerializableDictionary<string, Vector3> { }

    [Serializable] public class SDictionaryStringIntList : SerializableDictionary<string, IntList> { }
    [Serializable] public class SDictionaryStringLongList: SerializableDictionary<string, LongList> { }
    [Serializable] public class SDictionaryStringFloatList: SerializableDictionary<string, FloatList> { }
    [Serializable] public class SDictionaryStringDoubleList: SerializableDictionary<string, DoubleList> { }
    [Serializable] public class SDictionaryStringStringList: SerializableDictionary<string, StringList> { }
    [Serializable] public class SDictionaryStringUEObjectList: SerializableDictionary<string, UEObjectList> { }
    [Serializable] public class SDictionaryStringVector2List: SerializableDictionary<string, Vector2List> { }
    [Serializable] public class SDictionaryStringVector3List: SerializableDictionary<string, Vector3List> { }

    [Serializable]
    public class TypedList<T>
    {
        public void Resize(int newSize)
        {
            var deltaSize = Math.Abs(newSize - Data.Count);
            if (newSize > Data.Count)
            {
                for (int i = 0; i < deltaSize; i++)
                {
                    Data.Add(default);
                }
            }
            else
            {
                Data.RemoveRange(Data.Count - deltaSize, deltaSize);
            }
        }

        [SerializeField]
        public List<T> Data = new List<T>();
    }

    [Serializable]
    public class IntList: TypedList<int> { }
    [Serializable]
    public class LongList : TypedList<long> { }
    [Serializable]
    public class FloatList : TypedList<float> { }
    [Serializable]
    public class DoubleList : TypedList<double> { }
    [Serializable]
    public class StringList : TypedList<string> { }
    [Serializable]
    public class UEObjectList : TypedList<UnityEngine.Object> { }
    [Serializable]
    public class Vector2List : TypedList<Vector2> { }
    [Serializable]
    public class Vector3List : TypedList<Vector3> { }
}