const btnGuardar = document.getElementById('btnGuardar');
const btnCancelar = document.getElementById('btnCancelar');

btnGuardar.addEventListener('click', guardarProducto);

function guardarProducto() {
   
    const nombre = document.getElementById('inputnombre').value;
    const stock = document.getElementById('inputstock').value;
    const idcategoria = document.getElementById('inputcategoria').value;
    const idmarca = document.getElementById('inputmarca').value;
    const precio = document.getElementById('inputprecio').value;
    const descripcion = document.getElementById('inputdescripcion').value;
  
    const producto = {
        nombre: nombre,
        stock : stock,
        idcategoria: idcategoria,
        idmarca: idmarca,
        precio: precio,
        descripcion : descripcion
    };

    fetch('http://localhost:5121/api/Productos/Guardar/Guardar', {
        method: 'POST',
        headers: {

            'Content-Type': 'application/json',

        },
        body: JSON.stringify(producto)
    })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error(error));
}

