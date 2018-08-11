using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public float BPM = 120;

    float nextNoteTimer = 0;
    public int noteIndex = 0;

	// Use this for initialization
	void Start () {
        nextNoteTimer = 60.0f / BPM;
	}
	
	// Update is called once per frame
	void Update () {
/*		if (CubeSpawner.SpawnedCubeCount > 0)
        {
            nextNoteTimer -= Time.deltaTime;

            if (nextNoteTimer <= 0)
            {
                Cubie c = CubeSpawner.GetCube(noteIndex);
                //c.PlayNote(0.5f * 60.0f / BPM);
                noteIndex -= 1;
                if (noteIndex < 0)
                {
                    noteIndex = CubeSpawner.SpawnedCubeCount - 1;
                }
                nextNoteTimer = 60.0f / BPM;
            }
        }*/
	}
}
