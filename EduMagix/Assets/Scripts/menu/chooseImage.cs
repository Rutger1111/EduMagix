using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class chooseImage : MonoBehaviour
{
    public HouseImages houseImages;
    public int index = 0;
    public RawImage image;
    public void Start()
    {
        image.texture = houseImages.houseTextures[index];
    }
    public void NextImage(int direction){
        index += direction;
        index = index < 0 ? houseImages.houseTextures.Count - 1 : (index == houseImages.houseTextures.Count ? 0 : index);
        image.texture = houseImages.houseTextures[index];
    }

}
