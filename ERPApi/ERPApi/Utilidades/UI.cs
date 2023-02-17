using ERPApi.Convertidores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace ERPApi.Utilidades
{
    public class UI
    {
        private string CadenaSQL { get; set; } = string.Empty;

        public UI(string cadenaSql)
        {
            this.CadenaSQL = cadenaSql;

        }



        public async Task<DataTable> Table(string sp)
        {


            var dt = new DataTable();

            using (var con = new SqlConnection(this.CadenaSQL))
            {

                using (var adaptador = new SqlDataAdapter(sp, con))
                {
                    adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
                    await con.OpenAsync();
                    adaptador.Fill(dt);
                }
            }
            return dt;

        }

        public async Task<ContentResult> TableResult(string sp)
        {


            var dt = await this.Table(sp);
            //Serializacion a JSON
            var options = new JsonSerializerOptions()
            {
                Converters = { new DataTableConverter() }
            };
            string jsonDatatable = JsonSerializer.Serialize(dt, options);
            //Para que retorne no estring sino JSON
            var content = new ContentResult();
            content.Content = jsonDatatable;
            content.ContentType = "aplication/json";
            return content;


        }
    }
}
