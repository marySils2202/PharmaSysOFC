const btnGuardar = document.getElementById('btnGuardar');
const btnCancelar = document.getElementById('btnCancelar');

btnGuardar.addEventListener('click', guardarCompra);

function guardarCompra() {

    const tipoFactura = "Contado";
    const fechaVenta = document.getElementById('fecha').value;
    const idCliente = document.getElementById('ncliente').value;
    const idEmpleado = document.getElementById('nvendedor').value;
    const totalVenta = document.getElementById('total').value;
    const tipoPago = document.getElementById('tipoPago').value;
    const iva = document.getElementById('iva').value;
    const estado = "Activo";

    
   
             
                        "tipoPago": "Efectivo",
                            "iva": 100,
                                "estado": "Activo"

    const venta = {
        tipoFactura: tipoFactura,
        fechaVenta: fechaVenta,
        idCliente: idCliente,
        idEmpleado: idEmpleado,
        totalVenta: totalVenta,
        tipoPago: tipoPago,
        iva: iva,
        estado : estado


    };

    fetch('http://localhost:5121/api/Ventas/Guardar', {
        method: 'POST',
        headers: {

            'Content-Type': 'application/json',

        },
        body: JSON.stringify(venta)
    })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error(error));
}

