// ----------------------------------------------------------------------------
// The MIT License
// Ugui bindings https://github.com/Leopotam/ecslite-unity-ugui
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.EcsLite.Unity.Ugui {
    [RequireComponent (typeof (ScrollRect))]
    public sealed class EcsUguiScrollViewAction : EcsUguiActionBase<EcsUguiScrollViewEvent> {
        ScrollRect _scrollView;

        protected override void Awake () {
            base.Awake ();
            _scrollView = GetComponent<ScrollRect> ();
            _scrollView.onValueChanged.AddListener (OnScrollViewValueChanged);
        }

        void OnScrollViewValueChanged (Vector2 value) {
            if (IsValidForEvent ()) {
                ref var msg = ref CreateEvent ();
                msg.WidgetName = GetWidgetName ();
                msg.Sender = _scrollView;
                msg.Value = value;
            }
        }
    }
}