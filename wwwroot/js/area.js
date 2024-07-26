

document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.delete-button').forEach(button => {
        button.addEventListener('click', function (event) {
            event.preventDefault();
            const areaId = this.getAttribute('data-id');
            if (areaId) {
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
                        fetch(`/Area/Delete?id=${areaId}`, {
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
                                        text: 'Área eliminada correctamente.'
                                    }).then(() => {
                                        location.reload();
                                    });
                                } else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Error',
                                        text: data.errorMessage || 'Error al eliminar el área.'
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




//$(document).ready(function () {
//    $('.delete-button').click(function () {
//        var areaId = $(this).data('id');
//        var deleteUrl = $(this).data('delete-url');
//        if (confirm('¿Estás seguro de que deseas eliminar esta área?')) {
//            $.ajax({
//                url: deleteUrl,
//                type: 'POST',
//                data: { id: areaId },
//                success: function (result) {
//                    if (result.success) {
//                        location.reload();
//                    } else {
//                        alert('Error al eliminar el área: ' + result.message);
//                    }
//                },
//                error: function (xhr, status, error) {
//                    alert('Error al eliminar el área: ' + error);
//                }
//            });
//        }
//    });
//});

// --------------------------------------------------
//$(document).ready(function () {
//    $('.delete-button').click(function () {
//        var areaId = $(this).data('id');
//        if (confirm('¿Estás seguro de que deseas eliminar esta área?')) {
//            $.ajax({
//                url: '@Url.Action("Delete", "Area")',
//                type: 'POST',
//                data: { id: areaId },
//                success: function (result) {
//                    if (result.success) {
//                        location.reload();
//                    } else {
//                        alert('Error al eliminar el área: ' + result.message);
//                    }
//                },
//                error: function (xhr, status, error) {
//                    alert('Error al eliminar el área: ' + error);
//                }
//            });
//        }
//    });
//});

//********** /

//@* <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script> * @
//    @* <script>
//        $(document).ready(function () {
//            $('.delete-button').click(function () {
//                var areaId = $(this).data('id');
//                if (confirm('¿Estás seguro de que deseas eliminar esta área?')) {
//                    $.ajax({
//                        url: '@Url.Action("Delete", "Area")',
//                        type: 'POST',
//                        data: { id: areaId },
//                        success: function (result) {
//                            if (result.success) {
//                                location.reload();
//                            } else {
//                                alert('Error al eliminar el área: ' + result.message);
//                            }
//                        },
//                        error: function (xhr, status, error) {
//                            alert('Error al eliminar el área: ' + error);
//                        }
//                    });
//                }
//            });
//    });
//    </script>