using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour {
    public float maxLength;

    private LineRenderer lineRenderer;

	void OnEnable() {
	    lineRenderer = GetComponent<LineRenderer>();
	}
	
	void Update() {

	    bool hit;
        List<Vector3> list = new List<Vector3>();
	    Vector3 pos = transform.position;
	    Vector3 dir = transform.forward.normalized;
        list.Add(Vector3.zero);
	    float dist = maxLength;

        while (dist > 0) {
            RaycastHit hitInfo;

            if (!Physics.Raycast(pos + dir * 0.01f, dir, out hitInfo, dist))
            {
	            list.Add(pos + dir*dist);
                break;
	        }

	        dist -= (pos - hitInfo.point).magnitude;
	        list.Add(transform.InverseTransformPoint(pos = hitInfo.point));

	        if (hitInfo.collider.tag != "Mirror")
                break;

	        dir -= 2*Vector3.Project(dir, hitInfo.normal);
	    }

        lineRenderer.positionCount = list.Count;
        lineRenderer.SetPositions(list.ToArray());
	}
}
