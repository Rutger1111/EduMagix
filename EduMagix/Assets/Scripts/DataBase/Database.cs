using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Database : MonoBehaviour
{
    private string DBName = "URI=file:Data.db";
    public Data testData;
    public byte[] testTexture;
    public Serverhandler serverhandler;
    ListOfData listOfData;
    // Start is called before the first frame update
    void Start()
    {
        listOfData = ListOfData.GetListOfData();
        testData = new Data(testTexture = File.ReadAllBytes("C:/Beegame/ugh/EduMagix/EduMagix/Assets/art/Argentavis.png"), "Test", 10, 20);
        CreateDB();
        AddClass("a", testData);

        ReadAllClass();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void CreateDB(){
        try{

            using (var connection = new SqliteConnection(DBName)){
                connection.Open();
                using (var command = connection.CreateCommand()){
                    command.CommandText = "CREATE TABLE IF NOT EXISTS classes (class VARCHAR(20) NOT NULL PRIMARY KEY, dataClass BLOB);";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }            
        }
        catch(System.Exception ex){
            Debug.LogError("database error" + ex.Message);
        }
    }
    public void AddPoints(TMPro.TMP_InputField aanwezigenText){
        int aanwezigen = int.Parse(aanwezigenText.text);

        Data data = ReadClass(serverhandler.HouseToAddPointsTo);
        print("hoii" + data.aantalLeerlingen);
        if (aanwezigen <= data.aantalLeerlingen){
            data.currentAmountOfPoints += 100 / data.aantalLeerlingen * aanwezigen;
        }
        print("hoi" + data.currentAmountOfPoints);
        AddClass(data.houseName, data);
        BinaryFormatter formatter = new BinaryFormatter();
        using (var stream = new System.IO.MemoryStream()){
                formatter.Serialize(stream,data);
            byte[] bytes = stream.ToArray();
        }


    }
    public void AddClass(string className, Data data){
        try{
            using (var connection = new SqliteConnection(DBName)){
                connection.Open();

                using (var command = connection.CreateCommand()){
                    var formatter = new BinaryFormatter();
                    using (var stream = new System.IO.MemoryStream()){
                        formatter.Serialize(stream, data);
                        byte[] dataBytes = stream.ToArray();
                        command.CommandText = "INSERT OR REPLACE INTO classes (class, dataClass) VALUES (@className, @data);";
                        command.Parameters.AddWithValue("@className", className);
                        command.Parameters.AddWithValue("@data", dataBytes);
                        command.ExecuteNonQuery();                        
                    }
                }
            }
        }
        catch(System.Exception ex){
            Debug.LogError("database error" + ex.Message);
        }
    }
    public Data ReadClass(string className){
        try{
            using (var connection = new SqliteConnection(DBName)){
                connection.Open();
                using (var command = connection.CreateCommand()){
                    command.CommandText = "SELECT class, dataClass FROM classes WHERE class = @className;";
                    command.Parameters.AddWithValue("@className", className);
                    using (IDataReader reader = command.ExecuteReader()){
                        while (reader.Read()){
                            Debug.Log("Name: " +  reader["class"] + "\n Data:" + reader["dataClass"]);
                            BinaryFormatter formatter = new BinaryFormatter();
                            object obj = reader["dataClass"];
                            byte[] bytes = (byte[])obj;
                            using (var stream = new System.IO.MemoryStream(bytes)){
                                Data data = (Data)formatter.Deserialize(stream);
                                return data;
                            }
                        }

                    }
                    
                }
            }

        }
        catch(System.Exception ex){
            Debug.LogError("database error" + ex.Message);           
        }
        return null;
    }
    public void ReadAllClass(){
        try{
            using (var connection = new SqliteConnection(DBName)){
                connection.Open();
                using (var command = connection.CreateCommand()){
                    command.CommandText = "SELECT class, dataClass FROM classes";
                    //command.Parameters.AddWithValue("@className", className);
                    using (IDataReader reader = command.ExecuteReader()){
                        var formatter = new BinaryFormatter();
                        while (reader.Read()){
                            Debug.Log("Name: " +  reader["class"]);
                            object obj = reader["dataClass"]; 
                            byte[] bytes = (byte[])obj;
                            using (var stream = new System.IO.MemoryStream(bytes)){
                                Data data = (Data)formatter.Deserialize(stream);
                                //Sprite test = data.convertToSprite();
                                print("deserialized data" + data);   
 
                                listOfData.AddData(data); 
                                print("added" + listOfData);   
                            }

                        }
                    }
                    
                }
            }
        }
        catch(System.Exception ex){
            Debug.LogError("database error" + ex.Message);
        }
    }

}
