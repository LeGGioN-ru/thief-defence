using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Thief : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _path;
    [SerializeField] private int _delayBeforeNextPoint;

    private Transform[] _points;
    private SpriteRenderer _thiefSprite;
    private int _currentPoint = 0;
    private bool _isThiefMove = true;

    private void Start()
    {
        _thiefSprite = GetComponent<SpriteRenderer>();
        GetPathPoints();

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (_isThiefMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, _points[_currentPoint].position, _speed * Time.deltaTime);

            if (transform.position.x == _points[_currentPoint].position.x)
            {
                _currentPoint++;

                _thiefSprite.flipX = _thiefSprite.flipX == true ? false : true;

                if (_currentPoint >= _points.Length)
                {
                    _currentPoint = 0;
                }

                yield return new WaitForSecondsRealtime(_delayBeforeNextPoint);
            }

            yield return new WaitForFixedUpdate();
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

    public void StopMoving()
    {
        _isThiefMove = false;
    }

    public void StartMoving()
    {
        _isThiefMove = true;
    }
}