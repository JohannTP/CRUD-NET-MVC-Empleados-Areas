

document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.delete-button').forEach(button => {
        button.addEventListener('click', function (event) {
            event.preventDefault();
            const documentoId = this.getAttribute('data-id');
            if (documentoId) {
                Swal.fire({
                    title: '¿Estás seguro de eliminar el documento?',
                    text: 'No se podrá recuperar.',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Sí, eliminar'
                }).then((result) => {
                    if (result.isConfirmed) {
                        fetch(`/Attachment/Delete?id=${documentoId}`, {
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
                                        text: 'Documento eliminado correctamente.'
                                    }).then(() => {
                                        location.reload();
                                    });
                                } else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Error',
                                        text: data.errorMessage || 'Error al eliminar el Documento.'
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
                    text: 'No se pudo obtener el ID del área.'
                });
            }
        });
    });
});