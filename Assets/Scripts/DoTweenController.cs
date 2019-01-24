using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DoTweenController : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetLocation = Vector3.zero;

    [Range(0.5f, 10.0f), SerializeField]
    private float _moveDuration = 1.0f;

    [SerializeField]
    private Ease _moveEase = Ease.Linear;

    [SerializeField]
    private Color _targetColor;

    [Range(1.0f, 500.0f), SerializeField]
    private float _scaleMultiplier = 3.0f;

    [Range(1.0f, 10.0f), SerializeField]
    private float _colorChangeDuration = 1.0f;

    [SerializeField]
    private DoTweenType _doTweenType = DoTweenType.MovementOneWay;

    private enum DoTweenType
    {
        MovementOneWay,
        MovementTwoWay,
        MovementTwoWayWithSequence,
        MovementOneWayColorChange,
        MovementOneWayColorChangeAndScale
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_doTweenType == DoTweenType.MovementOneWay)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase);
        }
        else if (_doTweenType == DoTweenType.MovementTwoWay)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            StartCoroutine(MoveWithBothWays());
        }
        else if (_doTweenType == DoTweenType.MovementTwoWayWithSequence)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            Vector3 originalLocation = transform.position;
            DOTween.Sequence()
                .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                .Append(transform.DOMove(originalLocation, _moveDuration).SetEase(_moveEase));
        }
        else if (_doTweenType == DoTweenType.MovementOneWayColorChange)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            DOTween.Sequence()
                .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                .Append(transform.GetComponent<Renderer>().material
                .DOColor(_targetColor, _colorChangeDuration).SetEase(_moveEase));
        }
        else if (_doTweenType == DoTweenType.MovementOneWayColorChangeAndScale)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            DOTween.Sequence()
                .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                .Append(transform.DOScale(_scaleMultiplier, _moveDuration / 2.0f).SetEase(_moveEase))
                .Append(transform.GetComponent<Renderer>().material
                .DOColor(_targetColor, _colorChangeDuration).SetEase(_moveEase));
        }
    }

    private IEnumerator MoveWithBothWays()
    {
        Vector3 originalLocation = transform.position;
        transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase);
        yield return new WaitForSeconds(_moveDuration);
        transform.DOMove(originalLocation, _moveDuration).SetEase(_moveEase);
    }
}
