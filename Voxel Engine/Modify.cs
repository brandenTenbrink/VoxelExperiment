using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Modify : MonoBehaviour {

	int activeBlock = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			activeBlock = 1;
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			activeBlock = 2;
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			activeBlock = 3;
		} 

		if (Input.GetMouseButtonDown (1)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.distance < 10) {
					EditTerrain.DestroyBlock (hit, new BlockAir ());
				}
			}
		} else if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.distance < 5) {
					switch (activeBlock) {
					case 1: 
						EditTerrain.SetBlock (hit, new Block (), true);
						break;
					
					case 2:
						EditTerrain.SetBlock (hit, new BlockGrass (), true);
						break;
					
					case 3:
						EditTerrain.SetBlock (hit, new BlockWood (), true);
						break;

					default :
						break;
					}

				}
			}
		}
	}

	void OnApplicationQuit () {
		var chunksToDelete = new List<WorldPos> ();
		var world = GameObject.FindObjectOfType<World> ();

		foreach (var chunk in world.chunks) {
			chunksToDelete.Add (chunk.Key);
		}

		foreach (var chunk in chunksToDelete) {
			world.DestroyChunk(chunk.x,chunk.y,chunk.z);
		}
	}
}
