using Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CitaMedicaRepository : Repository<CitaMedica>
    {
        public override List<CitaMedica> Consultar()
        {
            string sentencia = "SELECT cm.id_cita, pp.nombre_completo, Pm.nombre_completo, hc.hora_inicio, hc.hora_fin, hc.fecha_hora FROM CITAS_MEDICAS CM JOIN MEDICOS M ON m.id_medico = cm.id_medico JOIN PERSONAS Pm ON m.ID_PERSONA = Pm.ID_PERSONA JOIN PACIENTES P ON p.id_paciente = cm.id_paciente JOIN PERSONAS pp ON p.ID_PERSONA = pp.ID_PERSONA JOIN HORARIOS_CITAS hc ON hc.id_horario_cita = cm.id_horario_cita;";
            List<CitaMedica> listaM = new List<CitaMedica>();
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);

            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                listaM.Add(Mappear(reader));
            }
            CerrarConexion();
            return listaM;
        }

        public override CitaMedica Mappear(NpgsqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override Task<string> Agregar(CitaMedica entity)
        {
            throw new NotImplementedException();
        }

        public override Task<string> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<string> Modificar(CitaMedica entity)
        {
            throw new NotImplementedException();
        }
    }
}
