using UnityEngine;
using static Ljs.PersonModel;

namespace Ljs
{
    public class PoolingManager : MonoSingleTon<PoolingManager>
    {
        //public static PoolingList<UIHud> UIHud;
        //public static PoolingList<UIWorld> UIWorld;
        public static PoolingList<Effect> Effect;
        public static PoolingList<Character> Character;
        public static PoolingList<Pooling> Pooling;
        public static PoolingList<Weapon> Weapon;
        public static PoolingList<InGameObject> InGameObject;

        public static bool IsLoad;

        private Shader m_invi;

        private void Awake()
        {
            m_invi = Shader.Find("Invisible/InvisibleShadowCaster");

            //gameObject.AddComponent<EventSystem>();
            //gameObject.AddComponent<StandaloneInputModule>();

            //UIHud = new PoolingList<UIHud>();
            //UIHud.SetTransform(transform);

            //UIWorld = new PoolingList<UIWorld>();
            //UIWorld.SetTransform(transform);

            Character = new PoolingList<Character>();
            Character.SetTransform(transform);

            Effect = new PoolingList<Effect>();
            Effect.SetTransform(transform);

            Pooling = new PoolingList<Pooling>();
            Pooling.SetTransform(transform);

            Weapon = new PoolingList<Weapon>();
            Weapon.SetTransform(transform);

            InGameObject = new PoolingList<InGameObject>();
            InGameObject.SetTransform(transform);
        }

        public void Init()
        {
            AttackLayer = LayerMask.GetMask(LayerName.Default, LayerName.Ground, LayerName.Wood, LayerName.Metal, LayerName.Sea, LayerName.Stone, LayerName.Glass);
            AllLayer = LayerMask.GetMask(LayerName.Default, LayerName.Ground, LayerName.Wood, LayerName.Metal, LayerName.Sea, LayerName.Stone, LayerName.Glass, LayerName.Mine);
            ObjectLayer = LayerMask.GetMask(LayerName.Ground, LayerName.Wood, LayerName.Metal, LayerName.Sea, LayerName.Stone, LayerName.Glass);
        }

        public static void PreLoad()
        {
            IsLoad = true;

            //{
            //    Pooling[] list = Resources.LoadAll<UIHud>(typeof(UIHud).Name);
            //    for (int i = 0; i < list.Length; ++i)
            //    {
            //        list[i] = UIHud.Create(list[i].name);
            //        list[i].gameObject.SetActive(false);
            //    }
            //}
            //{
            //    Pooling[] list = Resources.LoadAll<UIWorld>(typeof(UIWorld).Name);
            //    for (int i = 0; i < list.Length; ++i)
            //    {
            //        list[i] = UIWorld.Create(list[i].name);
            //        list[i].gameObject.SetActive(false);
            //    }
            //}
            {
                Object[] list = Resources.LoadAll<Effect>("Effect");
                for (int i = 0; i < list.Length; ++i)
                {
                    Effect eff = Effect.Create(list[i].name);
                    eff.gameObject.SetActive(false);
                }
            }
            //{
            //    Character[] list = Resources.LoadAll<Character>(typeof(Character).Name);
            //    for (int i = 0; i < list.Length; ++i)
            //    {
            //        list[i] = Character.Create(list[i].name);
            //        list[i].gameObject.SetActive(false);
            //    }
            //}

            //{
            //    Pooling[] list = Resources.LoadAll<Pooling>(typeof(Pooling).Name);
            //    for (int i = 0; i < list.Length; ++i)
            //    {
            //        list[i] = Pooling.Create(list[i].name);
            //        list[i].gameObject.SetActive(false);
            //    }
            //}

            //{
            //    Weapon[] list = Resources.LoadAll<Weapon>(typeof(Weapon).Name);
            //    for (int i = 0; i < list.Length; ++i)
            //    {
            //        list[i] = Weapon.Create(list[i].name);
            //        list[i].gameObject.SetActive(false);
            //    }
            //}

            //{
            //    InGameObject[] list = Resources.LoadAll<InGameObject>(typeof(InGameObject).Name);
            //    for (int i = 0; i < list.Length; ++i)
            //    {
            //        list[i] = InGameObject.Create(list[i].name);
            //        list[i].gameObject.SetActive(false);
            //    }
            //}

            IsLoad = false;
        }

        public static void ClearAll()
        {
            //UIHud.Clear();
            //UIWorld.Clear();
            Effect.Clear();
            Character.Clear();
            Pooling.Clear();
            Weapon.Clear();
            InGameObject.Clear();
        }
    }
}
