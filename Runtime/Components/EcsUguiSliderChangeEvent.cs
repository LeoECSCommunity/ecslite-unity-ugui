// ----------------------------------------------------------------------------
// The MIT License
// Ugui bindings https://github.com/Leopotam/ecslite-unity-ugui
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine.UI;

namespace Leopotam.EcsLite.Unity.Ugui {
    public struct EcsUguiSliderChangeEvent {
        public string WidgetName;
        public Slider Sender;
        public float Value;
    }
}