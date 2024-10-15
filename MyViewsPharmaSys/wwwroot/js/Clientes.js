const btnGuardar = document.getElementById('btnGuardar');
const btnEditar = document.getElementById('btneditar');
const btnEliminar = document.getElementById('btneliminar');

btnGuardar.addEventListener('click', guardarCliente);

function guardarCliente() {

    const nombre = document.getElementById('inputnombre').value;
    const cedula = document.getElementById('inputcedula').value;
    const telefono = document.getElementById('inputtelefono').value;
    const direccion = document.getElementById('inputdireccion').value;
    const tipocliente = document.getElementById('cbcliente').value;

    const cliente = {
        NombreCliente: nombre,
        CedulaCliente: cedula,
        TelefonoCliente: telefono,
        DireccionCliente: direccion,
        TipoCliente: tipocliente
    };

    fetch('http://localhost:5121/api/Clientes/Guardar', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(cliente)
    })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error(error));
}
//function editarCliente() {
//    const nombre = document.getElementById('inputnombre').value;
//    const cedula = document.getElementById('inputcedula').value;
//    const telefono = document.getElementById('inputtelefono').value;
//    const direccion = document.getElementById('inputdireccion').value;
//    const tipoCliente = document.getElementById('cbcliente').value;

//    const cliente = {
//        nombre: nombre,
//        cedula: cedula,
//        telefono: telefono,
//        direccion: direccion,
//        tipoCliente: tipoCliente
//    };

//    fetch('http://localhost:5121/api/Clientes/Editar', {
//        method: 'PUT',
//        headers: {

//            'Content-Type': 'application/json',

//        },
//        body: JSON.stringify(compra)
//    })
//        .then(response => response.json())
//        .then(data => console.log(data))
//        .catch(error => console.error(error));
//}