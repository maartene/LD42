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
using UnityEngine.EventSystems;

public class Cubie : MonoBehaviour
{
    public static readonly float[] PITCHES = { 264, 277.2f, 297, 311.1f, 330, 352, 370.0f, 396, 415.3f, 440f, 466.2f, 495, 528 };
    public static readonly float[] CHORDS = { 0, 4, 7, 10 };

    public int cubeLevel = 0;

    Rigidbody rigidBody;
    Collider m_collider;
    Renderer m_renderer;

    Material material;

    public CubeSpawner owner;
    public bool isFinalCube = false;

    float t = 0;

    public float coinzDelay = 2.5f;
    public float coinzTimer = 0;

    public float GoldPerSecond {
        get {
            return 1.5f * Mathf.Pow(2.75123f, cubeLevel);
        }
    }

    // Use this for initialization
    void Start () {

        coinzTimer = coinzDelay;

        rigidBody = GetComponent<Rigidbody>();
        m_collider = GetComponent<Collider>();
        m_renderer = GetComponent<Renderer>();

        material = new Material(m_renderer.material);

        if (isFinalCube)
        {
            Debug.Log("Won!!!");
            StartCoroutine(Spectrum());
        }

        SetColor();

    }

    public void IncreaseCubeLevel()
    {
        Simulation.Instance.GoldPerSecond -= GoldPerSecond;
        cubeLevel += 1;
        Simulation.Instance.GoldPerSecond += GoldPerSecond;
        SetColor();
    }

    void SetColor()
    {
        if (isFinalCube)
        {
            return;

        }

        material.mainTextureScale = Vector2.one / 4.0f;
        //int column = 3 - cubeLevel % 4;
        //float uv_x = 1.0f - (material.mainTextureScale.x * (float)column) - material.mainTextureScale.x;

        int column = cubeLevel % 4;
        float uv_x = material.mainTextureScale.x * (float)column;
        material.mainTextureOffset = new Vector2(uv_x, cubeLevel / 4 * material.mainTextureScale.y);
//        Debug.Log("column: "+column + " uv_x:"+ uv_x + " offset: " + material.mainTextureOffset);
        Color color = new HSBColor((float)((cubeLevel * 3) % 16) / 16.0f, 1, 1).ToColor();

        material.color = color;


              
        m_renderer.material = material;      
    }

    IEnumerator Spectrum()
    {
        while (true)
        {
            t = Random.Range(0.0f, 1.0f);
            material.color = new HSBColor(t, 1, 1).ToColor();
            m_renderer.material = material;
            yield return new WaitForSeconds(0.1f);
        }
    }

	// Update is called once per frame
	void Update () {
        coinzTimer -= Time.deltaTime;
        if (coinzTimer < 0) {

            Simulation.Instance.Gold += GoldPerSecond * coinzDelay;
            // instantiate some display effect.

            coinzTimer = coinzDelay;
        }
	}

    public void MakeRigidBodyDraggable(bool state)
    {
        rigidBody.isKinematic = state;
        m_collider.isTrigger = state;
    }

    public void OnDragStart(Vector3 mousePosition)
    {
        if (isFinalCube)
        {
            return;
        }

        Debug.Log("Begin drag");
        MakeRigidBodyDraggable(true);
    }

    public void OnDrag(Vector3 mousePosition)
    {
        if (isFinalCube)
        {
            return;
        }

        Plane plane = new Plane(Vector3.back, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        float distance;
        plane.Raycast(ray, out distance);

        Vector3 targetPosition = Camera.main.transform.position + ray.direction * distance;

        targetPosition = new Vector3(owner.transform.position.x, targetPosition.y, owner.transform.position.z);

        transform.position = targetPosition;
    }

    public void OnDragEnd(Vector3 mousePosition)
    {
        if (isFinalCube)
        {
            return;
        }

        // first we need to determine whether we are above another cube
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        // because we are always also hitting ourselves, we need to find all colliding cubes
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            Cubie c = hit.collider.GetComponent<Cubie>();
            if (c == this)
            {
                // we hit ourselves, don't do anything.
            } else if (c != null)
            {
                // we hit another cubie
                // is this other cubie of the same "level"?
                if (c.cubeLevel == this.cubeLevel)
                {
                    // we are on the level
                    // if we are already on the maximum level, we will spawn a cube in the next spawner

                    if (cubeLevel >= owner.maxLevel)
                    {
                        owner.nextSpawner.SpawnCube(cubeLevel + 1);
                        owner.RemoveCube(c);
                        Destroy(c.gameObject);
                    } else
                    {
                        // we will update the other cubie
                        c.IncreaseCubeLevel();
                        c.MakeRigidBodyDraggable(false);
                    }


                    // and we will destroy ourselves
                    owner.RemoveCube(this);
                    Destroy(gameObject);

                    // and register a succesful swipe
                    Simulation.Instance.Swipes += 1;

                } else
                {
                    // go back to the top
                    transform.position = transform.parent.position;
                    MakeRigidBodyDraggable(false);
                }
            } else
            {
                // we hit nothing
                MakeRigidBodyDraggable(false);
            }

        }

        
    }
}
