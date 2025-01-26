using UnityEngine;

[CreateAssetMenu(fileName = "NewPin", menuName = "Scriptable Objects/PinData")]
public class InformationPinData : ScriptableObject
{
    public string Title;       
    public Sprite Image;      
    public string Description;        
    public AudioClip Audio;    
}
