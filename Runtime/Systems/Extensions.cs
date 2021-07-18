// ----------------------------------------------------------------------------
// The MIT License
// Ugui bindings https://github.com/Leopotam/ecslite-unity-ugui
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using System;
using System.Reflection;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Leopotam.EcsLite.Unity.Ugui {
    public sealed class EcsUguiNamedAttribute : Attribute {
        public readonly string Name;

        public EcsUguiNamedAttribute (string name) {
            Name = name;
        }
    }

    public static class EcsSystemsExtensions {
        /// <summary>
        /// Injects named UI objects and Emitter to all systems added to EcsSystems.
        /// </summary>
        /// <param name="ecsSystems">EcsSystems group.</param>
        /// <param name="emitter">EcsUiEmitter instance.</param>
        /// <param name="worldName">World name.</param>
        /// <param name="skipNoExists">Not throw exception if named action not registered in emitter.</param>
        /// <param name="skipOneFrames">Skip OneFrame-event cleanup registration.</param>
        public static EcsSystems InjectUgui (this EcsSystems ecsSystems, EcsUguiEmitter emitter, string worldName = "ugui-events", bool skipNoExists = false, bool skipOneFrames = false) {
            if (!skipOneFrames) {
                InjectOneFrames (ecsSystems);
            }
            emitter.SetWorld (ecsSystems.GetWorld (worldName));
            var uiNamedType = typeof (EcsUguiNamedAttribute);
            var goType = typeof (GameObject);
            var componentType = typeof (Component);
            var emitterType = typeof (EcsUguiEmitter);
            IEcsSystem[] systems = null;
            var systemsCount = ecsSystems.GetAllSystems (ref systems);
            for (var i = 0; i < systemsCount; i++) {
                var system = systems[i];
                var systemType = system.GetType ();
                foreach (var f in systemType.GetFields (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                    // skip statics.
                    if (f.IsStatic) {
                        continue;
                    }
                    // emitter.
                    if (f.FieldType == emitterType) {
                        f.SetValue (system, emitter);
                        continue;
                    }
                    // skip fields without [EcsUiNamed] attribute.
                    if (!Attribute.IsDefined (f, uiNamedType)) {
                        continue;
                    }
                    var name = ((EcsUguiNamedAttribute) Attribute.GetCustomAttribute (f, uiNamedType)).Name;
#if DEBUG
                    if (string.IsNullOrEmpty (name)) { throw new Exception ($"Cant Inject field \"{f.Name}\" at \"{systemType}\" due to [EcsUiNamed] \"Name\" parameter is invalid."); }
                    if (!(f.FieldType == goType || componentType.IsAssignableFrom (f.FieldType))) {
                        throw new Exception ($"Cant Inject field \"{f.Name}\" at \"{systemType}\" due to [EcsUiNamed] attribute can be applied only to GameObject or Component type.");
                    }
                    if (!skipNoExists && !emitter.GetNamedObject (name)) { throw new Exception ($"Cant Inject field \"{f.Name}\" at \"{systemType}\" due to there is no UI action with name \"{name}\"."); }
#endif
                    var go = emitter.GetNamedObject (name);
                    // GameObject.
                    if (f.FieldType == goType) {
                        f.SetValue (system, go);
                        continue;
                    }
                    // Component.
                    if (componentType.IsAssignableFrom (f.FieldType)) {
                        f.SetValue (system, go != null ? go.GetComponent (f.FieldType) : null);
                    }
                }
            }
            return ecsSystems;
        }

        static void InjectOneFrames (EcsSystems ecsSystems) {
            ecsSystems.DelHere<EcsUguiDragStartEvent> ();
            ecsSystems.DelHere<EcsUguiDragMoveEvent> ();
            ecsSystems.DelHere<EcsUguiDragEndEvent> ();
            ecsSystems.DelHere<EcsUguiDropEvent> ();
            ecsSystems.DelHere<EcsUguiClickEvent> ();
            ecsSystems.DelHere<EcsUguiDownEvent> ();
            ecsSystems.DelHere<EcsUguiUpEvent> ();
            ecsSystems.DelHere<EcsUguiEnterEvent> ();
            ecsSystems.DelHere<EcsUguiExitEvent> ();
            ecsSystems.DelHere<EcsUguiScrollViewEvent> ();
            ecsSystems.DelHere<EcsUguiSliderChangeEvent> ();
            ecsSystems.DelHere<EcsUguiTmpDropdownChangeEvent> ();
            ecsSystems.DelHere<EcsUguiTmpInputChangeEvent> ();
            ecsSystems.DelHere<EcsUguiTmpInputEndEvent> ();
        }
    }
}