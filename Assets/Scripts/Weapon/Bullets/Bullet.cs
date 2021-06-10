using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 0.2f;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _lifeTime;

    public void Init(Vector3 startPos, Vector3 endpos)
    {
        _startPos = startPos;
        _endPos = endpos;
        _lifeTime = 0f;
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime / _speed;
        transform.position = Vector3.Lerp(_startPos, _endPos, _lifeTime);
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform.position, Vector3.up);
        }
        if (_lifeTime > 1f)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        //play destroy particle
        Destroy(gameObject);
    }
}
