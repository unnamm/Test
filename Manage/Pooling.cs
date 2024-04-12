//모든 인게임 객체

using UnityEngine;

namespace Ljs
{
    public class Pooling : MonoBehaviour
    {
        private bool m_onPlay;

        public bool NoDestroyLoadScene { get; set; }
        public new string name { get; set; }

        private void Awake()
        {
            name = base.name;
            NoDestroyLoadScene = false;

            if (!Util.IsIngame())
                return;

            OnAwake();
        }

        private void OnEnable()
        {
            if (PoolingManager.IsLoad)
                return;
            if (!Util.IsIngame())
                return;

            m_onPlay = true;
            OnInit();
        }

        protected void Start()
        {
            if (!Util.IsIngame())
                return;

            OnStart();
        }

        private void Update()
        {
            if (!Util.IsIngame())
                return;

            if (m_onPlay)
            {
                m_onPlay = false;
                OnActive();
            }
            OnUpdate();
        }

        public void Remove()
        {
            if (PoolingManager.IsLoad)
                return;

            if (Util.IsIngame())
            {
                OnRemove();
            }

            if (!(this is Effect)) //이펙트는 이펙트가 다 사라지고 끌 수 있도록
                gameObject.SetActive(false);
        }

        //즉시 프레임에 실행
        protected virtual void OnAwake() { } //할당, 초기화
        protected virtual void OnInit() { } //켜질때 마다 초기화

        //같은 프레임에 실행하지만 생성된 함수 이후에 실행
        protected virtual void OnStart() { } //한번 실행

        //다음 프레임부터 실행
        protected virtual void OnActive() { } //켜질때 마다 실행
        protected virtual void OnUpdate() { }

        protected virtual void OnRemove() { } //제거할 때 실행
    }
}
