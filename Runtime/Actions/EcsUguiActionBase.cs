// ----------------------------------------------------------------------------
// The MIT License
// Ugui bindings https://github.com/Leopotam/ecslite-unity-ugui
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine;

namespace Leopotam.EcsLite.Unity.Ugui {
    enum EcsUguiActionNameRegistrationType {
        None,
        OnAwake,
        OnStart
    }

    public abstract class EcsUguiActionBase<T> : MonoBehaviour where T : struct {
        [SerializeField] string _widgetName;
        [SerializeField] EcsUguiEmitter _emitter;
        [SerializeField] EcsUguiActionNameRegistrationType _nameRegistrationType = EcsUguiActionNameRegistrationType.None;
        [SerializeField] UnityEngine.UI.Selectable _selectable;

        EcsPool<T> _pool;

        protected virtual void Awake () {
            if (_nameRegistrationType == EcsUguiActionNameRegistrationType.OnAwake) {
                ValidateEmitter ();
                _emitter.SetNamedObject (_widgetName, gameObject);
            }
        }

        void Start () {
            if (_nameRegistrationType == EcsUguiActionNameRegistrationType.OnStart) {
                ValidateEmitter ();
                _emitter.SetNamedObject (_widgetName, gameObject);
            }
        }

        void OnDestroy () {
            if (_emitter) {
                _emitter.SetNamedObject (_widgetName, null);
                _emitter = null;
            }
        }

        void ValidateEmitter () {
            if (_emitter == null) {
                _emitter = GetComponentInParent<EcsUguiEmitter> ();
            }
#if DEBUG
            if (_emitter == null) {
                Debug.LogError ("EcsUiEmitter not found in hierarchy", this);
            }
#endif
            _pool = _emitter.GetWorld ().GetPool<T> ();
        }

        protected bool IsValidForEvent () {
            if (!_emitter) { return false; }
#if DEBUG
            if (_emitter.GetWorld () == null) { throw new System.Exception ("[EcsUiEmitter] Call EcsSystems.InjectUi() first."); }
#endif
            return _emitter.GetWorld ().IsAlive () && (_selectable == null || _selectable.interactable);
        }

        protected ref T CreateEvent () {
            return ref _pool.Add (_emitter.GetWorld ().NewEntity ());
        }

        public string GetWidgetName () {
            return _widgetName;
        }
    }
}