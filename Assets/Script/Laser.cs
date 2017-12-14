using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour {
    public float maxLength;
    public Laser laserPrefab;
    private Laser next;

    [NonSerialized] public bool hit;
    [NonSerialized] public RaycastHit hitInfo;
    private LineRenderer lineRenderer;
    public Vector3 EndPos { get { return hit ? hitInfo.point : transform.position + transform.forward * maxLength; } }
    public float MoveDist { get { return hit ? (hitInfo.point - transform.position).magnitude : maxLength; } }

	void OnEnable() {
	    lineRenderer = GetComponent<LineRenderer>();
	}
	
	void Update() {
	    hit = Physics.Raycast(transform.position + transform.forward*0.01f, transform.forward, out hitInfo, maxLength);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, EndPos);

	    if (hit) {
	        if (hitInfo.transform.tag == "Mirror") {
                Quaternion childRotation = Quaternion.LookRotation(Quaternion.FromToRotation(transform.forward, hitInfo.normal) * -hitInfo.normal);

                SetChild(hitInfo.point, childRotation);
	        } else {
	            if (next) Destroy(next);
	        }
	    }
	}

    private void SetChild(Vector3 position, Quaternion rotation)
    {
        if (next == null)
        {
            next = Instantiate(laserPrefab);
            next.transform.SetParent(transform);
        }

        next.transform.position = position;
        next.transform.rotation = rotation;
    }
}
