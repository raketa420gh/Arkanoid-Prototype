using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    #region Variables

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject prefab;
    
    #endregion
    
    
    #region Public methods

    public void PlayAudioClip(int index)
    {
        SpawnAudioClip(index, Vector3.zero);
    }

    public void SpawnAudioClip(int index, Vector3 position)
    {
        var audioClip = audioClips[index];
        prefab.GetComponent<AudioSource>().clip = audioClip;
        var spawnObject = Instantiate(prefab, position, Quaternion.identity, parentTransform);
        Destroy(spawnObject, audioClip.length);
    }

    #endregion
}