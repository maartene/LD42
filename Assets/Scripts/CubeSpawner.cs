// Ludum Dare 42 entry
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

public class CubeSpawner : MonoBehaviour {
    const int MAX_CUBES = 16;

    public Cubie cubePrefab;

    static List<Cubie> spawnedCubes = new List<Cubie>();

    public float cubeSpawnDelay = 5f;
    float cubeSpawnTimeRemaining = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cubeSpawnTimeRemaining -= Time.deltaTime;

	    if (cubeSpawnTimeRemaining <= 0 && spawnedCubes.Count < MAX_CUBES)
        {
            SpawnCube();
            cubeSpawnTimeRemaining = cubeSpawnDelay;
        }	
	}

    void SpawnCube()
    {
        Cubie cubie = Instantiate(cubePrefab, transform);
        spawnedCubes.Add(cubie);
    }

    public static int SpawnedCubeCount
    {
        get
        {
            return spawnedCubes.Count;
        }
    }

    public static Cubie GetCube(int index)
    {
        if (index < 0)
        {
            Debug.LogError("Requested cube index should be greater than or equal to 0. Returning null.");
            return null;
        }

        if (index >= spawnedCubes.Count)
        {
            Debug.LogError("Requested cube is out of bounds. Returning null.");
            return null;
        }

        if (spawnedCubes[index] == null)
        {
            Debug.Log("Cube with index: " + index + " no longer exists. Removing it from list and returning the next cube.");
            spawnedCubes.RemoveAt(index);
            return GetCube(index);
        }

        return spawnedCubes[index];
    }
}
