﻿using Database;

internal class Program
{
    private static void Main(string[] args)
    {
        var userDB = new UserDB();//ResearchDB ResearchDB = new ResearchDB();
        userDB.Kill();
        userDB.CreateTables();
    }
}