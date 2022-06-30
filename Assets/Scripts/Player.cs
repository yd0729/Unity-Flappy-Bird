using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float force;

    public delegate void NotifyDeath();
    public event NotifyDeath OnDie;
    public Text ScoreText;
    public Text FinalScoreText;

    public void Set()
    {
        _Score = 0;
        _isDead = false;
        _rigidbody2D.simulated = false;
        _animator.SetTrigger("Idle");
        _animator.applyRootMotion = false;
        transform.position = _initialPosition;
    }

    public void Run()
    {
        _rigidbody2D.simulated = true;
        _animator.applyRootMotion = true;
        _animator.SetTrigger("Fly");
    }

    public void Stop()
    {
        _isDead = true;
        OnDie?.Invoke();
    }

    Rigidbody2D _rigidbody2D;
    Animator _animator;
    bool _isDead;
    Vector3 _initialPosition;
    int _score;
    int _Score
    {
        get => _score;
        set
        {
            _score = value;
            ScoreText.text = _score.ToString();
            FinalScoreText.text = _score.ToString();
        }
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _initialPosition = transform.position;

        print(_rigidbody2D);

        Set();
    }

    void Update()
    {
        if (_isDead)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(new(0, force));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Empty"))
        {
            return;
        }
        Stop();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Empty"))
        {
            _Score += 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Stop();
    }
}
