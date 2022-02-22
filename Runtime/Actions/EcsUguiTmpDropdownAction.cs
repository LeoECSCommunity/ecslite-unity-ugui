// ----------------------------------------------------------------------------
// The MIT License
// Ugui bindings https://github.com/Leopotam/ecslite-unity-ugui
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using TMPro;
using UnityEngine;

namespace Leopotam.EcsLite.Unity.Ugui {
    [RequireComponent (typeof (TMP_Dropdown))]
    public sealed class EcsUguiTmpDropdownAction : EcsUguiActionBase<EcsUguiTmpDropdownChangeEvent> {
        TMP_Dropdown _dropdown;

        protected override void Awake () {
            _dropdown = GetComponent<TMP_Dropdown> ();
            _dropdown.onValueChanged.AddListener (OnDropdownValueChanged);
        }

        void OnDropdownValueChanged (int value) {
            if (IsValidForEvent ()) {
                ref var msg = ref CreateEvent ();
                msg.WidgetName = GetWidgetName ();
                msg.Sender = _dropdown;
                msg.Value = value;
            }
        }
    }
}