using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.LsantosLeenkenGroupContext context = new DL.LsantosLeenkenGroupContext())
                {
                    var addEmplead = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', {empleado.EntidadFederativa.IdEstado}");

                    if(addEmplead > 1)
                    {
                        result.Correct = true;
                        result.Message = "Se ha agregado el usuario";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Ocurrio un error";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.EX = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.LsantosLeenkenGroupContext context = new DL.LsantosLeenkenGroupContext())
                {
                    var updateEmpleado = context.Database.ExecuteSqlRaw($"EmpleadoUpdate {empleado.IdEmpleado}, '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', { empleado.EntidadFederativa.IdEstado}");

                    if (updateEmpleado > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha acualizado el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.EX = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LsantosLeenkenGroupContext context = new DL.LsantosLeenkenGroupContext())
                {
                    var empleados = context.Empleados.FromSqlRaw("EmpleadoGetAll").ToList();

                    result.Objects = new List<object>();

                    if(empleados != null)
                    {
                        foreach(var obj in empleados)
                        {
                            ML.Empleado empleado = new ML.Empleado();

                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.NumeroNomina = obj.NumeroNomina;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;

                            empleado.EntidadFederativa = new ML.EntidadFederativa();

                            empleado.EntidadFederativa.IdEstado = obj.IdEstado.Value;

                            result.Objects.Add(empleado);
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

        public static ML.Result GetById(int IdEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LsantosLeenkenGroupContext context = new DL.LsantosLeenkenGroupContext())
                {
                    var objEmpleado = context.Empleados.FromSqlRaw($"EmpleadoGetById {IdEmpleado}").AsEnumerable().FirstOrDefault();

                    if(objEmpleado != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();

                        empleado.IdEmpleado = objEmpleado.IdEmpleado;
                        empleado.NumeroNomina = objEmpleado.NumeroNomina;
                        empleado.Nombre = objEmpleado.Nombre;
                        empleado.ApellidoPaterno = objEmpleado.ApellidoPaterno;
                        empleado.ApellidoMaterno = objEmpleado.ApellidoMaterno;

                        empleado.EntidadFederativa = new ML.EntidadFederativa();
                        empleado.EntidadFederativa.IdEstado = objEmpleado.IdEstado.Value;

                        result.Object = empleado;
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

        public static ML.Result Delete(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LsantosLeenkenGroupContext context = new DL.LsantosLeenkenGroupContext())
                {
                    var deleteEmpleado = context.Database.ExecuteSqlRaw($"EmpleadoDelete {IdEmpleado}");

                    if (deleteEmpleado > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha eliminado el usuario";
                    }
                }
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
