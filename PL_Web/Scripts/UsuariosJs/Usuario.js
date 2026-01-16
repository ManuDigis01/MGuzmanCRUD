$(document).ready(function () {
    GetAll();
    GetAllRol();
    GetAllEstado();
});

let imagenBase64 = null;

function GetAll() {
    $.ajax({
        type: 'GET',
        url: endpointUsuario + 'GetAll',
        dataType: 'JSON',
        success: function (result) {
            if (result.Correct) {
                let usuarios = result.Objects;

                $.each(usuarios, function (index, value) {


                    let imagenSrc = 'https://e7.pngegg.com/pngimages/556/580/png-clipart-computer-icons-symbol-user-profile-logo-symbol-miscellaneous-white.png';


                    if (value.Imagen !== null) {
                        imagenSrc = `data:image/*;base64,${value.Imagen}`;
                    }

                    let etiqueta = `
<tr>

    <td>${value.IdUsuario}</td>
    <td>${value.UserName}</td>
    <td>${value.Nombre}</td>
    <td>${value.ApellidoPaterno}</td>
    <td>${value.ApellidoMaterno}</td>
    <td>${value.Email}</td>
    <td>${value.Password}</td>
    <td>${value.FechaNacimiento}</td>
    <td>${value.SEXO}</td>
    <td>${value.Telefono}</td>
    <td>${value.Celular}</td>
    <td>${value.Estatus}</td>
    <td>${value.CURP}</td>

    <td >
        <img class="col-12" src="${imagenSrc}" />
    </td>

    <td >${value.Rol.Nombre}</td>

    <td style="min-width:300px; white-space:normal;">
        ${value.Direccion.Calle} ${value.Direccion.NumeroInterior} 
        ext.${value.Direccion.NumeroExterior},
        ${value.Direccion.Colonia.Nombre} 
        ${value.Direccion.Colonia.CPostal}
        ${value.Direccion.Colonia.Municipio.Nombre},
        ${value.Direccion.Colonia.Municipio.Estado.Nombre}
    </td>

    <td>
        <button class="btn btn-outline-info" onclick="MostrarModal(${value.IdUsuario})">
            <i class="bi bi-pencil-square"></i>
        </button>
    </td>

    <td>
        <button class="btn btn-outline-danger" onclick="Delete(${value.IdUsuario})">
            <i class="bi bi-trash-fill"></i>
        </button>
    </td>
</tr>`;

                    $('#TbobyJsGetAll').append(etiqueta);
                });
            }
        },
        error: function () {
            console.error('Error al obtener usuarios');
        }
    });
}
function GetAllRol() {
    $.ajax({
        type: 'GET',
        url: endpointUsuario + 'GetAllRol',
        dataType: 'JSON',
        success: function (result) {
            if (result.Correct) {

                let roles = result.Objects;
                $.each(roles, function (index, rol) {
                    let etiqueta = `<option value="${rol.IdRol}">
                            ${rol.Nombre}
                         </option>`;
                    $('#selecUsuarioRol').append(etiqueta);
                });
            }
        },
        error: function () {
            console.error('Error al obtener usuarios');
        }

    })
}

function GetAllEstado() {
    $.ajax({
        type: 'GET',
        url: endpointUsuario + 'GetAllEstado',
        dataType: 'JSON',
        success: function (result) {
            if (result.Correct) {
                $('#ddlEstado').empty().append('<option value="">Seleccione Estado</option>');

                $.each(result.Objects, function (index, estado) {
                    $('#ddlEstado').append(
                        `<option value="${estado.IdEstado}">${estado.Nombre}</option>`
                    );
                });
            }
        },
        error: function () {
            console.error('Error al obtener estados');
        }
    });
}

$('#ddlEstado').change(function () {
    let idEstado = $('#ddlEstado').val();

    $('#ddlMunicipio').empty().append('<option value="">Seleccione Municipio</option>');
    $('#ddlColonia').empty().append('<option value="">Seleccione Colonia</option>');

    if (idEstado) {
        GetMunicipioByEstado(idEstado);
    }
});

function GetMunicipioByEstado(IdEstado, IdMunicipio = null, IdColonia = null) {
    $.ajax({
        type: 'GET',
        url: endpointUsuario + 'GetByIdMunicipio/' + IdEstado,
        success: function (result) {
            if (result.Correct) {

                $('#ddlMunicipio').empty()
                    .append('<option value="">Seleccione Municipio</option>');

                $.each(result.Objects, function (index, municipio) {
                    $('#ddlMunicipio').append(
                        `<option value="${municipio.IdMunicipio}">${municipio.Nombre}</option>`
                    );
                });

                // ✔️ Selecciona el municipio si viene de edición
                if (IdMunicipio) {
                    $('#ddlMunicipio').val(IdMunicipio);
                    GetColoniaByMunicipio(IdMunicipio, IdColonia);
                }
            }
        }
    });
}

$('#ddlMunicipio').change(function () {
    let idMunicipio = $('#ddlMunicipio').val();

    $('#ddlColonia').empty().append('<option value="">Seleccione Colonia</option>');

    if (idMunicipio) {
        GetColoniaByMunicipio(idMunicipio);
    }
});

function GetColoniaByMunicipio(IdMunicipio, IdColonia = null) {
    $.ajax({
        type: 'GET',
        url: endpointUsuario + 'GetByIdColonia/' + IdMunicipio,
        success: function (result) {
            if (result.Correct) {

                $('#ddlColonia').empty()
                    .append('<option value="">Seleccione Colonia</option>');

                $.each(result.Objects, function (index, colonia) {
                    $('#ddlColonia').append(
                        `<option value="${colonia.IdColonia}">${colonia.Nombre}</option>`
                    );
                });

                // ✔️ Selecciona colonia al editar
                if (IdColonia) {
                    $('#ddlColonia').val(IdColonia);
                }
            }
        }
    });
}

function GetById(IdUsuario) {
    $.ajax({
        type: 'GET',
        url: endpointUsuario + 'GetById/' + IdUsuario,
        dataType: 'JSON',
        success: function (result) {

            let usuario = result.Object;
            $('#IdUsuario').val(usuario.IdUsuario);
            $("#inptUsuarioUserName").val(usuario.UserName);
            $("#inptUsuarioNombre").val(usuario.Nombre);
            $("#inptUsuarioApellidoPaterno").val(usuario.ApellidoPaterno);
            $("#inptUsuarioApellidoMaterno").val(usuario.ApellidoMaterno);
            $("#inptUsuarioEmail").val(usuario.Email);
            $("#inptUsuarioPassword").val(usuario.Password);
            $("#inptUsuarioFechaDeNacimeiento").val(usuario.FechaNacimiento);
            $('#selctUsuarioSexo').val(usuario.SEXO);
            $('#inptUsuarioTelefono').val(usuario.Telefono);
            $('#inptUsuarioCelular').val(usuario.Celular);
            $('#selecUsuarioEstatus').val(usuario.Estatus.toString());
            $('#inptUsuarioCurp').val(usuario.CURP);


            $('#selecUsuarioRol').val(usuario.Rol.IdRol);



            $('#inptUsuarioCalle').val(usuario.Direccion.Calle);
            $('#inptUsuarioNInterior').val(usuario.Direccion.NumeroInterior);
            $('#inptUsuarioNExterior').val(usuario.Direccion.NumeroExterior);


            $('#ddlEstado').val(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);

            GetMunicipioByEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado,
                usuario.Direccion.Colonia.Municipio.IdMunicipio,
                usuario.Direccion.Colonia.IdColonia)

            if (usuario.Imagen) {
                $('#imgPreview').attr(
                    'src',
                    'data:image/*;base64,' + usuario.Imagen
                );
                imagenBase64 = usuario.Imagen; // ✅ conservar imagen
            }



        }
    })
}



function Delete(IdUsuario) {

    if (!confirm("¿Estás seguro de eliminar este usuario?")) {
        return;
    }

    $.ajax({
        type: 'DELETE',
        url: endpointUsuario + 'Delete/' + IdUsuario, // ✅ URL correcta
        dataType: 'JSON',
        success: function (result) {

            if (result.Correct) {
                alert("Usuario eliminado correctamente");

                // Limpiar tabla y volver a cargar
                $('#TbobyJsGetAll').empty();
                GetAll();
            } else {
                alert("Error al eliminar el usuario");
            }
        },
        error: function () {
            alert("Error en el servidor al eliminar");
        }
    });
}


function Add() {

    let usuario = {
        UserName: $('#inptUsuarioUserName').val(),
        Nombre: $('#inptUsuarioNombre').val(),
        ApellidoPaterno: $('#inptUsuarioApellidoPaterno').val(),
        ApellidoMaterno: $('#inptUsuarioApellidoMaterno').val(),
        Email: $('#inptUsuarioEmail').val(),
        Password: $('#inptUsuarioPassword').val(),
        FechaNacimiento: $('#inptUsuarioFechaDeNacimeiento').val(),
        SEXO: $('#selctUsuarioSexo').val(),
        Telefono: $('#inptUsuarioTelefono').val(),
        Celular: $('#inptUsuarioCelular').val(),
        Estatus: $('#selecUsuarioEstatus').val(),
        CURP: $('#inptUsuarioCurp').val(),
        Imagen: imagenBase64, // ✅ IMAGEN
        Rol: {
            IdRol: $('#selecUsuarioRol').val()
        },
        Direccion: {
            Calle: $('#inptUsuarioCalle').val(),
            NumeroInterior: $('#inptUsuarioNInterior').val(),
            NumeroExterior: $('#inptUsuarioNExterior').val(),
            Colonia: {
                IdColonia: $('#ddlColonia').val()
            }
        }
    };

    $.ajax({
        type: 'POST',
        url: endpointUsuario + 'Add',
        data: JSON.stringify(usuario),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        success: function (result) {
            if (result.Correct) {
                alert('Usuario registrado correctamente');
                location.reload();
            }
        }
    });
}

function Update() {

    let usuario = {
        IdUsuario: $('#IdUsuario').val(),
        UserName: $('#inptUsuarioUserName').val(),
        Nombre: $('#inptUsuarioNombre').val(),
        ApellidoPaterno: $('#inptUsuarioApellidoPaterno').val(),
        ApellidoMaterno: $('#inptUsuarioApellidoMaterno').val(),
        Email: $('#inptUsuarioEmail').val(),
        Password: $('#inptUsuarioPassword').val(),
        FechaNacimiento: $('#inptUsuarioFechaDeNacimeiento').val(),
        SEXO: $('#selctUsuarioSexo').val(),
        Telefono: $('#inptUsuarioTelefono').val(),
        Celular: $('#inptUsuarioCelular').val(),
        Estatus: $('#selecUsuarioEstatus').val(),
        CURP: $('#inptUsuarioCurp').val(),
        Rol: {
            IdRol: $('#selecUsuarioRol').val()
        },
        Direccion: {
            Calle: $('#inptUsuarioCalle').val(),
            NumeroInterior: $('#inptUsuarioNInterior').val(),
            NumeroExterior: $('#inptUsuarioNExterior').val(),
            Colonia: {
                IdColonia: $('#ddlColonia').val()
            }
        }
    };

    // ✅ SOLO si seleccionó nueva imagen
    if (imagenBase64) {
        usuario.Imagen = imagenBase64;
    }

    $.ajax({
        type: 'PUT',
        url: endpointUsuario + 'Update',
        data: JSON.stringify(usuario),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        success: function (result) {
            if (result.Correct) {
                alert('Usuario actualizado correctamente');
                location.reload();
            }
        }
    });
}


$('#btnGurdar').click(function () {



    let idUsuario = $('#IdUsuario').val();
    if (idUsuario && idUsuario !== "0") {
        Update();
    } else {
        Add();
    }

});




//---------------------------------------------------------------------------------------------------------------------------
function validaImagen() {
    var extensionaesValidas = ['png', 'jpg', 'jpeg']
    var extensionesArchivos = document.getElementById('imagenUsuario').value.split('.').pop().toLowerCase();
    var esValido = false;
    for (var index in extensionaesValidas) {

        if (extensionesArchivos == extensionaesValidas[index]) {
            esValido = true;
            console.log("imagen valida")
            break;
        }
    }
    if (!esValido) {
        alert("Imangen no valida" + extensionaesValidas)
        $('#imagenUsuario').val("");
    }
}
function validaImagen() {
    const file = document.getElementById("imagenUsuario").files[0];

    if (file && !file.type.startsWith("image/")) {
        alert("Solo se permiten imágenes");
        document.getElementById("imagenUsuario").value = "";
        imagenBase64 = null;
    }
}


function visualizarImagen(input) {
    if (input.files && input.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            // 🔥 PREVIEW
            $('#imgPreview').attr('src', e.target.result);

            // 🔥 GUARDAR BASE64 (SIN EL encabezado data:image)
            imagenBase64 = e.target.result.split(',')[1];
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function MostrarModal(IdUsuario = 0) {
    LimpiarModal();


    if (IdUsuario > 0) {
        GetById(IdUsuario);
    }

    $('#modalFrom').modal('show');
}

function LimpiarModal() {
    $('input').val('');
    $('select').prop('selectedIndex', 0);
    $('#imgPreview').attr('src', '');
    $('#IdUsuario').val('');
    imagenBase64 = null; // ✅ CLAVE
    $('input').removeClass("is-invalid");
    $('input').removeClass("is-valid");
    $('select').removeClass("is-invalid");
    $('select').removeClass("is-valid");
}




/*--------------------------------VALIDACIONES----------------------------------------------*/

// ===================== LETRAS =====================
function SoloLetras(input, event) {
    const char = String.fromCharCode(event.which || event.keyCode);
    const regex = /^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$/;

    let padre = $(input).closest('div');
    padre.find('.valid-feedback, .invalid-feedback').remove();

    if (regex.test(char)) {
        $(input).removeClass("is-invalid").addClass("is-valid");
        padre.append("<div class='valid-feedback'>válido.</div>");
        return true;
    } else {
        $(input).removeClass("is-valid").addClass("is-invalid");
        padre.append("<div class='invalid-feedback'>inválido.</div>");
        return false;
    }
}

// ===================== SOLO NÚMEROS =====================
function soloNumeros(input, event) {
    const regex = /^[0-9]$/;
    let padre = $(input).closest('div');
    padre.find('.valid-feedback, .invalid-feedback').remove();

    if (!regex.test(event.key)) {
        $(input).removeClass("is-invalid").addClass("is-valid");
        padre.append("<div class='valid-feedback'>válido.</div>");
        return true;
    } else {
        $(input).removeClass("is-valid").addClass("is-invalid");
        padre.append("<div class='invalid-feedback'>inválido.</div>");
        return false;
    }
}

// ===================== EMAIL =====================
function validarEmail(input) {
    const regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    let padre = $(input).closest('div');
    padre.find(".valid-feedback, .invalid-feedback").remove();

    if (regex.test(input.value)) {
        $(input).removeClass("is-invalid").addClass("is-valid");
        padre.append("<div class='valid-feedback'>Email válido.</div>");
        return true;
    } else {
        $(input).removeClass("is-valid").addClass("is-invalid");
        padre.append("<div class='invalid-feedback'>Email inválido.</div>");
        return false;
    }
}

// ===================== PASSWORD =====================

function validarPassword(input) {
    const password = input.value;
    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@!%*?&])[A-Za-z\d$@!%*?&]{8,15}$/;

    // Obtener el contenedor padre del input
    const padre = $(input).closest('div');
    // Limpiar mensajes anteriores
    padre.find('.valid-feedback, .invalid-feedback').remove();

    if (regex.test(password)) {
        // Contraseña válida
        $(input).removeClass('is-invalid').addClass('is-valid');
        padre.append("<div class='valid-feedback'>Contraseña válida.</div>");
        return true;
    } else {
        // Contraseña inválida
        $(input).removeClass('is-valid').addClass('is-invalid');
        padre.append("<div class='invalid-feedback'>La contraseña debe tener 8-15 caracteres,($@!%*?&).</div>");
        return false;
    }
}

$('#confirm-password').on('input', function () {
    const password = $('#inptUsuarioPassword').val(); // corregido
    const confirm = $(this).val();
    const padre = $(this).closest('div');
    padre.find('.valid-feedback, .invalid-feedback').remove();

    if (confirm.length === 0) {
        $(this).removeClass('is-valid is-invalid');
        return;
    }

    if (confirm === password) {
        $(this).removeClass('is-invalid').addClass('is-valid');
        padre.append("<div class='valid-feedback'>¡Coincide!</div>");
    } else {
        $(this).removeClass('is-valid').addClass('is-invalid');
        padre.append("<div class='invalid-feedback'>No coincide con la contraseña.</div>");
    }
});

function mostrarConfirmPassword(input) {
    const password = input.value;
    if (password.length > 0) {
        $('#confirm-container').slideDown();
    } else {
        $('#confirm-container').slideUp();
        $('#confirm-password').val('').removeClass('is-valid is-invalid');
        $('#confirm-container .valid-feedback, #confirm-container .invalid-feedback').remove();
    }
}

// ===================== FECHA =====================
function validarFecha(input) {
    const regex = /^(\d{4})-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$/;
    let padre = $(input).closest("div");
    padre.find(".valid-feedback, .invalid-feedback").remove();

    if (regex.test(input.value)) {
        $(input).removeClass("is-invalid").addClass("is-valid");
        padre.append("<div class='valid-feedback'>Fecha válida.</div>");
        return true;
    } else {
        $(input).removeClass("is-valid").addClass("is-invalid");
        padre.append("<div class='invalid-feedback'>Fecha inválida.</div>");
        return false;
    }
}


// ===================== TELÉFONO =====================
function phonenumber(input) {
    const regex = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    let padre = $(input).closest("div");
    padre.find(".valid-feedback, .invalid-feedback").remove();

    if (regex.test(input.value)) {
        $(input).removeClass("is-invalid").addClass("is-valid");
        padre.append("<div class='valid-feedback'>Número válido.</div>");
        return true;
    } else {
        $(input).removeClass("is-valid").addClass("is-invalid");
        padre.append("<div class='invalid-feedback'>Número inválido.</div>");
        return false;
    }
}

// ===================== CURP =====================
function validarCURP(input) {
    input.value = input.value.toUpperCase();
    const regex = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/;
    let padre = $(input).closest("div");
    padre.find(".valid-feedback, .invalid-feedback").remove();

    if (regex.test(input.value)) {
        $(input).removeClass("is-invalid").addClass("is-valid");
        padre.append("<div class='valid-feedback'>CURP válida.</div>");
        return true;
    } else {
        $(input).removeClass("is-valid").addClass("is-invalid");
        padre.append("<div class='invalid-feedback'>CURP inválida.</div>");
        return false;
    }
}

// ===================== ALFANUMÉRICO =====================
function validarAlfanumerico(input) {
    const regex = /^[A-Za-z0-9\s]+$/;
    let padre = $(input).closest('div');
    padre.find(".valid-feedback, .invalid-feedback").remove();

    if (regex.test(input.value)) {
        $(input).removeClass("is-invalid").addClass("is-valid");
        padre.append("<div class='valid-feedback'>Looks good!</div>");
        return true;
    } else {
        $(input).removeClass("is-valid").addClass("is-invalid");
        padre.append("<div class='invalid-feedback'>No válido. Solo letras, números y espacios.</div>");
        return false;
    }
}

// ===================== DROPDOWN =====================
function validarDropdown(input) {
    const valor = $(input).val();
    let padre = $(input).parent();
    padre.find(".valid-feedback, .invalid-feedback").remove();

    if (!valor || valor === "" || valor.startsWith("Selecciona")) {
        $(input).removeClass("is-valid").addClass("is-invalid");
        padre.append("<div class='invalid-feedback'>Debe seleccionar una opción.</div>");
        return false;
    } else {
        $(input).removeClass("is-invalid").addClass("is-valid");
        padre.append("<div class='valid-feedback'>Correcto</div>");
        return true;
    }
}

// ===================== SEXO =====================
function validarSexo() {
    const select = $("#selctUsuarioSexo");
    const padre = select.parent();

    // Limpiar mensajes previos
    padre.find(".valid-feedback, .invalid-feedback").remove();
    select.removeClass("is-valid is-invalid");

    if (!select.val()) {
        select.addClass("is-invalid");
        padre.append("<div class='invalid-feedback'>Debe seleccionar un sexo.</div>");
        return false;
    } else {
        select.addClass("is-valid");
        padre.append("<div class='valid-feedback'>¡Correcto!</div>");
        return true;
    }
}
function validarDropdownEstatus() {
    const select = $("#selecUsuarioEstatus");
    const padre = $("#estatusGroup");

    // Limpiar mensajes previos
    padre.find(".valid-feedback, .invalid-feedback").remove();
    select.removeClass("is-valid is-invalid");

    if (!select.val() || select.val() === "") {
        select.addClass("is-invalid");
        padre.append("<div class='invalid-feedback'>Debe seleccionar un estatus.</div>");
        return false;
    } else {
        select.addClass("is-valid");
        padre.append("<div class='valid-feedback'>¡Correcto!</div>");
        return true;
    }
}


// ===================== VALIDACIÓN FINAL =====================


function ValidarPegado(event, input) {
    // Obtener texto pegado
    var textoPegado = (event.clipboardData || window.clipboardData).getData('text');

    // Regex solo permite letras y espacios (mayúsculas, minúsculas y acentos)
    var regex = /^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]*$/;

    if (!regex.test(textoPegado)) {
        event.preventDefault(); // bloquea el pegado
        alert("Solo se permiten letras y espacios.");
    }
}

//-------------------------------------------------------------------------------------------
