// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#if !WINDOWS_UWP
// When the .NET scripting backend is enabled and C# projects are built
// The assembly that this file is part of is still built for the player,
// even though the assembly itself is marked as a test assembly (this is not
// expected because test assemblies should not be included in player builds).
// Because the .NET backend is deprecated in 2018 and removed in 2019 and this
// issue will likely persist for 2018, this issue is worked around by wrapping all
// play mode tests in this check.

using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using NUnit.Framework;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Microsoft.MixedReality.Toolkit.Tests
{
    public class ScrollViewTests : BasePlayModeTests 
    {

        private static string PrefabDirectoryPath = "Assets";

        private GameObject InstantiateDefaultPressableButton(string prefabFilename)
        {
            var path = Path.Combine(PrefabDirectoryPath, prefabFilename);
            Object pressableButtonPrefab = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
            GameObject testButton = Object.Instantiate(pressableButtonPrefab) as GameObject;

            return testButton;
        }
        #region Tests
     

        /// <summary>
        /// This tests the release behavior of a button
        /// </summary>
        [UnityTest]
        public IEnumerator ReleaseButton()
        {
            BaseEventSystem.enableDanglingHandlerDiagnostics = false;

            GameObject testButton = InstantiateDefaultPressableButton("scroll.prefab");
            testButton.transform.position = Vector3.forward;
            TestUtilities.PlayspaceToOriginLookingForward();

            //yield return new WaitForSeconds(3000.0f);

            PressableButton buttonComponent = testButton.GetComponentInChildren<PressableButton>();
            Assert.IsNotNull(buttonComponent);

            bool buttonPressed = false;
            buttonComponent.ButtonPressed.AddListener(() =>
            {
                buttonPressed = true;
            });


            float offset = 0.001f;
            Vector3 buttonPos = buttonComponent.transform.position;

            Vector3 startHand = buttonPos + new Vector3(0, 0, buttonComponent.StartPushDistance - offset);
            Vector3 inButtonOnPress = buttonPos + new Vector3(0, 0, buttonComponent.PressDistance + offset); // past press plane of mrtk pressablebutton prefab          
            Vector3 inButtonOnRelease = buttonPos + new Vector3(0, 0, 0.5f); // release plane of mrtk pressablebutton prefab

            TestHand hand = new TestHand(Handedness.Right);

            // test scenarios in normal and low framerate
            int[] stepVariations = { 30, 2 };
            for (int i = 0; i < stepVariations.Length; ++i)
            {
                int numSteps = stepVariations[i];

                // test release
                yield return hand.Show(startHand);
                yield return hand.MoveTo(inButtonOnPress, numSteps);
                //yield return new WaitForSeconds(1000.0f);
                //yield return hand.MoveTo(inButtonOnRelease, numSteps);
                //yield return null;
               // yield return null;
                yield return hand.Hide();

                Assert.IsTrue(buttonPressed, $"A{i} - Button did not get pressed when hand moved to press it.");

                buttonPressed = false;
            }

            //buttonComponent.ButtonPressed.RemoveAllListeners();

            // destroy imediate because scroll object collection is still listenibng to input stuff
            //var scrollViewComponent = testButton.GetComponentInChildren<Microsoft.MixedReality.Toolkit.Experimental.UI.ScrollingObjectCollection>();
            //Component.DestroyImmediate(buttonComponent);
            //Component.DestroyImmediate(scrollViewComponent);

            //GameObject.DestroyImmediate(testButton);
            //yield return null;
            //yield return null;
            //UnityEngine.Assertions.Assert.IsNull(testButton);


            //yield return null;
            //BaseEventSystem.enableDanglingHandlerDiagnostics = true;
        }

        /// <summary>
        /// This tests the release behavior of a button
        /// </summary>
        [UnityTest]
        public IEnumerator CancelTouchInputSpeechPointer()
        {
           //assembly scroll view with 2 elements
           //add handler to elements
            yield return null;
            // 
        }
            #endregion
        }
}


#endif
