using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioClip = null;
    [SerializeField]
    private float _speed = 1f;      // 9 is a good value
    [SerializeField]
    private float _rightLimit = 7.8f;
    [SerializeField]
    private float _leftLimit = -7.8f;

    private float _horizontalMove = 0f;
    private BallScript _ball = null; 

    private SoundFX _soundFX = null;

    private void Start()
    {
        _soundFX = GetComponent<SoundFX>();
        _ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallScript>();
    }

    void Update()
    {
        if (GameManager.instance.TheGameState == GameManager.GameState.GameOver 
            || GameManager.instance.TheGameState == GameManager.GameState.GameWon)
        {
            return;
        }

        // getting the keyboard input (because the movement looks more natural)
        float _horizontalMove = Input.GetAxis("Horizontal"); // for test - TODO: Delete this line

        // move the player
        transform.Translate(Vector2.right * _horizontalMove * _speed * Time.deltaTime);
    
       // limit the player movement
       if (transform.position.x < _leftLimit)
       {
            transform.position = new Vector2(_leftLimit, transform.position.y);
       }
       else if (transform.position.x > _rightLimit)
       {
            transform.position = new Vector2(_rightLimit, transform.position.y);
       }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sideValue">If value is 1, the player will move to the right
    /// and if it is -1 then the player will move to the left</param>
    public void MoveToSide(float sideValue)
    {
        _horizontalMove = sideValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PowerUp"))
        {
            GameManager.instance.IncreaseHealthByOne();


            // play SFX
            _soundFX.PlaySFX(_audioClip);

            // destroy the object
            Destroy(collision.gameObject);
        }
    }

}
