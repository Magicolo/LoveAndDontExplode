using UnityEngine;
using System.Collections;


public enum GizmoType {
	sphere,
	wireSphere,
	cube,
	wireCube
}

public class GizmoGiver : MonoBehaviour {
	public Color gizmoColour = Color.black;
	public GizmoType gizmoType = GizmoType.sphere;

	public float radius = 1f;
	public Vector3 boxSize = Vector3.one;
	public Vector3 offset = Vector3.zero;

	public void OnDrawGizmos () {

		Gizmos.color = gizmoColour;

		switch (gizmoType) {
		case GizmoType.sphere:
			Gizmos.DrawSphere (transform.position + offset, radius);
			break;
		case GizmoType.wireSphere:
			Gizmos.DrawWireSphere (transform.position + offset, radius);
			break;
		case GizmoType.cube:
			Gizmos.DrawCube (transform.position + offset, boxSize);
			break;
		case GizmoType.wireCube:
			Gizmos.DrawWireCube (transform.position + offset, boxSize);
			break;

		}
	}


}
