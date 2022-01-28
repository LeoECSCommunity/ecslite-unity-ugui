// ----------------------------------------------------------------------------
// The MIT License
// Ugui bindings https://github.com/Leopotam/ecslite-unity-ugui
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using TMPro;
using UnityEngine;

namespace Leopotam.EcsLite.Unity.Ugui {
    [RequireComponent (typeof (TMP_InputField))]
    public sealed class EcsUguiTmpInputChangeAction : EcsUguiActionBase<EcsUguiTmpInputChangeEvent> {
        TMP_InputField _input;

        protected override void Awake () {
            _input = GetComponent<TMP_InputField> ();
            _input.onValueChanged.AddListener (OnInputValueChanged);
        }

        void OnInputValueChanged (string value) {
            if (IsValidForEvent ()) {
                ref var msg = ref CreateEvent ();
                msg.WidgetName = GetWidgetName ();
                msg.Sender = _input;
                msg.Value = value;
            }
        }
    }
}