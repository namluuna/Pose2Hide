using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTestChange : MonoBehaviour
{
    public List<SkeletonDataAsset> list_poses = new List<SkeletonDataAsset>();
    private List<SkeletonDataAsset> list_all_poses = new List<SkeletonDataAsset>();
    public int _pose;
    public SkeletonAnimation _sprite;

    //Local variable
    private Vector3 offset;
    private Vector3 standard_position;
    private void Start()
    {
      ResetCollider();
        _pose = 0;
    }
    private bool isDraggable = false;
    private float lastClickTime = 0f;
    private float clickCooldown = 0.2f;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            AnimateGameObject();
        }
    }

    void ResetCollider()
    {
        Destroy(GetComponent<MeshCollider>());
        gameObject.AddComponent<MeshCollider>();

    }

    void AnimateGameObject()
    {
        StartCoroutine(Tweening());
    }

    IEnumerator Tweening()
    {
        _pose++;
        if(_pose == list_poses.Count)
        {
            _pose = 0;
        }
       
        yield return new WaitForSeconds(0.2f);

        _sprite.skeletonDataAsset = list_poses[_pose];
        _sprite.Initialize(true);

        ResetCollider();
    }
}
