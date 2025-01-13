using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;
//[System.Serializable]
public class Data
{
    public int aantalLeerlingen;
    public int houseImage;
    public string houseName;
    public float currentAmountOfPoints;
    
    public Data(int ImageIndex, string String, float Float, int AantalLeerlingen){
        houseImage = ImageIndex;
        
        houseName = String;
        currentAmountOfPoints = Float;
        aantalLeerlingen = AantalLeerlingen;
    }
    
    public Sprite convertToSprite(){
        Texture2D textures = new Texture2D(2,2);
        //textures.LoadImage(System.Convert.FromBase64String(houseImage));
        Sprite sprite = Sprite.Create(textures, new Rect(0,0, textures.width, textures.height), new Vector2(0,0));
        return sprite;
    }
}
