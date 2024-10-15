const btnGuardar = document.getElementById('btnGuardar');
const btnCancelar = document.getElementById('btnCancelar');

btnGuardar.addEventListener('click', guardarCredito);

function guardarCredito() {
    const plazo = 1;
    const montoTotal = document.getElementById('total').value;
    const estado = "Pendiente"

    const credito = {
        plazo: plazo,
        montoTotal: montoTotal,
        estado: estado
    };

    fetch('http://localhost:5121/api/Credito/Guardar', {
        method: 'POST',
        headers: {

            'Content-Type': 'application/json',

        },
        body: JSON.stringify(credito)
    })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error(error));
}

