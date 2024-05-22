using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterHIWController : MonoBehaviour
{
    public List<SkeletonDataAsset> list_poses = new List<SkeletonDataAsset>();
    private List<SkeletonDataAsset> list_all_poses = new List<SkeletonDataAsset>();
    public List<string> skin_list = new List<string>();
    public int _pose;
    public SkeletonAnimation _sprite;
    public int index;

    public int first_index;
    //Local variable
    private Vector3 offset;
    private Vector3 standard_position;

    private bool isDraggable = false;
    private float lastClickTime = 0f;
    private float clickCooldown = 0.3f;

    [SerializeField]
    private Sprite blank;
    private void Start()
    {
        GameUIController.Instance.StopAllSoudn();
        AudioManager.instance.playMusic();
        if (!gameObject.name.Contains("Blank"))
        {
            LoadListAllPose();
            //ResetCollider();
            LoadListPose();
        }
        else
        {
            _sprite.GetComponent<MeshRenderer>().enabled = false;  
        }
    }

    private void Update()
    {
        if (HIWGameController.isEndGame&& gameObject.name.Contains("Blank"))
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && !HIWGameController.isEndGame)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject && Time.time - lastClickTime > clickCooldown)
                {
                    first_index = index;
                    isDraggable = true; // Chỉ kích hoạt kéo thả cho gameobject này
                    standard_position = transform.position;
                    offset = transform.position - touchPosition;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDraggable)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                transform.position = touchPosition + offset;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (isDraggable)
                {
                    isDraggable = false;

                    if (Vector3.Distance(standard_position, transform.position) < 0.1f)
                    {
                        if (Time.time - lastClickTime > clickCooldown)
                        {
                            lastClickTime = Time.time;

                            _pose++;

                            if (_pose == list_poses.Count)
                            {
                                _pose = 0;
                            }
                            AnimateGameObject();
                            AudioManager.instance.playSFX("ChangePose");
                        }
                    }
                    else if (Vector3.Distance(standard_position, transform.position) > 0.1f)
                    {
                        if (target_collision != null)
                        {
                            int my_index = first_index;
                            index = target_collision.gameObject.GetComponent<CharacterHIWController>().index;
                            target_collision.gameObject.GetComponent<CharacterHIWController>().index = my_index;
                            target_collision.gameObject.GetComponent<CharacterHIWController>().ResetPosition();
                        }
                        ResetPosition();
                    }
                }
                target_collision = null;
                ResetPosition();
            }
        }
    }


    void AnimateGameObject()
    {
        StartCoroutine(Tweening());
    }

    void ResetCollider()
    {
        //Destroy(GetComponent<PolygonCollider2D>());
        //gameObject.AddComponent<PolygonCollider2D>();   
    }

    IEnumerator Tweening()
    {

        yield return new WaitForSeconds(0.2f);

        _sprite.skeletonDataAsset = list_poses[_pose];
        _sprite.Initialize(true);
        ResetCollider();
        ResetPosition();
    }

    void LoadListAllPose()
    {
        foreach (var i in GameManager.Instance.Characters)
        {
            if (gameObject.name == i.CharacterId)
            {
                list_all_poses = i.ListOfAnime;

                foreach (var o in i.ListSkinData)
                {
                    skin_list.Add(o.Name);
                }
            }
        }
    }

    void LoadListPose()
    {
        if (skin_list.Count > 1)
        {
            _sprite.initialSkinName = skin_list[0];
        }
        HIWLevelData data = GameManager.Instance.HIWLevels[GameManager.Instance.HIW_current_level];
        foreach(var i in data.Characters)
        {
            if(i._character == gameObject.name)
            {
                foreach(var j in i._pose)
                {
                    list_poses.Add(list_all_poses[j]);
                }
            }
        }
        _sprite.skeletonDataAsset = list_poses[0];
        _sprite.Initialize(true);
        ResetCollider();
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(HIWGameController.positions[index].x, -30, 0);
    }

    Collider2D target_collision;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        target_collision = collision;
    }

}
