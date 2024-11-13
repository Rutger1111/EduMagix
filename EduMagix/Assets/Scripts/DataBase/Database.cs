using System.Data;
using System.Xml.Linq;
using Mono.Data.Sqlite;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class Database : MonoBehaviour
{
    private string DBName = "URI=file:Data.db";
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
        AddUser("a", 1);

        ReadUser("a");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateDB(){
        using (var connection = new SqliteConnection(DBName)){
            connection.Open();
            print("hoi");
            using (var command = connection.CreateCommand()){
                command.CommandText = "CREATE TABLE IF NOT EXISTS classes (class VARCHAR(20) NOT NULL PRIMARY KEY, points INT);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public void AddUser(string Username, int Role){
        using (var connection = new SqliteConnection(DBName)){
            connection.Open();
            using (var command = connection.CreateCommand()){
                
                command.CommandText = "INSERT OR REPLACE INTO classes (class, points) VALUES ('"+ Username + "', '" + Role + "');";
                command.ExecuteNonQuery();
                print("yep");
                
            }
            connection.Close();
        }
    }
    public void ReadUser(string name){
        using (var connection = new SqliteConnection(DBName)){
            connection.Open();
            using (var command = connection.CreateCommand()){
                command.CommandText = "SELECT class, points FROM classes WHERE class = '" + name + "';";
                using (IDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        Debug.Log("Name: " +  reader["class"] + "/n Role: " + reader["points"]);
                    }
                    reader.Close();
                }
                
            }
            connection.Close();
        }
    }
}
