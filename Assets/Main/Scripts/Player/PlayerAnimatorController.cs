using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    private Animator _animator;
    private bool stopAnimation;

    private Vector2 _animationDirection = new();
    private void OnEnable()
    {
        GlobalEvents.OnPlayerAttacked += () =>
        {
            stopAnimation = true;
        };

        GlobalEvents.OnSwitchToAttackType += (value) => _animator.SetInteger("AttackType", value);
    }

    private void OnDisable()
    {
        GlobalEvents.OnPlayerAttacked -= () =>
        {
            stopAnimation = true;
        }; 
    }

    private void Awake()
    {
        _animator =  GetComponent<Animator>();
        _inputHandler = GetComponentInParent<InputHandler>();
    }
    
    private void Update()
    {
        stopAnimation = false;

        if (!_inputHandler) return;

        if (_inputHandler.LookInputVector == Vector2.zero)
        {
            _animationDirection.x = 0f;
            _animationDirection.y = 1f;
        } 
        else
        {
            var angle = Vector2.SignedAngle(_inputHandler.MoveInputVector, _inputHandler.LookInputVector);
            
            float angleInRadians = angle * Mathf.Deg2Rad;
            _animationDirection.y = Mathf.Cos(angleInRadians);
            _animationDirection.x = Mathf.Sin(angleInRadians);
        }

        _animationDirection *= _inputHandler.MoveInputVector.magnitude;
        _animator.SetFloat("Move Input X", _animationDirection.x);
        _animator.SetFloat("Move Input Z", _animationDirection.y);

        if (!stopAnimation)
            _animator.SetBool("IsAttacking", _inputHandler.LookInputVector != Vector2.zero);
        
    }
}
