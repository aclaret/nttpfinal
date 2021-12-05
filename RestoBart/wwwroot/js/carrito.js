var carrito = new Array();
var total_venta;

//MODAL:

// Get the modal
var modal = document.getElementById("myModal");

// Get the button that opens the modal
var btn = document.getElementById("myBtn");

// Get the <span> element that closes the modal
var span = document.getElementById("cerrar");

// When the user clicks on the button, open the modal
btn.onclick = function () {
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
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

    console.log("El total de venta es:" + total_venta);
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
        $('#total_pedido').html('$' + total_venta);
    } else {
        $('#carrito_desktop').hide();
    }
}

function finalizar_pedido(btn_accion) {
    //Detengo el default action submit del formulario
    event.preventDefault();

    var accion = $(btn_accion).val();
    datos_pedido = {};

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
            success: function (data) {
                console.log("Pedido realizado con éxito");
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var mensaje = "Ocurrió un error generando el pedido";
                if (jqXHR.responseText) {
                    mensaje = jqXHR.responseText;
                }
                if (mensaje != "") {
                    alert(mensaje);
                }
            }
        });
    }
}