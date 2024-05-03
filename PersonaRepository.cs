using BVallejoT5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Java.Util.Jar.Attributes;

namespace BVallejoT5
{
    public class PersonaRepository
    {
        string _dbPath;
        private SQLiteConnection _connection;
        public string StatusMsg { get; set; }

        private void Init()
        {
            if (_connection is not null)
                return;

            _connection = new(_dbPath);
            _connection.CreateTable<Persona>();

        }
        public PersonaRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddPerson(string name)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre es requerido");

                Persona persona = new() { Name = name };

                result = _connection.Insert(persona);
                StatusMsg = string.Format("Se inserto una persona", result, name);


            }
            catch (Exception ex)
            {
                StatusMsg = string.Format("Error al insertar una persona", name, ex.Message);
            }
        }

        public void EditPerson(int id,string name)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre es requerido");
                if (id <= 0)
                    throw new Exception("ID debe ser mayor a cero");

                Persona persona = new() {Id =id, Name = name };

                result = _connection.Update(persona);
                StatusMsg = string.Format("Se Edito una persona", result, name);


            }
            catch (Exception ex)
            {
                StatusMsg = string.Format("Error al Editar una persona", name, ex.Message);
            }
        }
        public void DeletePerson(int id)
        {
            try
            {
                Init();

                if (id <= 0)
                    throw new Exception("ID debe ser mayor a cero");

                Persona persona = _connection.Find<Persona>(x => x.Id == id);


                _connection.Delete(persona);

                
                StatusMsg = string.Format("Se Elimino una persona");


            }
            catch (Exception ex)
            {
                StatusMsg = string.Format("Error al Eliminar una persona", id, ex.Message);
            }
        }

        public List<Persona> GetAllPersonas()
        {
            try
            {
                Init();
                return _connection.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMsg = string.Format("Error al Listar Personas", ex.Message);
            }
            return new List<Persona>();
        }
    }
}
