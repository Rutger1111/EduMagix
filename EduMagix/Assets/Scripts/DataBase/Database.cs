using System.Data;
using System.Xml.Linq;
using Mono.Data.Sqlite;
using UnityEngine;

public class Database : MonoBehaviour
{
    private string DBName = "URI=file:Data.db";
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
        AddClass("a", 1);

        ReadAllClass("a");
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
                    command.CommandText = "CREATE TABLE IF NOT EXISTS classes (class VARCHAR(20) NOT NULL PRIMARY KEY, points INT);";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }            
        }
        catch(System.Exception ex){
            Debug.LogError("database error" + ex.Message);
        }
    }
    public void AddClass(string className, int Points){
        try{
            using (var connection = new SqliteConnection(DBName)){
                connection.Open();
                using (var command = connection.CreateCommand()){
                    
                    command.CommandText = "INSERT OR REPLACE INTO classes (class, points) VALUES (@className, @Points);";
                    command.Parameters.AddWithValue("@className", className);
                    command.Parameters.AddWithValue("@Points", Points);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch(System.Exception ex){
            Debug.LogError("database error" + ex.Message);
        }
    }
    public void ReadClass(string className){
        try{
            using (var connection = new SqliteConnection(DBName)){
                connection.Open();
                using (var command = connection.CreateCommand()){
                    command.CommandText = "SELECT class, points FROM classes WHERE class = @className;";
                    command.Parameters.AddWithValue("@className", className);
                    using (IDataReader reader = command.ExecuteReader()){
                        while (reader.Read()){
                            Debug.Log("Name: " +  reader["class"] + "\n Points:" + reader["points"]);
                        }
                    }
                    
                }
            }
        }
        catch(System.Exception ex){
            Debug.LogError("database error" + ex.Message);
        }
    }
    public void ReadAllClass(string className){
        try{
            using (var connection = new SqliteConnection(DBName)){
                connection.Open();
                using (var command = connection.CreateCommand()){
                    command.CommandText = "SELECT class, points FROM classes";
                    command.Parameters.AddWithValue("@className", className);
                    using (IDataReader reader = command.ExecuteReader()){
                        while (reader.Read()){
                            Debug.Log("Name: " +  reader["class"] + "\n Points:" + reader["points"]);
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
