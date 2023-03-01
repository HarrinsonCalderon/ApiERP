using ERPApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using ERPApi.Convertidores;
using ERPApi.Utilidades ;

namespace ERPApi.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class ApiClienteController : ControllerBase
    {

        private readonly UI db;
        //para usar inyeccion de dependencias inyectar la clase UI, para solo hacer ese paso una vez
        public ApiClienteController(UI db) {
            this.db = db;
        }

        //Controlador con entidades
        //[HttpGet("GetCliente")]
        //public Collection<Cliente> GetCliente() {

        //    var datos = new Collection<Cliente>();

        //    using (var con=new SqlConnection(UI.CadenaSQL)) {
        //        con.Open();
        //        using (var adaptador=new SqlDataAdapter("select * from Cliente",con)) {
        //            var reader = adaptador.SelectCommand.ExecuteReader();
        //            while (reader.Read()) {
        //                var datos2 = new Cliente()
        //                {
        //                    Id = reader.GetInt32(0),
        //                    Nombre = reader.GetString(1)
        //                };
        //                datos.Add(datos2);

        //            }
        //        }
        //    return datos;
        //}}

        //ASINCRONO PETICION GET
        //[HttpGet("GetCliente")]
        //public async Task<Collection<Cliente>> GetCliente()
        //{

        //    var datos = new Collection<Cliente>();

        //    using (var con = new SqlConnection(UI.CadenaSQL))
        //    {

        //        using (var adaptador = new SqlDataAdapter("select * from Cliente", con))
        //        {
        //            await con.OpenAsync();
        //            var reader = await adaptador.SelectCommand.ExecuteReaderAsync();
        //            while (await reader.ReadAsync())
        //            {
        //                var datos2 = new Cliente()
        //                {
        //                    Id = reader.GetInt32(0),
        //                    Nombre = reader.GetString(1)
        //                };
        //                datos.Add(datos2);

        //            }
        //        }

        //    }
        //    return datos;
        //}

        //Asincrono retorna datatable
        //[HttpGet("GetCliente")]
        //public async Task<DataTable> GetCliente()
        //{

            
        //    var dt = new DataTable();

        //    using (var con = new SqlConnection(UI.CadenaSQL))
        //    {

        //        using (var adaptador = new SqlDataAdapter("select * from Cliente", con))
        //        {
        //            await con.OpenAsync();  
        //            adaptador.Fill(dt);
        //        }
        //    }
        //    var options = new JsonSerializerOptions() {
        //        Converters = { new DataTableConverter() }
        //    };
        //    string jsonDatatable = JsonSerializer.Serialize(dt,options);
        //    return dt;

        //}
        //Asincrono retorna datatable en JSON 
        [HttpGet("GetCliente")]
        public async Task<IActionResult> GetCliente()
        {

            return await  db.TableResult("sp_obtenerCliente"); ;

        }
        [HttpPost("PostCliente")]
        public async Task<IActionResult> PostCliente(Cliente model) {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@id", model.Id));
            Params.Add(new SqlParameter("@nombre", model.Nombre));
            return await db.PostResult("sp_guardarCliente", Params);
        }
        [HttpPost("DeleteCliente")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@id", id));
             
            return await db.PostResult("sp_eliminarCliente", Params);
        }

    }

   

}