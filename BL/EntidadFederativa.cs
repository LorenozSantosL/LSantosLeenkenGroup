using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EntidadFederativa
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.LsantosLeenkenGroupContext context = new DL.LsantosLeenkenGroupContext())
                {
                    var estados = context.EntidadFederativas.FromSqlRaw("EntidadFederativaGetAll").ToList();

                    result.Objects = new List<object>();

                    if(estados != null)
                    {
                        foreach(var obj in estados)
                        {
                            ML.EntidadFederativa entidadFederativa = new ML.EntidadFederativa();
                            entidadFederativa.IdEstado = obj.IdEstado;
                            entidadFederativa.Nombre = obj.Estado;

                            result.Objects.Add(entidadFederativa);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.EX = ex;
                result.Message = ex.Message;
            }

            return result;

        }
    }
}
