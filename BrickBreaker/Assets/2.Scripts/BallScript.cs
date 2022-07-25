using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb = null;
    [SerializeField]
    private Transform _playerTranform = null;
    [SerializeField]
    private float _speed = 400f;
    [SerializeField]
    private LevelManager _levelManager = null;
    [SerializeField]
    private AudioClip[] _audioClips = new AudioClip[2];
    [SerializeField]
    private SoundFX _sfx = null;

    void Start()
    {
        if (_rb == null)
        {
            Debug.LogError("You did not set the RigidBody2D");
        }

        if (_playerTranform == null)
        {
            Debug.LogError("You did not set the _playerTransform");
        }

        if (_levelManager == null)
        {
            Debug.LogError("You did not set the _levelManager");
        }
    }

    private void Update()
    {
        // if game not started - the ball will move with player
        if (GameManager.instance.TheGameState != GameManager.GameState.GameStarted)
        {
            transform.position = _playerTranform.position;
        }
        else 
        {
            //BallHitPaddle();
            /*
            var posBall = this.transform.position;
            var posPlayer = _playerTranform.transform.parent.position;
            var dir = (posPlayer - posBall).normalized;
            Debug.Log("Dir: " + dir);
            */
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {   
        // checking if we hit the bottom collider
        if (col.CompareTag("BotCol"))
        {   
            // play sfx
            _sfx.PlaySFX(_audioClips[1]);

            // check the remaining lives
            _levelManager.CheckPlayerLives();

            // stop the ball
            _rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (this.transform.position.y < collision.transform.position.y)
                return;

            _sfx.PlaySFX(_audioClips[0]);

            /*float dist = this.transform.position.x - collision.transform.position.x;
            //float d2 = this.transform.position.x - collision.transform.position.x;

            Vector2 dir = new Vector2(dist, 1).normalized;

            print("Dist: " + dist + "\nDir: " + dir);

            if (dist > 0)
            {
                _rb.velocity = Vector2.zero;
                if (dir.x < 0 && dir.y < 0.9)
                {
                    _rb.AddForce(new Vector2(-dist, 1).normalized * _speed);     // add the force to move the ball

                }
                else
                {
                    _rb.AddForce(dir * _speed);     // add the force to move the ball

                }
            }
            else if (dist < 0)
            {
                _rb.velocity = Vector2.zero;
                if (dir.x < 0 && dir.y < 0.9)
                {
                    _rb.AddForce(new Vector2(-dist, 1).normalized * _speed);     // add the force to move the ball
                }
                else
                {
                    _rb.AddForce(dir * _speed);     // add the force to move the ball
                }
            }
            */


            /*if (dir.x > 0)
            {
                _rb.velocity = Vector2.zero;
                if (dist < 0)
                {
                    _rb.AddForce(dir * _speed);     // add the force to move the ball
                }
                else if (dist > 0)
                {
                    _rb.AddForce(new Vector2(-dist, 1).normalized * _speed);     // add the force to move the ball
                }
            }
            else if (dir.x < 0)
            {
                _rb.velocity = Vector2.zero;
                if (dist < 0)
                {
                    _rb.AddForce(new Vector2(dist, 1).normalized * _speed);     // add the force to move the ball
                }
                else if (dist > 0)
                {
                    _rb.AddForce(dir * _speed);     // add the force to move the ball
                }
            }
            */
        }
    }

    public void StartGame()
    {
        GameManager.instance.ChangeGameState(GameManager.GameState.GameStarted);

        float randomXForce = Random.Range(-1f, 1f);
        _rb.AddForce(new Vector2(randomXForce, 1).normalized * _speed);     // add the force to move the ball
    }
}
