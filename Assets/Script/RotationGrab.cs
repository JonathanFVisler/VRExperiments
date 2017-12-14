using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.GrabAttachMechanics;

public class RotationGrab : VRTK_TrackObjectGrabAttach {
    public override void ProcessUpdate() {
        var rotateForce = trackPoint.position - initialAttachPoint.position;
        grabbedObject.transform.Rotate(Quaternion.FromToRotation(initialAttachPoint.position - grabbedObject.transform.position, trackPoint.position - grabbedObject.transform.position).eulerAngles, Space.World);
        //grabbedObjectRigidBody.AddForceAtPosition(rotateForce, initialAttachPoint.position, ForceMode.VelocityChange);
    }

    protected override void SetTrackPointOrientation(ref Transform trackPoint, Transform currentGrabbedObject, Transform controllerPoint) {
        trackPoint.position = controllerPoint.position;
        trackPoint.rotation = controllerPoint.rotation;
    }
}
