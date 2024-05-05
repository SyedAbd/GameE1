//SlapChickenGames
//2021
//Scene controller

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace scgFullBodyController
{
    public class sceneController : MonoBehaviour
    {
        public GameObject[] cameras;
        public GameObject movCam;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                movCam.GetComponent<Animator>().enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var cam = cameras[0];
                cam.SetActive(true);

                foreach (GameObject go in cameras)
                {
                    if (go.name != cam.name)
                    go.SetActive(false);
                }

                movCam.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                var cam = cameras[1];
                cam.SetActive(true);

                foreach (GameObject go in cameras)
                {
                    if (go.name != cam.name)
                        go.SetActive(false);
                }

                movCam.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                var cam = cameras[2];
                cam.SetActive(true);

                foreach (GameObject go in cameras)
                {
                    if (go.name != cam.name)
                        go.SetActive(false);
                }

                movCam.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                var cam = cameras[3];
                cam.SetActive(true);

                foreach (GameObject go in cameras)
                {
                    if (go.name != cam.name)
                        go.SetActive(false);
                }

                movCam.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                var cam = cameras[4];
                cam.SetActive(true);

                foreach (GameObject go in cameras)
                {
                    if (go.name != cam.name)
                        go.SetActive(false);
                }

                movCam.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                var cam = cameras[5];
                cam.SetActive(true);

                foreach (GameObject go in cameras)
                {
                    if (go.name != cam.name)
                        go.SetActive(false);
                }

                movCam.SetActive(false);
            }
        }
    }
}
