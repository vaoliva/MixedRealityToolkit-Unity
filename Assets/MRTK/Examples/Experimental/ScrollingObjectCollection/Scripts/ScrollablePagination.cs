﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.MixedReality.Toolkit.Experimental.UI;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Examples
{
    /// <summary>
    /// Example script of how to navigate a <see cref="Microsoft.MixedReality.Toolkit.Experimental.UI.ScrollingObjectCollection"/> by pagination.
    /// Allows the call to scroll pagination methods from the inspector.
    /// </summary>
    public class ScrollablePagination : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The ScrollingObjectCollection to navigate")]
        private ScrollingObjectCollection scrollView;

        public ScrollingObjectCollection Scrollview
        {
            get
            {
                if (scrollView == null)
                {
                    scrollView = GetComponent<ScrollingObjectCollection>();
                }
                return scrollView;
            }
            set
            {
                scrollView = value;
            }
        }

        public void ScrollByTier(int amount)
        {
            Debug.Assert(Scrollview != null, "Scroll view needs to be defined before using pagination.");
            scrollView.MoveByTiers(amount);
        }
    }
}