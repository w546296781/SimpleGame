using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class DBManager
{
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader dataReader;
    public string mazeId { get; private set; }

    /// <summary>
    /// Connect to the database.
    /// </summary>
    public void ConnectToDB(string filename)
    {
        string filePath = Application.streamingAssetsPath + "/" + filename;
        try
        {
            dbConnection = new SqliteConnection("Data Source = " + filePath);
            dbConnection.Open();
            Debug.Log("success to connect " + filename);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public HeroClass GetHero(int id)
    {
        ConnectToDB("SimpleGame.db");

        HeroClass hero = new HeroClass();

        dataReader =
            ExecuteQuery("SELECT Name, Attack, Defense, Speed FROM Hero WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                hero.name = dataReader.GetString(0);
                hero.atk = dataReader.GetInt32(1);
                hero.def = dataReader.GetInt32(2);
                hero.speed = dataReader.GetInt32(3);
            }
        }

        CloseConnection();

        return hero;
    }

    public PetClass GetPet(int id)
    {
        ConnectToDB("SimpleGame.db");

        PetClass pet = new PetClass();

        dataReader =
            ExecuteQuery("SELECT Name, Attack, Defense, Speed FROM Pet WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                pet.name = dataReader.GetString(0);
                pet.atk = dataReader.GetInt32(1);
                pet.def = dataReader.GetInt32(2);
                pet.speed = dataReader.GetInt32(3);
            }
        }

        CloseConnection();

        return pet;
    }

    public ServantClass GetServant(int id)
    {
        ConnectToDB("SimpleGame.db");

        ServantClass servant = new ServantClass();

        dataReader =
            ExecuteQuery("SELECT Name, Attack, Defense, Speed FROM Servant WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                servant.name = dataReader.GetString(0);
                servant.atk = dataReader.GetInt32(1);
                servant.def = dataReader.GetInt32(2);
                servant.speed = dataReader.GetInt32(3);
            }
        }

        CloseConnection();

        return servant;
    }

    public EnemyClass GetEnemy(int id)
    {
        ConnectToDB("SimpleGame.db");

        EnemyClass enemy = new EnemyClass();

        dataReader =
            ExecuteQuery("SELECT Name, Attack, Defense, Speed FROM Enemy WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                enemy.name = dataReader.GetString(0);
                enemy.atk = dataReader.GetInt32(1);
                enemy.def = dataReader.GetInt32(2);
                enemy.speed = dataReader.GetInt32(3);
            }
        }

        CloseConnection();

        return enemy;
    }

    public SqliteDataReader ExecuteQuery(string queryString)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        Debug.Log(queryString);
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }

    public void CloseConnection()
    {
        if (dbCommand != null)
        {
            dbCommand.Cancel();
        }
        dbCommand = null;

        if (dataReader != null)
        {
            dataReader.Close();
        }
        dataReader = null;

        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
    }
}
