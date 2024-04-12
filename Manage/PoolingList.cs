using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

namespace Ljs
{
    public class PoolingList<T> where T : Pooling
    {
        private List<T> m_list;
        private List<T> m_activeList;
        private Transform m_owner; //hierachy gameobject
        private List<string> m_test;

        public PoolingList()
        {
            m_list = new List<T>();
            m_activeList = new List<T>();
            m_test = new List<string>();
        }

        public void SetTransform(Transform parent)
        {
            GameObject gam = new GameObject();
            gam.name = typeof(T).Name.ToString();
            gam.transform.SetParent(parent);

            //if (typeof(T) == typeof(UIWorld))
            //{
            //    gam.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            //    gam.AddComponent<CanvasScaler>();
            //    gam.AddComponent<GraphicRaycaster>();
            //}
            //else if (typeof(T) == typeof(UIHud))
            //{
            //    gam.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            //    gam.AddComponent<CanvasScaler>();
            //    gam.AddComponent<GraphicRaycaster>();
            //}

            m_owner = gam.transform;
        }

        public T Create(string text)
        {
            T item = SearchList(text, m_owner);
            if (item != null)
            {
                return item;
            }

            T resource = LoadResource(text);
            if (resource == null)
            {
                return null;
            }

            T instance = Object.Instantiate(resource);
            instance.transform.SetParent(m_owner, false);
            instance.name = instance.name.Replace("(Clone)", "");
            m_list.Add(instance);
            return instance;
        }
        public T Create(string text, Vector3 pos)
        {
            T item = SearchList(text, m_owner);
            if (item != null)
            {
                item.transform.position = pos;
                return item;
            }

            T resource = LoadResource(text);
            if (resource == null)
            {
                return null;
            }
            T instance = Object.Instantiate(resource, pos, Quaternion.identity);
            instance.transform.SetParent(m_owner, false);
            instance.name = instance.name.Replace("(Clone)", "");
            m_list.Add(instance);
            return instance;
        }
        public T Create(string text, Transform parent)
        {
            if (parent == null)
            {
                Debug.LogError(text + " parent null");
                return null;
            }

            T item = SearchList(text, parent);
            if (item != null)
            {
                return item;
            }

            T resource = LoadResource(text);
            if (resource == null)
            {
                return null;
            }

            T instance = Object.Instantiate(resource);
            instance.transform.SetParent(parent, false);
            instance.name = instance.name.Replace("(Clone)", "");
            m_list.Add(instance);
            return instance;
        }

        private T LoadResource(string text) //리소스 접근
        {
            string path = typeof(T).Name + "/" + text;

#if BundleMode
            T val = null;

            if (AssetBundleManager.Instance.FileChecker(path))
            {
                GameObject _obj = AssetBundleManager.Instance.Load<GameObject>(path);
                val = _obj.GetComponent<T>();
            }
            else
            {
                val = Resources.Load<T>(path);
            }
#else
            T val = Resources.Load<T>(path);
#endif
            if (val == null)
            {
                Debug.LogError(text + " is null");
                return null;
            }
            return val;
        }

        private T SearchList(string text, Transform parent)
        {
            foreach (T t in m_list)
            {
                if (t.name == text && !t.isActiveAndEnabled && t.transform.parent == parent)
                {
                    t.gameObject.SetActive(true);
                    return t;
                }
            }
            return null;
        }

        public List<T> GetList()
        {
            m_activeList.Clear();
            for (int i = 0; i < m_list.Count; ++i)
            {
                T temp = m_list[i];
                if (temp.isActiveAndEnabled == true)
                {
                    m_activeList.Add(temp);
                }
            }
            return m_activeList;
        }
        public List<U> GetList<U>() where U : T
        {
            List<U> list = new List<U>();

            for (int i = 0; i < m_list.Count; ++i)
            {
                T temp = m_list[i];
                if (temp.isActiveAndEnabled == true && temp is U)
                {
                    list.Add((U)temp);
                }
            }
            return list;
        }
        public List<T> GetList(string name)
        {
            List<T> list = new List<T>();

            for (int i = 0; i < m_list.Count; ++i)
            {
                T temp = m_list[i];
                if (temp.isActiveAndEnabled == true && temp.name == name)
                {
                    list.Add(temp);
                }
            }
            return list;
        }

        public void Clear()
        {
            for (int i = 0; i < m_list.Count; ++i)
            {
                if (!m_list[i].NoDestroyLoadScene)
                {
                    if (m_list[i] == null)
                    {
                        Debug.LogError("잡았다");
                        string temp = "";
                        foreach (T t in m_list)
                        {
                            if (t != null)
                            {
                                temp += t.name + "/";
                            }
                        }
                        Debug.LogError(temp);

                        temp = "";
                        foreach (string t in m_test)
                        {
                            temp += t + "/";
                        }
                        Debug.LogError(temp);
                    }
                    else
                    {
                        Object.Destroy(m_list[i].gameObject);
                    }
                }
            }
            m_list.Clear();
        }

        void TestFun(string value)
        {
            if (!m_test.Contains(value))
            {
                m_test.Add(value);
            }
        }
    }
}
