// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.MixedReality.Toolkit.Boundary;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Microsoft.MixedReality.Toolkit.Input
{
    /// <summary>
    /// Script used to raise Unity Events in response to pointer events.
    /// </summary>
    [AddComponentMenu("Scripts/MRTK/SDK/PointerHandler")]
    public class PointerHandler : BaseInputHandler, IMixedRealityPointerHandler
    {
        [SerializeField]
        [Tooltip("Whether input events should be marked as used after handling so other handlers in the same game object ignore them")]
        private bool MarkEventsAsUsed = false;

        /// <summary>
        /// Unity event raised on pointer down.
        /// </summary>
        public PointerUnityEvent OnPointerDown = new PointerUnityEvent();

        /// <summary>
        /// Unity event raised on pointer up.
        /// </summary>
        public PointerUnityEvent OnPointerUp = new PointerUnityEvent();

        /// <summary>
        /// Unity event raised on pointer clicked.
        /// </summary>
        public PointerUnityEvent OnPointerClicked = new PointerUnityEvent();

        /// <summary>
        /// Unity event raised every frame the pointer is down.
        /// </summary>
        public PointerUnityEvent OnPointerDragged = new PointerUnityEvent();

        #region InputSystemGlobalHandlerListener Implementation

        protected override void RegisterHandlers()
        {
            CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);
        }

        protected override void UnregisterHandlers()
        {
            CoreServices.InputSystem?.UnregisterHandler<IMixedRealityPointerHandler>(this);
        }

        #endregion InputSystemGlobalHandlerListener Implementation

        #region IMixedRealityPointerHandler

        void IMixedRealityPointerHandler.OnPointerDown(MixedRealityPointerEventData eventData)
        {
                                  ExecuteEvents.ExecuteHierarchy<IMixedRealityPointerHandler>(gameObject.transform.parent.gameObject, eventData, OnPointerClickedPropagated);
            gameObject.GetComponent<Renderer>().material.color = Color.black;
            if (!eventData.used)
            {
                OnPointerDown.Invoke(eventData);
                if (MarkEventsAsUsed)
                {
                    eventData.Use();
                }
            }
        }

        void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
        {
            if (!eventData.used)
            {
                OnPointerUp.Invoke(eventData);
                if (MarkEventsAsUsed)
                {
                    eventData.Use();
                }
            }
        }

        private static readonly ExecuteEvents.EventFunction<IMixedRealityPointerHandler> OnPointerClickedPropagated =
            delegate (IMixedRealityPointerHandler handler, BaseEventData eventData)
            {
                var pointerEventData = ExecuteEvents.ValidateEventData<MixedRealityPointerEventData>(eventData);
                handler.OnPointerDown(pointerEventData);
            };

        void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData)
        {


         
        
            if (!eventData.used)
            {
                OnPointerClicked.Invoke(eventData);
                if (MarkEventsAsUsed)
                {
                    eventData.Use();
                }
            }
        }

        void IMixedRealityPointerHandler.OnPointerDragged(MixedRealityPointerEventData eventData)
        {
            if (!eventData.used)
            {
                OnPointerDragged.Invoke(eventData);
                if (MarkEventsAsUsed)
                {
                    eventData.Use();
                }
            }
        }

        #endregion IMixedRealityPointerHandler
    }
}
