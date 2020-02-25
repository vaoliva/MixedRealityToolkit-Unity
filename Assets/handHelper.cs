using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.WindowsMixedReality.Input;
using System.Collections;
using System.Linq;
using UnityEngine;

public class handHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject go;
    void Start()
    {
        //InputSimulationService inputSimulationService = CoreServices.GetInputSystemDataProvider<InputSimulationService>();
        //inputSimulationService.UserInputEnabled = false;


        //var handedness = Handedness.Right;

        //StartCoroutine(ShowHand(handedness, inputSimulationService));
      
    }

    private void Update()
    {
        //MixedRealityPose pose;
        //var b = HandJointUtils.TryGetJointPose<WindowsMixedRealityArticulatedHand>(TrackedHandJoint.IndexTip, Handedness.Right, out pose);
        //if(b)Debug.Log(pose.Position.x);
    }

    public IEnumerator ShowHand(Handedness handedness, InputSimulationService inputSimulationService)
    {
        Vector3 pos = go.GetComponentsInChildren<PressableButton>().First().gameObject.transform.position;
        Vector3 pos1= go.GetComponentsInChildren<PressableButton>().First().gameObject.transform.position;
        pos1.z = -0.05f;
        //pos1.z += go.GetComponentsInChildren<PressableButton>().First().StartPushDistance * 2.0f;
        yield return ShowHand(handedness, inputSimulationService, ArticulatedHandPose.GestureId.Open,pos1);

        yield return new WaitForSeconds(2.0f);


        pos1.z += go.GetComponentsInChildren<PressableButton>().First().StartPushDistance * 2.0f;
        yield return ShowHand(handedness, inputSimulationService, ArticulatedHandPose.GestureId.Open,
            pos1);

        yield return new WaitForSeconds(2.0f);

        pos1.z 
            = pos.z
            + go.GetComponentsInChildren<PressableButton>().First().PressDistance + go.GetComponentsInChildren<PressableButton>().First().MaxPushDistance;

        pos1.z = 0.05f;

        yield return ShowHand(handedness, inputSimulationService, ArticulatedHandPose.GestureId.Open, pos1);
    }

    public IEnumerator ShowHand(Handedness handedness, InputSimulationService inputSimulationService, ArticulatedHandPose.GestureId handPose, Vector3 handLocation)
    {
        yield return null;

        SimulatedHandData handData = handedness == Handedness.Right ? inputSimulationService.HandDataRight : inputSimulationService.HandDataLeft;
        handData.Update(true, false, GenerateHandPose(handPose, handedness, handLocation, Quaternion.identity));

        // Wait one frame for the hand to actually appear
        yield return null;
    }

    private SimulatedHandData.HandJointDataGenerator GenerateHandPose(ArticulatedHandPose.GestureId gesture, Handedness handedness, Vector3 worldPosition, Quaternion rotation)
    {
        return (jointsOut) =>
        {
            ArticulatedHandPose gesturePose = SimulatedArticulatedHandPoses.GetGesturePose(gesture);
            Quaternion worldRotation = rotation * CameraCache.Main.transform.rotation;
            gesturePose.ComputeJointPoses(handedness, worldRotation, worldPosition, jointsOut);
        };
    }

    public void changeColor(GameObject go)
    {       go.GetComponent<Renderer>().material.color = Color.blue;
    }
}
