using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Planificador.Modelos;

namespace Planificador.Repositorios
{
    public class ConnectionManager
    {
        private static SQLiteConnection conn;
        public ConnectionManager(string dbPath)
        {
            conn = new SQLiteConnection(dbPath);

            conn.CreateTable<Tarea>();
            conn.CreateTable<Objetivo>();
            conn.CreateTable<Recurrencia>();
            conn.CreateTable<Actividad>();
            conn.CreateTable<RecurrenciasCargadas>();
        }

        public static SQLiteConnection getConnection()
        {
            return conn;
        }
    }
}
