

$(document).ready(function () { //click
    GetAll();
});

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5235/api/Empleado/GetAll',
        success: function (result) { //200 OK 
            $('#myModal').modal('hide');
            $('#ModalUpdate').modal('hide');
           
            $('#tblEmpleado tbody').empty();
            $.each(result.objects, function (i, empleado) {
                var filas =
                    '<tr>'
                        + '<td class="text-center"> '
                        + '<a href="#" class="btn btn-warning bi bi-pencil-square" onclick="GetById(' + empleado.idEmpleado + ')">'
                        + '</a> '
                        + '</td>'
                    + "<td  id='id' class='text-center'>" + empleado.numeroNomina + "</td>"
                        + "<td class='text-center'>" + empleado.nombre + "</td>"
                        + "<td class='text-center'>" + empleado.apellidoPaterno + "</ td>"
                        + "<td class='text-center'>" + empleado.apellidoMaterno + "</td>"
                        + "<td class='text-center'>" + empleado.entidadFederativa.idEstado + "</td>"
                        /*+ '<td class="text-center">  <a href="#" onclick="return Eliminar(' + empleado.idempleado + ')">' + '</a>    </td>'*/
                        + '<td class="text-center"> '
                    + '<a href="#" class="btn btn-danger bi bi-trash" onclick="return Eliminar(' + empleado.idEmpleado + ')">'
                        + '</a> '
                        + '</td>'
         

                    + "</tr>";
                $("#tblEmpleado tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};


function Actualizar() {
    $('#ModalUpdate').modal('hide');
    var empleado = {
        IdEmpleado: $('#txtIdEmpleado').val(),
        NumeroNomina: $('#txtNumeroNomina').val(),
        Nombre: $('#txtNombre').val(),
        ApellidoPaterno: $('#txtApellidoPaterno').val(),
        ApellidoMaterno: $('#txtApellidoMaterno').val(),
        EntidadFederativa: {
            IdEstado: $('#txtIdEstado').val()
        }
    };

    if (empleado.IdEmpleado == '') {
        empleado.IdEmpleado = 0;
        Add(empleado);
    }
    else {
        Update(empleado);
    }
};


function Add(empleado) {
    $.ajax({
        type: 'POST',
        url: 'http://localhost:5235/api/Empleado/Add',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(empleado),
        success: function (result) {
            $('#myModal').modal('show')
        },
        error: function (result) {
            alert('Error en la consulta. ' + result.responseJSON.ErrorMessage);
        }
    });
};

function GetById(IdEmpleado) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5235/api/Empleado/GetById/' + IdEmpleado,
        success: function (result) {
            $('#txtIdEmpleado').val(result.object.idEmpleado);
            $('#txtNumeroNomina').val(result.object.numeroNomina);
            $('#txtNombre').val(result.object.nombre);
            $('#txtApellidoPaterno').val(result.object.apellidoPaterno);
            $('#txtApellidoMaterno').val(result.object.apellidoMaterno);
            $('#txtIdEstado').val(result.object.entidadFederativa.idEstado);
            $('#ModalUpdate').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};

function Update(empleado) {
    let IdEmplado = parseInt(empleado.IdEmpleado);
    $.ajax({
        type: 'PUT',
        url: 'http://localhost:5235/api/Empleado/Update/' + IdEmplado,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(empleado),
        success: function (result) {
            $('#ModalUpdate').modal('hide');
            $('#myModal').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta. ' + result.responseJSON.ErrorMessage);
        }
    });
};


function Modal() {
    $('#txtIdEmpleado').val('');
    $('#txtNumeroNomina').val('');
    $('#txtNombre').val('');
    $('#txtApellidoPaterno').val('');
    $('#txtApellidoMaterno').val('');
    $('#txtIdEstado').val('');
    $('#ModalUpdate').modal('show');

};

function ModalCerrar() {
    $('#ModalUpdate').modal('hide');
}



function Eliminar(IdEmpleado) {
    if (confirm("¿Estas seguro de eliminar el empleado seleccionado?")) {
        $.ajax({
            type: "DELETE",
            url: 'http://localhost:5235/api/Empleado/Delete/' + IdEmpleado,
            success: function (result) {
                $('#myModal').modal('show');
            
            },
            error: function (result) {
                alert('Errror al eliminar, ' + result.responseJSON.ErrorMessage);
            }

        });
    };
};