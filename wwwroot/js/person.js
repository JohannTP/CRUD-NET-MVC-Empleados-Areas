
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.delete-button').forEach(button => {
        button.addEventListener('click', function (event) {
            event.preventDefault();
            const personId = this.getAttribute('data-id');
            if (personId) {
                Swal.fire({
                    title: '¿Estás seguro de eliminar el registro?',
                    text: 'No se podrá recuperar.',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Sí, eliminar'
                }).then((result) => {
                    if (result.isConfirmed) {
                        fetch(`/Person/Delete?id=${personId}`, {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        })
                            .then(response => response.json())
                            .then(data => {
                                if (data.success) {
                                    Swal.fire({
                                        icon: 'success',
                                        title: '¡Eliminado!',
                                        text: 'Empleado eliminado correctamente.'
                                    }).then(() => {
                                        location.reload();
                                    });
                                } else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Error',
                                        text: data.errorMessage || 'Error al eliminar el Empleado.'
                                    });
                                }
                            })
                            .catch(error => {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Error',
                                    text: 'Error al procesar la solicitud.'
                                });
                            });
                    }
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'No se pudo obtener el ID del Empleado.'
                });
            }
        });
    });
});


