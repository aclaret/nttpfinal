var carrito = new Array();
var total_venta;

$(document).ready(function () {
    $(document).on('submit', '#form-login', function (e) {
        e.preventDefault();
        finalizar_pedido("ingresar");
    });

    $(document).on('submit', '#form-registro', function (e) {
        e.preventDefault();
        finalizar_pedido("registrar");
    });
});

function mostrar_form_registro() {
    $('#form-login')[0].reset();
    $('#form-registro')[0].reset();
    $('#modal_form').modal('show');
}

function agregar_carrito(id_plato) {
    var nombre_plato = $('#nombre_' + id_plato).text();
    var precio_unidad = $('#precio_' + id_plato).val();
    var cantidad = $('#cantidad_' + id_plato).val();

    if (cantidad < 1 || cantidad > 100) {
        alert("Debe ingresar una cantidad válida");
        return false;
    }

    if (carrito[id_plato]) {
        //Agrego la cantidad del plato
        carrito[id_plato]['cantidad'] += parseInt(cantidad);
    } else {
        carrito[id_plato] = new Array();
        carrito[id_plato]['nombre_plato'] = nombre_plato;
        carrito[id_plato]['precio_unidad'] = parseFloat(precio_unidad);
        carrito[id_plato]['cantidad'] = parseInt(cantidad);
    }

    //Calculo el precio total del plato
    carrito[id_plato]['precio_total'] = carrito[id_plato]['cantidad'] * carrito[id_plato]['precio_unidad'];

    renderizar_carrito();
}

function calcular_total_pedido() {
    total_venta = 0;

    carrito.forEach(function (valores_plato, id_plato) {
        total_venta += parseFloat(valores_plato.precio_total);
    });
}

function renderizar_carrito() {
    calcular_total_pedido();

    //Pregunto si no esta vacío
    if (typeof carrito !== 'undefined' && carrito.length > 0) {
        var item;

        $('#items_carrito ul').html('');
        carrito.forEach(function (valores_plato, id_plato) {
            item = '<li><span class="float-left">' + valores_plato.cantidad + 'x ' + valores_plato.nombre_plato + ' </span><span class="float-right"> $' + valores_plato.precio_total + '</span></li>';
            $('#items_carrito ul').append(item);
        });

        $('#carrito_desktop').show();
        $('#total_pedido').html(' $' + total_venta);
    } else {
        $('#carrito_desktop').hide();
    }
}

function finalizar_pedido(accion){
    //var accion = $(btn_accion).val();
    var datos_pedido = {}

    if (accion == "ingresar") {
        datos_pedido["user_accion"] = "ingresar";
        var mensaje_accion = "¿Ingresar y confirmar pedido?";

        //Realizar validaciones javascript
        datos_pedido["username"] = $('#login_username').val();
        datos_pedido["password"] = $('#login_password').val();
    } else {
        datos_pedido['user_accion'] = "registrar";
        var mensaje_accion = "¿Registrarse y confirmar pedido?";

        //Realizar validaciones javascript
        datos_pedido['username'] = $('#username_registro').val();
        datos_pedido['password'] = $('#password_registro').val();

        //Busco todos los demás campos correspondientes al registro de usuario por el name
        datos_pedido['nombre_completo'] = $('[name="nombre_completo"]').val();
        datos_pedido['telefono'] = $('[name="telefono"]').val();
        datos_pedido['email'] = $('[name="email"]').val();
        datos_pedido['direccion'] = $('[name="direccion"]').val();
    }

    if (confirm(mensaje_accion)) {
        var platosPedido = "";

        carrito.forEach(function (valores_plato, id_plato) {
            platosPedido += id_plato + "|" + valores_plato.cantidad + ";";
        });

        if (!platosPedido.length) {
            alert("Error, no hay platos seleccionados");
            return false;
        }

        //Agrego los platos al json object
        datos_pedido['platosPedido'] = platosPedido;

        $.ajax({
            url: "/Home/AjaxGuardarPedido",
            type: "POST",
            data: datos_pedido,
            dataType: "json",
            success: function (response) {
                if (response.result == true) {
                    alert("Pedido realizado con exito!");
                    location.reload();
                } else {
                    alert(response.error);
                    return false;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var mensaje = "Ocurrió un error generando el pedido";
                if (jqXHR.responseText) {
                    mensaje = jqXHR.responseText;
                }
                if (mensaje != "") {
                    alert(mensaje);
                    return false;
                }
            }
        });
    }
}