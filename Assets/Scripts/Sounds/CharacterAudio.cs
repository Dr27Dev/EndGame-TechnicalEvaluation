using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAudio : MonoBehaviour
{
    private enum GroundType { Grass, Wood }
    [SerializeField] private AudioSource _footSteps_AS;
    [SerializeField] private AudioSource _shoot_AS;
    [SerializeField] private AudioSource _hurt_AS;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] _footstepsGrass;
    [SerializeField] private AudioClip[] _footstepsWood;
    [SerializeField] private AudioClip[] _rifleShoot;
    [SerializeField] private AudioClip _getDamage;
    [Header("Ground Type Check")]
    [SerializeField] private float _rayLength = 3;
    [SerializeField] private LayerMask _groundLayers;

    public void FootStep()
    {
        switch (CheckGroundType())
        {
            case GroundType.Grass: _footSteps_AS.PlayOneShot(GetRandomSound(_footstepsGrass)); break;
            case GroundType.Wood: _footSteps_AS.PlayOneShot(GetRandomSound(_footstepsWood)); break;
        }
    }

    public void Fire() => _shoot_AS.PlayOneShot(GetRandomSound(_rifleShoot));

    public void GetHurt()
    {
        if (!_hurt_AS.isPlaying)
        {
            _hurt_AS.pitch = Random.Range(0.75f, 1.25f);
            _hurt_AS.PlayOneShot(_getDamage);   
        }
    }
    
    private AudioClip GetRandomSound(AudioClip[] array)
    {
        int randomIndex = Random.Range(0, array.Length);
        return array[randomIndex];
    }

    private GroundType CheckGroundType()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 1.5f;
        if (Physics.Raycast(rayOrigin, Vector3.down, out var hit, _rayLength, _groundLayers))
        {
            if (hit.collider.CompareTag("Grass")) return GroundType.Grass;
        }
        return GroundType.Wood;
    }
}
