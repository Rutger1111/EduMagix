using System;
using System.Collections.Generic;
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
    DebugTextCollector textCollector;
    // Start is called before the first frame update
    void Start()
    {
        textCollector  = DebugTextCollector.GetTextCollector();
        listOfData = ListOfData.GetListOfData();
        //testData = new Data(testTexture = File.ReadAllBytes("C:/Beegame/ugh/EduMagix/EduMagix/Assets/art/Argentavis.png"), "a", 10, 20);
        CreateDB();
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
        textCollector.AddDebugText("" + aanwezigen);
        Data data = ReadClass(serverhandler.HouseToAddPointsTo);
        textCollector.AddDebugText("hoii" + data.currentAmountOfPoints + data.houseName);
        if (aanwezigen <= data.aantalLeerlingen && aanwezigen > 0){
            data.currentAmountOfPoints += ((100 / data.aantalLeerlingen) * aanwezigen);
        }
        textCollector.AddDebugText("hoi" + data.currentAmountOfPoints + data.houseName);
        AddClass(serverhandler.HouseToAddPointsTo, data);
        ListOfData.GetListOfData().AddData(data);

    }
    public void AddClass(string className, Data data){
        try{
            using (var connection = new SqliteConnection(DBName)){
                connection.Open();

                using (var command = connection.CreateCommand()){
                    var formatter = new BinaryFormatter();
                        byte[] dataBytes = new Decryptor().SerializeDB(data);
                        command.CommandText = "INSERT OR REPLACE INTO classes (class, dataClass) VALUES (@className, @data);";
                        command.Parameters.AddWithValue("@className", className);
                        command.Parameters.AddWithValue("@data", dataBytes);
                        command.ExecuteNonQuery();
                        listOfData.AddData(data);   
                    
                }
            }
        }
        catch(System.Exception ex){
            Debug.LogError("database error Addclass" + ex.Message);
        }
    }
    public void EditClass(string name, Data data){
        try{
            using (var connection = new SqliteConnection(DBName)){
                connection.Open();

                using (var command = connection.CreateCommand()){
                    print(data.houseName);
                    print(name);
                    print(listOfData.GetData(name).houseName);
                    command.CommandText = "INSERT OR REPLACE INTO classes (class, dataClass) VALUES (@className, @data);";
                    command.Parameters.AddWithValue("@className", data.houseName);
                    command.Parameters.AddWithValue("@data", new Decryptor().SerializeDB(data));
                    //command.Parameters.AddWithValue("@oldClassName",  listOfData.GetData(name).houseName);
                    command.ExecuteNonQuery();
                    
                    print("updated");
                    if (name != data.houseName){
                        command.CommandText = "DELETE FROM classes WHERE class = @oldClassName";
                        command.Parameters.AddWithValue("@oldClassName", name);
                        command.ExecuteNonQuery();
                        print("DELETE");

                    }

                }
                connection.Close();
            }
        }
        catch(System.Exception ex){
            Debug.LogError("database error Addclass" + ex.Message);
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
                            Data data = new Decryptor().DeserializeDB(bytes);
                            return data;
                        
                        }

                    }
                    
                }
            }

        }
        catch(System.Exception ex){
            Debug.LogError("database error Readclass" + ex.Message);           
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
                        
                            Data data = new Decryptor().DeserializeDB(bytes);
                            //Sprite test = data.convertToSprite();
                            textCollector.AddDebugText("deserialized data" + data.houseName);   
                            listOfData.AddData(data); 
                            textCollector.AddDebugText("listofate:" + listOfData.GetData(data.houseName).houseName + "," + listOfData.names.Count);

                            //print("added" + listOfData.GetData("a").houseName);   
                        

                        }
                    }
                    
                }
            }
        }
        catch(System.Exception ex){
            Debug.LogError("database error ReadAllClasses" + ex.Message);
        }
    }

    internal void AddClass()
    {
        throw new NotImplementedException();
    }
}
