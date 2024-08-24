using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothAppear : MonoBehaviour
{
    public Transform startPositionObject;  // 開始位置を示すゲームオブジェクト
    public Transform endPositionObject;    // 終了位置を示すゲームオブジェクト
    public float duration = 1.0f;          // アニメーションの持続時間

    void Start()
    {
        // オブジェクトを開始位置に配置
        if (startPositionObject != null)
        {
            transform.position = startPositionObject.position;
        }

        // アニメーションを開始
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        float elapsedTime = 0;

        Vector3 startPosition = startPositionObject.position;
        Vector3 endPosition = endPositionObject.position;

        while (elapsedTime < duration)
        {
            // 位置をLerpで滑らかに変化
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 最後に正確に終了位置に設定
        transform.position = endPosition;
    }
}
