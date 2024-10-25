(function () {
    $('#login-form').on('submit', function (event) {
        event.preventDefault();

        var apiUrl = '/api/Contas/Login';
        var data = { Email: $("#email").val(), Senha: $("#senha").val() };

        $.ajax({
            url: apiUrl,
            contentType: 'application/json',
            data: JSON.stringify(data),
            type: 'POST',
            dataType: 'json',
            success: function (data) {
                console.log(data)
                location.href = "/";
            },
            error: function (error) {
                alert(error.message)
            }
        });
    });
})();