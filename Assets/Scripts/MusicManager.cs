﻿// Ludum Dare 42 entry
// Copyright (C) 2018 Maarten Engels, thedreamweb.eu

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

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
