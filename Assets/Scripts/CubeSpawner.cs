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

public class CubeSpawner : MonoBehaviour
{
    public float LUCKY_CHANGE = 0.15f;
    public int MAX_CUBES = 6;

    public Cubie cubePrefab;
    public int SpawnCount { get; protected set; }

    List<Cubie> spawnedCubes = new List<Cubie>();

    public float cubeSpawnDelay = 5f;
    public float cubeSpawnTimeRemaining = 0;

    public bool autoSpawn = false;

    public CubeSpawner nextSpawner;

    public int baseSpawnLevel = 0;
    public int maxLevel = 3;

    public bool debugMode = false;

    public GameObject winEffect;
    public UI_LuckyLabel luckyLabel;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (debugMode && Input.GetKeyDown(KeyCode.S))
        {
            SpawnCube(maxLevel-1);
        }

        if (autoSpawn)
        {
            cubeSpawnTimeRemaining -= Time.deltaTime;

            if (cubeSpawnTimeRemaining <= 0 && spawnedCubes.Count < MAX_CUBES)
            {
                // there is a small chance that a better cube is spawned if we already have some leveled up cubes
                if (Simulation.Instance.highestCubeLevel > 1 && Random.value <= LUCKY_CHANGE)
                {
                    Debug.Log("Lucky!!");
 
                    SpawnBetterCube();
                }
                else
                {
                    SpawnCube(baseSpawnLevel);                    
                }
                cubeSpawnTimeRemaining = cubeSpawnDelay;
            }
        }
	}

    void SpawnBetterCube()
    {
        int selectValue = Mathf.RoundToInt(Mathf.Lerp(1, Simulation.Instance.highestCubeLevel - 1, Random.value));
        Debug.Log("SelectValue: " + selectValue);
        CubeSpawner spawnerThatCanSpawn = null;
        CubeSpawner testedSpawner = this;
        while (spawnerThatCanSpawn == null && testedSpawner.nextSpawner != null)
        {
            if (selectValue <= testedSpawner.maxLevel)
            {
                spawnerThatCanSpawn = testedSpawner;
            } else
            {
                testedSpawner = testedSpawner.nextSpawner;
            }
        }

        if (spawnerThatCanSpawn == null)
        {
            Debug.LogWarning("No spawner for cube level: " + selectValue + " could be found. Spawning a basic block instead.");
            SpawnCube();
        } else
        {
            luckyLabel.gameObject.SetActive(true);
            luckyLabel.SetLevel(selectValue);
            spawnerThatCanSpawn.SpawnCube(selectValue);
        }
    }

    public void SpawnCube(int level = 0)
    {
        Cubie cubie = Instantiate(cubePrefab, transform);
        cubie.cubeLevel = level;
        cubie.owner = this;
        spawnedCubes.Add(cubie);
        Simulation.Instance.GoldPerSecond += cubie.GoldPerSecond;
        Simulation.Instance.highestCubeLevel = Mathf.Max(Simulation.Instance.highestCubeLevel, level);
        SpawnCount += 1;

        if (cubePrefab.isFinalCube)
        {
            winEffect.SetActive(true);
        }

    }

    public void RemoveCube(Cubie cube)
    {
        spawnedCubes.Remove(cube);
        Simulation.Instance.GoldPerSecond -= cube.GoldPerSecond;
    }

    public int SpawnedCubeCount
    {
        get
        {
            return spawnedCubes.Count;
        }
    }

    public Cubie GetCube(int index)
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
