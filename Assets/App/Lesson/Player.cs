using System;
using UnityEngine;

namespace App.Lesson
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;

        [SerializeField] private Vector3 _targetPosition;
    
        private void Update()
        {
            var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            transform.position += moveDirection * (_speed * Time.deltaTime);
        
            if (Vector3.Distance(transform.position, _targetPosition) < 0.5f)
            {
                Debug.Log("You win!");
            }
        }
    }

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            transform.position += moveDirection * (_speed * Time.deltaTime);
        }
    }

    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform _character;
        [SerializeField] private Vector3 _targetPosition;

        private void Update()
        {
            if (Vector3.Distance(_character.transform.position, _targetPosition) < 0.5f)
            {
                Debug.Log("You win");
            }
        }
    }

    public class EnemyExample : MonoBehaviour
    {
        [SerializeField] private int _startBulletCount = 10;
        [SerializeField] private int _jumpForce = 35;
        [SerializeField] private string _analyticsUrl = "http://example.com";

        private int _bulletCount;

        private void Awake()
        {
            _bulletCount = _startBulletCount;
        }

        private void Update()
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                if (_bulletCount > 0)
                {
                    Debug.Log("Shoot!");
                    _bulletCount--;
                    SendAnalytics("Shoot");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump with force " + _jumpForce);
            }
        }

        private void SendAnalytics(string eventKey)
        {
            Debug.Log("Send analytics " + eventKey + "to server " + _analyticsUrl);
        }
    }

    public class Enemy : MonoBehaviour
    {
        [SerializeField] private AnalyticsManager _analyticsManager;
        [SerializeField] private int _startBulletCount = 10;

        private int _bulletCount;

        private void Awake()
        {
            _bulletCount = _startBulletCount;
        }

        private void Update()
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                if (_bulletCount > 0)
                {
                    Debug.Log("Shoot!");
                    _bulletCount--;
                    _analyticsManager.SendAnalytics("Shoot");
                }
            }
        }
    }
    
    public class JumpController : MonoBehaviour
    {
        [SerializeField] private int _jumpForce = 35;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump with force " + _jumpForce);
            }
        }
    }
    
    public class AnalyticsManager : MonoBehaviour
    {
        [SerializeField] private string _analyticsUrl = "http://example.com";
        
        public void SendAnalytics(string eventKey)
        {
            Debug.Log("Send analytics " + eventKey + "to server " + _analyticsUrl);
        }
    }

    public class InputController : MonoBehaviour
    {
        public Vector2 MoveDirection;
        
        private IInputHandler _inputHandler;

        private void Start()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _inputHandler = new TouchInputHandler();
                    break;
                case RuntimePlatform.PS5:
                    _inputHandler = new JoystickInputHandler();
                    break;
                default:
                    _inputHandler = new StandaloneInputHandler();
                    break;
            }
        }

        private void Update()
        {
            MoveDirection = _inputHandler.GetDirection();
        }
    }
    
    public interface IInputHandler
    {
        Vector2 GetDirection();
    }

    public class StandaloneInputHandler : IInputHandler
    {
        Vector2 IInputHandler.GetDirection()
        {
            return Vector2.zero;
        }
    }

    public class TouchInputHandler : IInputHandler
    {
        Vector2 IInputHandler.GetDirection()
        {
            return Vector2.zero;
        }
    }
    
    public class JoystickInputHandler : IInputHandler
    {
        Vector2 IInputHandler.GetDirection()
        {
            return Vector2.zero;
        }
    }

    
    
    
}
