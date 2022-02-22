// ----------------------------------------------------------------------------
// The MIT License
// Ugui bindings https://github.com/Leopotam/ecslite-unity-ugui
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEditor.UI;

namespace Leopotam.EcsLite.Unity.Ugui.Editor {
    [CustomEditor (typeof (EcsUguiNonVisualWidget), false)]
    [CanEditMultipleObjects]
    sealed class EcsUguiNonVisualWidgetInspector : GraphicEditor {
        public override void OnInspectorGUI () {
            serializedObject.Update ();
            EditorGUILayout.PropertyField (m_Script);
            RaycastControlsGUI ();
            serializedObject.ApplyModifiedProperties ();
        }
    }
}