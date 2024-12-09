using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int aantalLeerlingen;
    public byte[] houseImage;
    public string houseName;
    public float currentAmountOfPoints;
    public Data(byte[] texture, string String, float Float, int AantalLeerlingen){
        houseImage = texture;
        houseName = String;
        currentAmountOfPoints = Float;
        aantalLeerlingen = AantalLeerlingen;
    }
    public Sprite convertToSprite(){
        Texture2D textures = new Texture2D(2,2);
        textures.LoadImage(houseImage);
        Sprite sprite = Sprite.Create(textures, new Rect(0,0, textures.width, textures.height), new Vector2(0,0));
        return sprite;
    }
}
