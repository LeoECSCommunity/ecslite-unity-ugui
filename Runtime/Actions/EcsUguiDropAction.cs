// ----------------------------------------------------------------------------
// The MIT License
// Ugui bindings https://github.com/Leopotam/ecslite-unity-ugui
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine.EventSystems;

namespace Leopotam.EcsLite.Unity.Ugui {
    public sealed class EcsUguiDropAction : EcsUguiActionBase<EcsUguiDropEvent>, IDropHandler {
        public void OnDrop (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref CreateEvent ();
                msg.WidgetName = GetWidgetName ();
                msg.Sender = gameObject;
                msg.Button = eventData.button;
            }
        }
    }
}