const btnGuardar = document.getElementById('btnGuardar');

btnGuardar.addEventListener('click',guardarCompra)  {
   
    //if (!validarFormulario()) {
    //    e.preventDefault();
    //    return;
    //}
    guardarCompra();
});

//function validarFormulario() {
  
//    const numeroVenta = document.getElementById('numeroVenta').value.trim();
//    const idProveedor = document.getElementById('inputproveedor').value.trim();
//    const telefono = document.getElementById('telefono').value.trim();
//    const fechaCompra = document.getElementById('fecha').value.trim();
//    const idProducto = document.getElementById('producto').value.trim();
//    const precioCompra = document.getElementById('precio').value.trim();
//    const cantidadCompra = document.getElementById('cantidad').value.trim();

    
//    if (!numeroVenta) {
//        alert("El número de compra es obligatorio.");
//        return false;
//    }

  
//    if (!idProveedor) {
//        alert("Debe seleccionar un proveedor.");
//        return false;
//    }

    
//    const telefonoRegex = /^[0-9]{8}$/; 
//    if (!telefonoRegex.test(telefono)) {
//        alert("El número de teléfono debe tener 8 dígitos.");
//        return false;
//    }

//    if (!fechaCompra) {
//        alert("Debe seleccionar una fecha.");
//        return false;
//    }

    
//    if (!idProducto) {
//        alert("Debe ingresar un producto.");
//        return false;
//    }

   
//    if (!precioCompra || isNaN(precioCompra) || parseFloat(precioCompra) <= 0) {
//        alert("El precio debe ser un número positivo.");
//        return false;
//    }

    
//    if (!cantidadCompra || isNaN(cantidadCompra) || parseInt(cantidadCompra) <= 0) {
//        alert("La cantidad debe ser un número positivo.");
//        return false;
//    }

    
//    return true;
//}

function guardarCompra() {
    const idProveedor = document.getElementById('inputproveedor').value;
    const fechaCompra = document.getElementById('fecha').value;
    const totalCompra = document.getElementById('total').value;

    const compra = {
        idProveedor: idProveedor,
        fechaCompra: fechaCompra,
        totalCompra: totalCompra
    };

   
    fetch('http://localhost:5121/api/Compras/Guardar', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(compra)
    })
        .then(response => response.json())
        .then(data => {
            console.log("Compra guardada", data);

            const idCompra = data.idCompra;

            guardarDetalles(idCompra);
        })
        .catch(error => console.error('Error al guardar la compra:', error));
}
function cargarProductos() {
    fetch('http://localhost:5121/api/Productos/Lista/Lista')
        .then(response => response.json())
        .then(data => {
            if (data.mensaje === "OK") {
                let productos = data.productos;
                let comboBox = document.getElementById('producto');
          

               
                productos.forEach(producto => {
                    let option = document.createElement('option');
                    option.value = producto.IdProducto;
                    option.text = producto.Nombre;
                    comboBox.appendChild(option);
                });
            } else {
                console.error('Error al cargar los productos:', data.mensaje);
            }
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}


window.onload = function () {
    cargarProductos();
};

function guardarDetalles(idCompra) {

    const idproducto = document.getElementById('producto').value;
    const cantidadCompra = document.getElementById('cantidad').value;
    const precioCompra = document.getElementById('precio').value;
    const subtotalCompra = cantidadCompra * precioCompra;

    const detallesCompra = [
        {
            idProducto: idproducto,
            CantidadCompra: cantidadCompra,
            PrecioCompra: precioCompra,
            SubtotalCompra: subtotalCompra
        },

    ];

    detallesCompra.forEach(detalle => {
        detalle.idCompra = idCompra;

        fetch('http://localhost:5121/api/CompraDetalle/Guardar', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(detalle)
        })
            .then(response => response.json())
            .then(data => console.log('Detalle guardado:', data))
            .catch(error => console.error('Error al guardar el detalle:', error));
    });
}

document.addEventListener("DOMContentLoaded", function () {
   
    fetch('http://localhost:5121/api/Compras/ObtenerUltimoIdCompra')
        .then(response => response.json())
        .then(data => {
            const idCompra = data.idCompra;

            document.getElementById('numeroVenta').value = idCompra;
        })
        .catch(error => console.error('Error al obtener el IdCompra:', error));
});
