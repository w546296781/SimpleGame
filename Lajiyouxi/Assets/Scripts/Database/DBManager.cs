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
        hero.id = id;

        dataReader =
            ExecuteQuery("SELECT Name, Attack, Defense, Speed, Life, AP, Dodge, CritChance, CritDamage, FireResis, ColdResis, LightResis, FirePene, ColdPene, LightPene, Level, EXP, STR, AGI, INT, AttrPoint FROM Hero WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                hero.name = dataReader.GetString(0);
                hero.atk = dataReader.GetInt32(1);
                hero.def = dataReader.GetInt32(2);
                hero.speed = dataReader.GetInt32(3);
                hero.life = dataReader.GetInt32(4);
                hero.ap = dataReader.GetInt32(5);
                hero.dodge = dataReader.GetDouble(6);
                hero.critChance = dataReader.GetDouble(7);
                hero.critDamage = dataReader.GetInt32(8);
                hero.fireResis = dataReader.GetInt32(9);
                hero.coldResis = dataReader.GetInt32(10);
                hero.lightResis = dataReader.GetInt32(11);
                hero.FirePene = dataReader.GetInt32(12);
                hero.coldPene = dataReader.GetInt32(13);
                hero.lightPene = dataReader.GetInt32(14);
                hero.level = dataReader.GetInt32(15);
                hero.exp = dataReader.GetInt32(16);
                hero.str = dataReader.GetInt32(17);
                hero.agi = dataReader.GetInt32(18);
                hero.Int = dataReader.GetInt32(19);
                hero.attrPoint = dataReader.GetInt32(20);
            }
        }

        CloseConnection();

        return hero;
    }

    public PetClass GetPet(int id)
    {
        ConnectToDB("SimpleGame.db");

        PetClass pet = new PetClass();
        pet.id = id;

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
        servant.id = id;

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
        enemy.id = id;

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

    public List<EnemyClass> GetAllEnemy()
    {
        ConnectToDB("SimpleGame.db");

        List<EnemyClass> result = new List<EnemyClass>();

        dataReader =
            ExecuteQuery("SELECT Name, Attack, Defense, Speed, ID FROM Enemy;");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                EnemyClass enemy = new EnemyClass();
                enemy.name = dataReader.GetString(0);
                enemy.atk = dataReader.GetInt32(1);
                enemy.def = dataReader.GetInt32(2);
                enemy.speed = dataReader.GetInt32(3);
                enemy.id = dataReader.GetInt32(4);
                result.Add(enemy);
            }
        }

        CloseConnection();

        return result;
    }

    public string GetGame(int id)
    {
        ConnectToDB("SimpleGame.db");

        string result = "";

        dataReader =
            ExecuteQuery("SELECT Date FROM Game WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                result = dataReader.GetString(0);
            }
        }

        CloseConnection();

        return result;
    }

    public Dictionary<int,string> GetAllGame()
    {
        ConnectToDB("SimpleGame.db");

        Dictionary<int,string> result = new Dictionary<int, string>();

        dataReader =
            ExecuteQuery("SELECT ID, Date FROM Game;");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                result.Add(dataReader.GetInt32(0), dataReader.GetString(1));
            }
        }

        CloseConnection();

        return result;
    }

    public EventClass GetEvent(int id)
    {
        ConnectToDB("SimpleGame.db");

        EventClass newEvent = new EventClass();
        newEvent.id = id;

        dataReader =
            ExecuteQuery("SELECT Level, Area1, Area2, Area3, EventLeft, BattlePosition, BattleFinish FROM Event WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                newEvent.level = dataReader.GetInt32(0);
                newEvent.area1 = dataReader.GetInt32(1);
                newEvent.area2 = dataReader.GetInt32(2);
                newEvent.area3 = dataReader.GetInt32(3);
                newEvent.event_left = dataReader.GetInt32(4);
                newEvent.battle_position = dataReader.GetInt32(5);
                newEvent.battle_finish = dataReader.GetInt32(6);
            }
        }

        CloseConnection();

        return newEvent;
    }

    public void CreateGame(int id, string date)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Game (ID, Date) VALUES (" + id + ", '" + date + "')";
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void CreateHero(HeroClass hero)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Hero (ID, Name, Attack, Defense, Speed, Life, AP, Dodge, CritChance, CritDamage, FireResis, ColdResis, LightResis, FirePene, ColdPene, LightPene, Level, EXP, STR, AGI, INT, AttrPoint) VALUES (" + hero.id + ", '" + hero.name + "', " + hero.atk + ", " + hero.def + ", " + hero.speed
            + ", " + hero.life + ", " + hero.ap + ", " + hero.dodge + ", " + hero.critChance + ", " + hero.critDamage + ", " + hero.fireResis + ", " + hero.coldResis + ", " + hero.lightResis + ", " + hero.FirePene + ", " + hero.coldPene + ", " + hero.lightPene
            + ", " + hero.level + ", " + hero.exp + ", " + hero.str + ", " + hero.agi + ", " + hero.Int + ", " + hero.attrPoint + ")";
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void CreatePet(PetClass pet)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Pet (ID, Name, Attack, Defense, Speed) VALUES (" + pet.id + ", '" + pet.name + "', " + pet.atk + ", " + pet.def + ", " + pet.speed + ")";
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void CreateServant(ServantClass servant)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Servant (ID, Name, Attack, Defense, Speed) VALUES (" + servant.id + ", '" + servant.name + "', " + servant.atk + ", " + servant.def + ", " + servant.speed + ")";
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void CreateEnemy(EnemyClass enemy)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Enemy (ID, Name, Attack, Defense, Speed) VALUES (" + enemy.id + ", '" + enemy.name + "', " + enemy.atk + ", " + enemy.def + ", " + enemy.speed + ")";
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void CreateEvent(EventClass newEvent)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Event (ID, Level, Area1, Area2, Area3, EventLeft, BattlePosition, BattleFinish) VALUES (" + newEvent.id + ", " + newEvent.level + ", " + newEvent.area1 + ", " + newEvent.area2 + ", " + newEvent.area3 + ", " + newEvent.event_left + ", " + newEvent.battle_position + ", " + newEvent.battle_finish + ")";
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void SaveGame(int id, string date)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "UPDATE Game SET Date = '" + date + "' WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void SaveHero(HeroClass hero)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "UPDATE Hero SET Name = '" + hero.name + "', Attack = " + hero.atk + ", Defense = " + hero.def + ", Speed = " + hero.speed
             + ", Life = " + hero.life + ", AP = " + hero.ap + ", Dodge = " + hero.dodge + ", CritChance = " + hero.critChance + ", CritDamage = " + hero.critDamage + ", FireResis = " + hero.fireResis
              + ", ColdResis = " + hero.coldResis + ", LightResis = " + hero.lightResis + ", FirePene = " + hero.FirePene + ", ColdPene = " + hero.coldPene + ", LightPene = " + hero.lightPene
               + ", Level = " + hero.level + ", EXP = " + hero.exp + ", STR = " + hero.str + ", AGI = " + hero.agi + ", INT = " + hero.Int + ", AttrPoint = " + hero.attrPoint + " WHERE ID = " + hero.id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void SavePet(PetClass pet)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "UPDATE Pet SET Name = '" + pet.name + "', Attack = " + pet.atk + ", Defense = " + pet.def + ", Speed = " + pet.speed + " WHERE ID = " + pet.id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void SaveServant(ServantClass servant)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "UPDATE Servant SET Name = '" + servant.name + "', Attack = " + servant.atk + ", Defense = " + servant.def + ", Speed = " + servant.speed + " WHERE ID = " + servant.id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void SaveEnemy(EnemyClass enemy)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "UPDATE Enemy SET Name = '" + enemy.name + "', Attack = " + enemy.atk + ", Defense = " + enemy.def + ", Speed = " + enemy.speed + " WHERE ID = " + enemy.id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void SaveEvent(EventClass newEvent)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "UPDATE Event SET Level = " + newEvent.level + ", Area1 = " + newEvent.area1 + ", Area2 = " + newEvent.area2 + ", Area3 = " + newEvent.area3 + ", EventLeft = " + newEvent.event_left + ", BattlePosition = " + newEvent.battle_position + ", BattleFinish = " + newEvent.battle_finish + " WHERE ID = " + newEvent.id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void DeleteGame(int id)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "DELETE FROM Game WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void DeleteHero(int id)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "DELETE FROM Hero WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void DeletePet(int id)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "DELETE FROM Pet WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void DeleteServant(int id)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "DELETE FROM Servant WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void DeleteEnemy(int id)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "DELETE FROM Enemy WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void DeleteEvent(int id)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "DELETE FROM Event WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
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
