using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _imageComponent = null;
    [SerializeField]
    private int _requiredHits = 1;
    [SerializeField]
    private bool _canBeDestroyed = true;
    [SerializeField]
    private Sprite[] _brickStateSprites = new Sprite[0];
    [SerializeField]
    private AudioClip _audioClip = null;
    [SerializeField]
    private SoundFX _sfx = null;
    [SerializeField]
    private GameObject _sfxObject = null;

    [Header("Score System")]
    [SerializeField]
    private int _brickValue = 10;

    //private int _brickCurrentState = 0;
    private int _brickCurrentHits = 0;
    private bool _wasChecked = false;

    public bool CanBeDestroyed { get => _canBeDestroyed; }

    private void Start()
    {
        if (_imageComponent == null)
        {
            // _imageComponent = GetComponent<SpriteRenderer>();
            Debug.LogError("There is no sprite renderer selected for object: " + this.gameObject.name);
        }

        if (_audioClip == null)
        {
            Debug.LogError("There is no audio clip selected for object: " + this.gameObject.name);
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ball"))
        {
            if (!_canBeDestroyed)
            {
                return;
            }

            // update the nr of hits for this brick
            _brickCurrentHits += 1;

            if (_requiredHits - _brickCurrentHits <= 0 && !_wasChecked)
            {
                _wasChecked = true;
                // instantiate the audio source & the VFX object
                GameObject sfxObj = Instantiate(_sfxObject, this.transform.position, Quaternion.identity);
                AudioSource aSource = sfxObj.GetComponent<AudioSource>();
                aSource.clip = _audioClip;
                aSource.Play();

                // check if I will drop a bonus health
                int rnd = Random.Range(0, 100);
                if (rnd < GameManager.instance.ChancesForBonusLife)
                {
                    Instantiate(GameManager.instance.PowerUps[0], this.transform.position, Quaternion.identity);
                }

                GameManager.instance.UpdateScore(_brickValue);
                // destroy the brick after 0.25 seconds
                Destroy(this.gameObject);
            }
            else 
            {   
                // change the sprite of the brick
                _imageComponent.sprite = _brickStateSprites[_brickCurrentHits - 1];
                
                // play the sound
                _sfx.PlaySFX(_audioClip);
            }
        }
    }
}
