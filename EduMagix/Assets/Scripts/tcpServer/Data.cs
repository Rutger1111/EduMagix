using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;
//[System.Serializable]
[MessagePackObject]
public class Data
{
    [Key(0)]
    public int aantalLeerlingen;
    [Key(1)]
    public byte[] houseImage;
    [Key(2)]
    public string houseName;
    [Key(3)]
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
