using ERPApi.Convertidores;
using ERPApi.Entity;
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



        public async Task<DataTable> Table(string sp, IEnumerable<SqlParameter>? Params = null)
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

        public async Task<ContentResult> TableResult(string sp, IEnumerable<SqlParameter>? Params = null)
        {


            var dt = await this.Table(sp,Params);
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
        public async Task<TransactionEN> Exectute(string sp,IEnumerable<SqlParameter>? Params = null)
        {


            var result = new TransactionEN();
            try { 
            
            using (var con = new SqlConnection(this.CadenaSQL))
            {

                using (var comando = new SqlCommand(sp, con))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (Params != null) {
                        foreach (var it in Params) {
                            comando.Parameters.AddWithValue(it.ParameterName,it.Value);
                        }
                    }
                    await con.OpenAsync();
                    await comando.ExecuteNonQueryAsync();
                    result.success = 1;
                    result.mensaje = "Ok";
                }
            }
            }
            catch (Exception ex)
            {
                result.success = 0;
                result.mensaje = ex.Message.ToString();
            }
            return result;


        }
        public async Task<ContentResult> PostResult(string sp,IEnumerable<SqlParameter>? Params=null)
        {

             
            var dt = await this.Exectute(sp,Params);
            
            string jsonDatatable = JsonSerializer.Serialize(dt);
            //Para que retorne no estring sino JSON
            var content = new ContentResult();
            content.Content = jsonDatatable;
            content.ContentType = "aplication/json";
            return content;


        }
    }
}
