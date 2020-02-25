using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class observer : MonoBehaviour
{


    public void NotVisible()
    {
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();

        // Set to not visible
        observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.None;
    }

    public void Visible()
    {
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();

        // Set to visible and the Occlusion material
        observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.Visible;
    }

    
}
