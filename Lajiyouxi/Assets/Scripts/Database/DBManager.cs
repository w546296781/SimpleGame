using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class DBManager
{
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader dataReader;

    #region BASIC
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


    #endregion

    #region HERO
    public HeroClass GetHero(int id)
    {
        ConnectToDB("SimpleGame.db");

        HeroClass hero = new HeroClass();
        hero.id = id;

        dataReader =
            ExecuteQuery("SELECT Name, Attack, Defense, Speed, Life, AP, Dodge, CritChance, CritDamage, " +
            "FireResis, ColdResis, LightResis, FirePene, ColdPene, LightPene, " +
            "Level, EXP, STR, AGI, INT, AttrPoint, SkillPoint, Skill, Gold FROM Hero WHERE ID = " + id + ";");
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
                hero.skillPoint = dataReader.GetInt32(21);
                string skillstr = dataReader.GetString(22);
                hero.skillList = ConvertSkillToList(skillstr);
                hero.gold = dataReader.GetInt32(23);
            }
        }

        CloseConnection();

        return hero;
    }

    public void CreateHero(HeroClass hero)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Hero (ID, Name, Attack, Defense, Speed, Life, AP, Dodge, CritChance, CritDamage, FireResis, ColdResis, LightResis, FirePene, ColdPene, LightPene, Level, EXP, STR, AGI, INT, AttrPoint, SkillPoint, Skill) VALUES (" + hero.id + ", '" + hero.name + "', " + hero.atk + ", " + hero.def + ", " + hero.speed
            + ", " + hero.life + ", " + hero.ap + ", " + hero.dodge + ", " + hero.critChance + ", " + hero.critDamage + ", " + hero.fireResis + ", " + hero.coldResis + ", " + hero.lightResis + ", " + hero.FirePene + ", " + hero.coldPene + ", " + hero.lightPene
            + ", " + hero.level + ", " + hero.exp + ", " + hero.str + ", " + hero.agi + ", " + hero.Int + ", " + hero.attrPoint + ", " + hero.skillPoint + ", '" + ConvertSkillToString(hero.skillList) + "')";
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
               + ", Level = " + hero.level + ", EXP = " + hero.exp + ", STR = " + hero.str + ", AGI = " + hero.agi + ", INT = " + hero.Int + ", AttrPoint = " + hero.attrPoint + ", SkillPoint = " + hero.skillPoint + ", Skill = '" + ConvertSkillToString(hero.skillList) + "', Gold = " + hero.gold + " WHERE ID = " + hero.id;
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

    public List<List<int>> ConvertSkillToList(string str)
    {
        List<List<int>> skill = new List<List<int>>();
        string[] firstSplit = str.Split(';');
        foreach (string i in firstSplit)
        {
            if (i != "")
            {
                string[] secondSplit = i.Split('-');
                int skillName = int.Parse(secondSplit[0]);
                int skillLevel = int.Parse(secondSplit[1]);
                List<int> thisSkill = new List<int>();
                thisSkill.Add(skillName);
                thisSkill.Add(skillLevel);
                skill.Add(thisSkill);
            }
        }
        return skill;
    }

    public string ConvertSkillToString(List<List<int>> skill)
    {
        string result = "";
        foreach (var i in skill)
        {
            result += i[0] + "-" + i[1] + ";";
        }
        return result;
    }

    #endregion

    #region SKILL
    public SkillClass GetSkill(int id)
    {
        ConnectToDB("SimpleGame.db");

        SkillClass skill = new SkillClass();
        skill.id = id;

        dataReader =
            ExecuteQuery("SELECT Name, Type, Level, Active, Passive1, Passive2, Passive3, Passive4, Passive5, Passive6" +
            ", Passive7, Passive8, Passive9, Passive10, Passive11, Passive12, BasicDamage FROM Skill WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                skill.name = dataReader.GetString(0);
                skill.type = dataReader.GetInt32(1);
                skill.level = dataReader.GetInt32(2);
                skill.active = dataReader.GetInt32(3);
                skill.passive1 = dataReader.GetInt32(4);
                skill.passive2 = dataReader.GetInt32(5);
                skill.passive3 = dataReader.GetInt32(6);
                skill.passive4 = dataReader.GetInt32(7);
                skill.passive5 = dataReader.GetInt32(8);
                skill.passive6 = dataReader.GetInt32(9);
                skill.passive7 = dataReader.GetInt32(10);
                skill.passive8 = dataReader.GetInt32(11);
                skill.passive9 = dataReader.GetInt32(12);
                skill.passive10 = dataReader.GetInt32(13);
                skill.passive11 = dataReader.GetInt32(14);
                skill.passive12 = dataReader.GetInt32(15);
                skill.basicDamage = dataReader.GetInt32(16);
            }
        }

        CloseConnection();

        return skill;
    }

    public List<SkillClass> GetAllSkill()
    {
        ConnectToDB("SimpleGame.db");

        List<SkillClass> skillList = new List<SkillClass>();

        dataReader =
            ExecuteQuery("SELECT Name, Type, Level, Active, Passive1, Passive2, Passive3, Passive4, Passive5, Passive6" +
            ", Passive7, Passive8, Passive9, Passive10, Passive11, Passive12, ID, BasicDamage FROM Skill;");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                SkillClass skill = new SkillClass();
                skill.name = dataReader.GetString(0);
                skill.type = dataReader.GetInt32(1);
                skill.level = dataReader.GetInt32(2);
                skill.active = dataReader.GetInt32(3);
                skill.passive1 = dataReader.GetInt32(4);
                skill.passive2 = dataReader.GetInt32(5);
                skill.passive3 = dataReader.GetInt32(6);
                skill.passive4 = dataReader.GetInt32(7);
                skill.passive5 = dataReader.GetInt32(8);
                skill.passive6 = dataReader.GetInt32(9);
                skill.passive7 = dataReader.GetInt32(10);
                skill.passive8 = dataReader.GetInt32(11);
                skill.passive9 = dataReader.GetInt32(12);
                skill.passive10 = dataReader.GetInt32(13);
                skill.passive11 = dataReader.GetInt32(14);
                skill.passive12 = dataReader.GetInt32(15);
                skill.id = dataReader.GetInt32(16);
                skill.basicDamage = dataReader.GetInt32(17);

                skillList.Add(skill);
            }
        }

        CloseConnection();

        return skillList;
    }

    public void SaveSkill(SkillClass skill)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "UPDATE Skill SET Name = '" + skill.name + "', Type = " + skill.type + ", Level = " + skill.level + ", Active = " + skill.active + ", Passive1 = " + skill.passive1 + ", Passive2 = " + skill.passive2
             + ", Passive3 = " + skill.passive3 + ", Passive4 = " + skill.passive4 + ", Passive5 = " + skill.passive5 + ", Passive6 = " + skill.passive6 + ", Passive7 = " + skill.passive7
              + ", Passive8 = " + skill.passive8 + ", Passive9 = " + skill.passive9 + ", Passive10 = " + skill.passive10 + ", Passive11 = " + skill.passive11 + ", Passive12 = " + skill.passive12 + " WHERE ID = " + skill.id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    #endregion

    #region PET
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

    public void CreatePet(PetClass pet)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Pet (ID, Name, Attack, Defense, Speed) VALUES (" + pet.id + ", '" + pet.name + "', " + pet.atk + ", " + pet.def + ", " + pet.speed + ")";
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

    public void DeletePet(int id)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "DELETE FROM Pet WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }


    #endregion

    #region SERVANT
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

    public void CreateServant(ServantClass servant)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Servant (ID, Name, Attack, Defense, Speed) VALUES (" + servant.id + ", '" + servant.name + "', " + servant.atk + ", " + servant.def + ", " + servant.speed + ")";
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

    public void DeleteServant(int id)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "DELETE FROM Servant WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    #endregion

    #region ENEMY
    public EnemyClass GetEnemy(int id)
    {
        ConnectToDB("SimpleGame.db");

        EnemyClass enemy = new EnemyClass();
        enemy.id = id;

        dataReader =
            ExecuteQuery("SELECT Name, Attack, Defense, Speed, Life FROM Enemy WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                enemy.name = dataReader.GetString(0);
                enemy.atk = dataReader.GetInt32(1);
                enemy.def = dataReader.GetInt32(2);
                enemy.speed = dataReader.GetInt32(3);
                enemy.life = dataReader.GetInt32(4);
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
            ExecuteQuery("SELECT Name, Attack, Defense, Speed, ID, Life FROM Enemy;");
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
                enemy.life = dataReader.GetInt32(5);
                result.Add(enemy);
            }
        }

        CloseConnection();

        return result;
    }

    public void CreateEnemy(EnemyClass enemy)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Enemy (ID, Name, Attack, Defense, Speed, Life) VALUES (" + enemy.id + ", '" + enemy.name + "', " + enemy.atk + ", " + enemy.def + ", " + enemy.speed + ", " + enemy.life + ")";
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    public void SaveEnemy(EnemyClass enemy)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "UPDATE Enemy SET Name = '" + enemy.name + "', Attack = " + enemy.atk + ", Defense = " + enemy.def + ", Speed = " + enemy.speed + ", Life = " + enemy.life + " WHERE ID = " + enemy.id;
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
    #endregion

    #region GAME

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

    public void CreateGame(int id, string date)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Game (ID, Date) VALUES (" + id + ", '" + date + "')";
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

    public void DeleteGame(int id)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "DELETE FROM Game WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    #endregion

    #region EVENT

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

    public void CreateEvent(EventClass newEvent)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "INSERT INTO Event (ID, Level, Area1, Area2, Area3, EventLeft, BattlePosition, BattleFinish) VALUES (" + newEvent.id + ", " + newEvent.level + ", " + newEvent.area1 + ", " + newEvent.area2 + ", " + newEvent.area3 + ", " + newEvent.event_left + ", " + newEvent.battle_position + ", " + newEvent.battle_finish + ")";
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

    public void DeleteEvent(int id)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "DELETE FROM Event WHERE ID = " + id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    #endregion

    #region PACKAGE

    public PackageClass GetPackage(int id)
    {
        ConnectToDB("SimpleGame.db");

        PackageClass newPackage = new PackageClass();
        newPackage.id = id;

        dataReader =
            ExecuteQuery("SELECT Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7, Slot8, Slot9, Slot10, " +
            "Slot11, Slot12, Slot13, Slot14, Slot15, Slot16, Slot17, Slot18, Slot19, Slot20, " +
            "Slot21, Slot22, Slot23, Slot24, Slot25, Slot26, Slot27, Slot28, Slot29, Slot30, " +
            "Weapon, Armor, Halmet, Boot, Ring, Amulet FROM Package WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                newPackage.slot1 = dataReader.GetInt32(0);
                newPackage.slot2 = dataReader.GetInt32(1);
                newPackage.slot3 = dataReader.GetInt32(2);
                newPackage.slot4 = dataReader.GetInt32(3);
                newPackage.slot5 = dataReader.GetInt32(4);
                newPackage.slot6 = dataReader.GetInt32(5);
                newPackage.slot7 = dataReader.GetInt32(6);
                newPackage.slot8 = dataReader.GetInt32(7);
                newPackage.slot9 = dataReader.GetInt32(8);
                newPackage.slot10 = dataReader.GetInt32(9);
                newPackage.slot11 = dataReader.GetInt32(10);
                newPackage.slot12 = dataReader.GetInt32(11);
                newPackage.slot13 = dataReader.GetInt32(12);
                newPackage.slot14 = dataReader.GetInt32(13);
                newPackage.slot15 = dataReader.GetInt32(14);
                newPackage.slot16 = dataReader.GetInt32(15);
                newPackage.slot17 = dataReader.GetInt32(16);
                newPackage.slot18 = dataReader.GetInt32(17);
                newPackage.slot19 = dataReader.GetInt32(18);
                newPackage.slot20 = dataReader.GetInt32(19);
                newPackage.slot21 = dataReader.GetInt32(20);
                newPackage.slot22 = dataReader.GetInt32(21);
                newPackage.slot23 = dataReader.GetInt32(22);
                newPackage.slot24 = dataReader.GetInt32(23);
                newPackage.slot25 = dataReader.GetInt32(24);
                newPackage.slot26 = dataReader.GetInt32(25);
                newPackage.slot27 = dataReader.GetInt32(26);
                newPackage.slot28 = dataReader.GetInt32(27);
                newPackage.slot29 = dataReader.GetInt32(28);
                newPackage.slot30 = dataReader.GetInt32(29);
                newPackage.weapon = dataReader.GetInt32(30);
                newPackage.armor = dataReader.GetInt32(31);
                newPackage.halmet = dataReader.GetInt32(32);
                newPackage.boot = dataReader.GetInt32(33);
                newPackage.ring = dataReader.GetInt32(34);
                newPackage.amulet = dataReader.GetInt32(35);
            }
        }

        CloseConnection();

        return newPackage;
    }

    public void SavePackage(PackageClass newPackage)
    {
        ConnectToDB("SimpleGame.db");

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText =
            "UPDATE Package SET Slot1 = " + newPackage.slot1 + ", Slot2 = " + newPackage.slot2 + ", Slot3 = " + newPackage.slot3 + ", Slot4 = " + newPackage.slot4 + ", Slot5 = " + newPackage.slot5 +
             ", Slot6 = " + newPackage.slot6 + ", Slot7 = " + newPackage.slot7 + ", Slot8 = " + newPackage.slot8 + ", Slot9 = " + newPackage.slot9 + ", Slot10 = " + newPackage.slot10 +
              ", Slot11 = " + newPackage.slot11 + ", Slot12 = " + newPackage.slot12 + ", Slot13 = " + newPackage.slot13 + ", Slot14 = " + newPackage.slot14 + ", Slot15 = " + newPackage.slot15 +
               ", Slot16 = " + newPackage.slot16 + ", Slot17 = " + newPackage.slot17 + ", Slot18 = " + newPackage.slot18 + ", Slot19 = " + newPackage.slot19 + ", Slot20 = " + newPackage.slot20 +
                ", Slot21 = " + newPackage.slot21 + ", Slot22 = " + newPackage.slot22 + ", Slot23 = " + newPackage.slot23 + ", Slot24 = " + newPackage.slot24 + ", Slot25 = " + newPackage.slot25 +
                 ", Slot26 = " + newPackage.slot26 + ", Slot27 = " + newPackage.slot27 + ", Slot28 = " + newPackage.slot28 + ", Slot29 = " + newPackage.slot29 + ", Slot30 = " + newPackage.slot30 +
                  ", Weapon = " + newPackage.weapon + ", Armor = " + newPackage.armor + ", Halmet = " + newPackage.halmet + ", Boot = " + newPackage.boot + ", Ring = " + newPackage.ring + ", Amulet = " + newPackage.amulet +" WHERE ID = " + newPackage.id;
        dbCommand.ExecuteNonQuery();

        CloseConnection();
    }

    #endregion

    #region EQUIPMENT

    public List<EquipmentClass> GetAllEquipment()
    {
        ConnectToDB("SimpleGame.db");

        List<EquipmentClass> result = new List<EquipmentClass>();

        dataReader =
            ExecuteQuery("SELECT ID, Name, Attr1, Attr2, Attr3, Attr4, Attr5, Attr6, Class, Price FROM Equipment;");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                EquipmentClass equipment = new EquipmentClass();
                equipment.id = dataReader.GetInt32(0);
                equipment.name = dataReader.GetString(1);
                equipment.attr1 = dataReader.GetString(2);
                equipment.attr2 = dataReader.GetString(3);
                equipment.attr3 = dataReader.GetString(4);
                equipment.attr4 = dataReader.GetString(5);
                equipment.attr5 = dataReader.GetString(6);
                equipment.attr6 = dataReader.GetString(7);
                equipment.Class = dataReader.GetInt32(8);
                equipment.price = dataReader.GetInt32(9);
                result.Add(equipment);
            }
        }

        CloseConnection();

        return result;
    }

    #endregion
}
