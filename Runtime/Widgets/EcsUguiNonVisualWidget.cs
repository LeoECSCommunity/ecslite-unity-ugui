// ----------------------------------------------------------------------------
// The MIT License
// Ugui bindings https://github.com/Leopotam/ecslite-unity-ugui
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.EcsLite.Unity.Ugui {
    [RequireComponent (typeof (CanvasRenderer))]
    [RequireComponent (typeof (RectTransform))]
    public class EcsUguiNonVisualWidget : Graphic {
        public override void SetMaterialDirty () { }
        public override void SetVerticesDirty () { }
        public override Material material { get => defaultMaterial; set { } }
        public override void Rebuild (CanvasUpdate update) { }
    }
}