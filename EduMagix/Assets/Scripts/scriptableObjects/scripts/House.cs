using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EduMagix/House")]
public class House : ScriptableObject
{
    public List<Color> houseBubblesColor = new List<Color>();
    public List<Color> houseBarColor = new List<Color>();
    public List<Color> houseBarTopColor = new List<Color>();
    public List<Sprite> houseTextures = new List<Sprite>();
    public List<Sprite> houseShields = new List<Sprite>();

}
