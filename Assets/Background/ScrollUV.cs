using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour {

    public float speed = 1;

	void Update () {

		MeshRenderer mr = GetComponent<MeshRenderer>();

		Material mat = mr.material;

		Vector2 offset = mat.mainTextureOffset;

		offset.x += Time.deltaTime / speed;

		mat.mainTextureOffset = offset;

	}

}
