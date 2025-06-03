using Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    public class HorariosRepository : Repository<HorarioMedico>
    {
        public override Task<string> Agregar(HorarioMedico entity)
        {
            throw new NotImplementedException();
        }

        public override List<HorarioMedico> Consultar()
        {
            string sentencia = "SELECT id_horario_medico, hora_inicio, hora_fin FROM horarios_medicos;";
            List<HorarioMedico> listaH = new List<HorarioMedico>();
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);

            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                listaH.Add(Mappear(reader));
            }
            CerrarConexion();
            return listaH;
        }

        public override Task<string> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public override HorarioMedico Mappear(NpgsqlDataReader reader)
        {
            HorarioMedico horario = new HorarioMedico();
            horario.HoraInicio = (TimeSpan)reader["hora_inicio"];
            horario.HoraFin = (TimeSpan)reader["hora_fin"];

            return horario;
        }

        public override Task<string> Modificar(HorarioMedico entity)
        {
            throw new NotImplementedException();
        }
    }
}
