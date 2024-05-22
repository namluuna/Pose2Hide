using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField]
    CharacterPoseController characterPoseController1;
    [SerializeField]
    CharacterPoseController characterPoseController2;

    Vector3 position1;
    Vector3 position2;
    GameObject index1;
    GameObject index2;

    public GameController gameController;

    int phase;
    // Start is called before the first frame update
    void Start()
    {

        phase = 0;
        characterPoseController1 = gameController.Character[0].GetComponent<CharacterPoseController>();
        characterPoseController2 = gameController.Character[1].GetComponent<CharacterPoseController>();
        index1 = gameController.Image_standard[0];
        index2 = gameController.Image_standard[1];
        position1 = GameManager.Instance.Levels[0].Answers[0]._positon;
        position2 = GameManager.Instance.Levels[0].Answers[1]._positon;

    }

    // Update is called once per frame
    void Update()
    {
        GameController.timer = 60;
        switch (phase)
        {
            case 0:
                {

                    if (!GetComponent<Animation>().isPlaying)
                    {
                        GetComponent<Animation>().Play("1st");
                    }
                    if (index1.GetComponent<MeshRenderer>().material.name == characterPoseController1.gameObject.GetComponent<MeshRenderer>().material.name || index2.GetComponent<MeshRenderer>().material.name == characterPoseController2.gameObject.GetComponent<MeshRenderer>().material.name)
                    {
                        phase++;
                        GetComponent<Animation>().Stop();
                    }
                    break;
                }
            case 1:
                {
                    if (!GetComponent<Animation>().isPlaying)
                    {
                        GetComponent<Animation>().Play("2nd");
                    }
                    if (index1.GetComponent<MeshRenderer>().material.name == characterPoseController1.gameObject.GetComponent<MeshRenderer>().material.name)
                    {
                        if (Vector3.Distance(characterPoseController1.transform.position, position1) < 1f)
                        {
                            phase++;
                            GetComponent<Animation>().Stop();
                        }
                    }
                    else if (index2.GetComponent<MeshRenderer>().material.name == characterPoseController2.gameObject.GetComponent<MeshRenderer>().material.name)
                    {
                        if (Vector3.Distance(characterPoseController2.transform.position, position2) < 1f)
                        {
                            phase++;
                            GetComponent<Animation>().Stop();
                        }
                    }
                    else
                    {
                        phase--;
                        GetComponent<Animation>().Stop();
                    }

                    break;
                }
            case 2:
                {
                    if (!GetComponent<Animation>().isPlaying)
                    {
                        GetComponent<Animation>().Play("3rd");
                    }

                    if (Vector3.Distance(characterPoseController1.transform.position, position1) > 1f && Vector3.Distance(characterPoseController2.transform.position, position2) > 1f)
                    {
                        phase--;
                        GetComponent<Animation>().Stop();
                    }
                    if (index1.GetComponent<MeshRenderer>().material.name
                        != characterPoseController1.gameObject.GetComponent<MeshRenderer>().material.name
                        &&
                        index2.GetComponent<MeshRenderer>().material.name
                        != characterPoseController2.gameObject.GetComponent<MeshRenderer>().material.name
                        )
                    {
                        phase--;
                        GetComponent<Animation>().Stop();
                    }


                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began)
                        {
                            phase++;
                            GetComponent<Animation>().Stop();
                        }
                    }
                    break;
                }
            case 3:
                {
                    if (!GetComponent<Animation>().isPlaying)
                    {
                        GetComponent<Animation>().Play("4th");
                    }
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began)
                        {
                            gameObject.SetActive(false);
                            GetComponent<Animation>().Stop();
                        }
                    }
                    PlayerPrefs.SetInt("isTutorialGameplay", 1);
                    break;
                }
        }
    }
}
