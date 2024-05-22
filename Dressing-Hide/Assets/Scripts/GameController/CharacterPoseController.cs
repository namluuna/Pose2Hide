using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPoseController : MonoBehaviour
{
    public List<SkeletonDataAsset> list_poses = new List<SkeletonDataAsset>();
    private List<SkeletonDataAsset> list_all_poses = new List<SkeletonDataAsset>();
    public int _pose;
    public SkeletonAnimation _sprite;
    public List<string> skin_list = new List<string>();
    //Local variable
    private Vector3 offset;
    private Vector3 standard_position;
    private void Start()
    {
        LoadListPose();
        SetPoses();
        ResetCollider();
    }
    private bool isDraggable = false;
    private float lastClickTime = 0f;
    private float clickCooldown = 0f;

    private void Update()
    {
       
        if (Input.touchCount > 0)
        {
            
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && GameController.gamePlayState == GamePlayState.PROCESS)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(touchPosition, Vector3.forward, out hit, Mathf.Infinity))
                {
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        GetComponent<MeshRenderer>().sortingOrder = 999;
                        standard_position = transform.position;
                        isDraggable = true; // Chỉ kích hoạt kéo thả cho gameobject này
                        offset = transform.position - hit.point;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDraggable && GameController.gamePlayState != GamePlayState.LOSE)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 newPosition = touchPosition + offset;

                // Lấy giới hạn của màn hình với padding
                float padding = 0.1f; // Điều chỉnh padding theo mong muốn
                Vector3 screenMin = Camera.main.ViewportToWorldPoint(new Vector3(padding, 0, 0));
                Vector3 screenMax = Camera.main.ViewportToWorldPoint(new Vector3(1 - padding, 0.7f, 0));
                 
                // Giới hạn vị trí mới nằm trong màn hình với padding
                float clampedX = Mathf.Clamp(newPosition.x, screenMin.x, screenMax.x);
                float clampedY = Mathf.Clamp(newPosition.y, screenMin.y, screenMax.y);

                transform.position = new Vector3(clampedX, clampedY, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                GetComponent<MeshRenderer>().sortingOrder = 4;
                if (isDraggable)
                {
                    isDraggable = false;

                    if (GameController.gamePlayState == GamePlayState.PROCESS && Vector3.Distance(standard_position, transform.position) < 0.1f)
                    {
                        _pose++;

                        if (_pose == list_poses.Count)
                        {
                            _pose = 0;
                        }
                        AnimateGameObject();

                        AudioManager.instance.playSFX("ChangePose");
                    }
                }
                
                if (GameController.gamePlayState == GamePlayState.WIN && !GameController.end_game)
                {
                    GameUIController.Instance.Victory();
                }
               
            }
            
        }
    }
    void ResetCollider()
    {
        Destroy(GetComponent<MeshCollider>());
        gameObject.AddComponent<MeshCollider>();
    }

    void AnimateGameObject()
    {
        //_sprite.skeletonDataAsset = list_poses[_pose];
        //_sprite.Initialize(true);
        //ResetCollider();
        StartCoroutine(Tweening());
    }

    IEnumerator Tweening()
    {
        yield return new WaitForSeconds(0.2f);

        _sprite.skeletonDataAsset = list_poses[_pose];
        RandomSkin();
        _sprite.Initialize(true);
        ResetCollider();
    }

    void RandomSkin()
    {
        LevelData data = GameManager.Instance.Levels[GameManager.Instance.current_level];
        if (skin_list.Count > 1)
        {
            int randomSkin = data.Skin[Random.RandomRange(0, data.Skin.Count)];
            _sprite.initialSkinName = skin_list[randomSkin];
        }
    }

    void LoadListPose()
    {
        foreach (var i in GameManager.Instance.Characters) 
        {
            if (gameObject.name == i.CharacterId) 
            {
                list_all_poses = i.ListOfAnime;
                
                foreach(var o in i.ListSkinData)
                {
                    skin_list.Add(o.Name);
                }
            } 
        }
    }

    void SetPoses()
    {
        LevelData data = GameManager.Instance.Levels[GameManager.Instance.current_level];

        //RandomSkin();

        foreach (var i in data.Answers)
        {
            if (i._character == gameObject.name)
            { 
                if(i.ListOfPose.Count > 0)
                {

                    foreach (var answer1 in data.Answers)
                    {
                        if (answer1._character == gameObject.name)
                        {
                            foreach (var a in answer1.ListOfPose)
                            {
                                list_poses.Add(list_all_poses[a]);
                            }
                            break;
                        }
                    }
                    _sprite.skeletonDataAsset = list_poses[0];
                    _pose = 0;
                    
                    _sprite.Initialize(true);
                }
                else
                {
                    foreach (var answer2 in data.Answers)
                    {
                        if (answer2._character == gameObject.name)
                        {
                            if (list_all_poses[answer2._pose] != list_all_poses[0])
                            {
                                list_poses.Add(list_all_poses[answer2._pose]);
                            }
                            break;
                        }
                    }

                    foreach (var pose in list_all_poses)
                    {

                        if (!list_poses.Contains(pose) && pose != list_all_poses[0])
                        {
                            list_poses.Add(pose);
                        }
                        if (4 - list_poses.Count == 1)
                        {
                            break;
                        }
                    }

                    int n = list_poses.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = Random.Range(0, n + 1);
                        SkeletonDataAsset value = list_poses[k];
                        list_poses[k] = list_poses[n];
                        list_poses[n] = value;
                    }
                    list_poses.Add(list_all_poses[0]);
                    _sprite.skeletonDataAsset = list_poses[list_poses.Count - 1];
                    _pose = list_poses.Count - 1;
                    _sprite.Initialize(true);

                }
            }
        }
    }
    

}
