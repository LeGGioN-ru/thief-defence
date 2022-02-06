using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Thief : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _path;
    [SerializeField] private int _delayBeforeNextPoint;

    private Transform[] _points;
    private SpriteRenderer _sprite;
    private int _currentPoint = 0;
    private bool _isMove = true;
    private WaitForSecondsRealtime _realSecondTimeDelay;
    private WaitForFixedUpdate _fixedUpdateDelay = new WaitForFixedUpdate();

    public void StopMoving()
    {
        _isMove = false;
    }

    public void StartMoving()
    {
        _isMove = true;
    }

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        GetPathPoints();

        _realSecondTimeDelay = new WaitForSecondsRealtime(_delayBeforeNextPoint);

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (_isMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, _points[_currentPoint].position, _speed * Time.deltaTime);

            if (transform.position.x == _points[_currentPoint].position.x)
            {
                _currentPoint++;

                _sprite.flipX = !_sprite.flipX;

                if (_currentPoint >= _points.Length)
                {
                    _currentPoint = 0;
                }

                yield return _realSecondTimeDelay;
            }

            yield return _fixedUpdateDelay;
        }
    }

    private void GetPathPoints()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }
}
