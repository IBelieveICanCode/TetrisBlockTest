using UnityEngine;

namespace ObjectPool
{
    public class PrefabFactory<T> : IFactory<T> where T : MonoBehaviour
    {
        readonly GameObject _prefab;
        readonly string _name;
        int _index = 0;

        public PrefabFactory(GameObject prefab) : this(prefab, prefab.name) { }

        public PrefabFactory(GameObject prefab, string name)
        {
            this._prefab = prefab;
            this._name = name;
        }

        public T Create()
        {
            GameObject tempGameObject = GameObject.Instantiate(_prefab) as GameObject;
            tempGameObject.transform.position = Vector3.down * 100;
            tempGameObject.name = _name + _index.ToString();
            T objectOfType = tempGameObject.GetComponent<T>();
            _index++;
            return objectOfType;
        }
    }
}