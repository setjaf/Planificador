using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Planificador.Modelos;

namespace Planificador.Repositorios
{
    public class ObjetivoRepositorio
    {
        private SQLiteConnection conn;
        
        public ObjetivoRepositorio()
        {
            conn = ConnectionManager.getConnection();
        }

        public int agregarObjetivo(Objetivo objetivo)
        {
            return conn.Insert(objetivo);
        }

        public List<Objetivo> consultarObjetivos(int idTarea)
        {
            return conn.Table<Objetivo>().Where(x => x.idTarea == idTarea).OrderBy(x => x.finalizacionFecha).ToList();
        }

        public Objetivo consultarObjetivo(int idObjetivo)
        {
            return conn.Table<Objetivo>().Where(x => x.id == idObjetivo).FirstOrDefault();
        }

        public int editarObjetivo(Objetivo objetivo)
        {
            return conn.Update(objetivo);
        }

        public int eliminarObjetivo(int idObjetivo)
        {
            return conn.Delete(idObjetivo,new TableMapping(typeof(Objetivo)));
        }
    }
}
