using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuaterView;

    [SerializeField]
    Vector3 _delta;

    [SerializeField]
    PlayerController _player;
    public Transform[] target; // 보스 스테이지에서 카메라가 고정될 대상 오브젝트
    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuaterView)
        {
            if (SceneManager.GetActiveScene().name == "BossStageScene") { // 보스 스테이지일 경우 카메라 고정
                transform.position = new Vector3(target[BossStageManage.curStage / 4 - 1].position.x, target[BossStageManage.curStage / 4 - 1].position.y, transform.position.z);
            }
            else { // 일반 스테이지
                transform.position = _player.transform.position + _delta;
            }
            //transform.LookAt(_player.transform);
        }
    }
}