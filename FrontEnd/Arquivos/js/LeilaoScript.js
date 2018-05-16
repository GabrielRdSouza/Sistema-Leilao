//Mascaras
$(function ($) {
    $("#cpfLogin").mask("999.999.999-99");
    $("#cpfCadastroUsuario").mask("999.999.999-99");
    $("#telefoneCadastroUsuario").mask("(99) 999999999");
    $("#precoCadastroProduto").mask("000.000.000.000.000,00", { reverse: true });
    $("#valorParticiparProduto").mask("000.000.000.000.000,00", { reverse: true });
    $("#maiorLanceParticiparProduto").mask("000.000.000.000.000,00", { reverse: true });
});

//Tabelas
$(function ($) {
    $('#tableProdutos').DataTable();
    $('#tableUsuarios').DataTable();
    $('#tableUltimosProdutos').DataTable();
});

//Funções
function BuscarCPF() {
    var cpf = $('#cpfCadastroUsuario').val();
    if (cpf.length == 14) {
        $.ajax({
            url: 'http://localhost:2677/Cadastro/ValidarCPF',
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                CPF: $('#cpfCadastroUsuario').val()
            }),
            success: function (result) {
                if (result) {
                    toastr.error('O CPF ja existe.', 'Erro!', {
                        "progressBar": true,
                        "closeButton": true,
                        "showMethod": "slideDown",
                        "hideMethod": "slideUp",
                        "timeOut": 5000
                    });
                }
            },
            error: function (response) {
                toastr.error('Houve um erro ao tentar validar seu CPF. Tente novamente mais tarde', 'Erro!', {
                    "progressBar": true,
                    "closeButton": true,
                    "showMethod": "slideDown",
                    "hideMethod": "slideUp",
                    "timeOut": 5000
                });
            }
        });
    }
    else {
        toastr.error('Seu CPF esta incompleto.', 'CPF invalido!', {
            "progressBar": true,
            "closeButton": true,
            "showMethod": "slideDown",
            "hideMethod": "slideUp",
            "timeOut": 5000
        });
    }
}

function BuscarTelefone() {
    if ($('#telefoneCadastroUsuario').val().length >= 14) {
        $.ajax({
            url: 'http://localhost:2677/Cadastro/ValidarTelefone',
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                Telefone: $('#telefoneCadastroUsuario').val()
            }),
            success: function (result) {
                if (result) {
                    toastr.error('Este numero de telefone ja esta cadastrado em outra conta.', 'Erro!', {
                        "progressBar": true,
                        "closeButton": true,
                        "showMethod": "slideDown",
                        "hideMethod": "slideUp",
                        "timeOut": 5000
                    });
                }
            },
            error: function (response) {
                toastr.error('Houve um erro ao tentar validar seu Telefone. Tente novamente mais tarde', 'Erro!', {
                    "progressBar": true,
                    "closeButton": true,
                    "showMethod": "slideDown",
                    "hideMethod": "slideUp",
                    "timeOut": 5000
                });
            }
        });
    }
}

function BuscarEmail() {
    $.ajax({
        url: 'http://localhost:2677/Cadastro/ValidarEmail',
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            Email: $('#emailCadastroUsuario').val()
        }),
        success: function (result) {
            if (result) {
                toastr.error('Este email ja esta cadastrado em outra conta.', 'Erro!', {
                    "progressBar": true,
                    "closeButton": true,
                    "showMethod": "slideDown",
                    "hideMethod": "slideUp",
                    "timeOut": 5000
                });
            }
        },
        error: function (response) {
            toastr.error('Houve um erro ao tentar validar seu Telefone. Tente novamente mais tarde', 'Erro!', {
                "progressBar": true,
                "closeButton": true,
                "showMethod": "slideDown",
                "hideMethod": "slideUp",
                "timeOut": 5000
            });
        }
    });
}

function ValidarCadastroUsuario() {
    var senha = $('#senha1').val(), confirma = $('#senha2').val();
    if (senha == confirma && senha.length >= 8 && senha.length <= 32 && confirma.length >= 8 && confirma.length <= 32) {
        $('#cadastrarUsuario').click();
    }
    else {
        ValidarSenha()
    }
}

function ValidarSenha() {
    var senha = $('#senha1').val(), confirma = $('#senha2').val();
    if (senha != confirma || senha.length < 8 || senha.length > 32) {
        toastr.warning('As duas senhas sao obrigatorias, precisam ser iguais e ter entre 8 e 32 caracteres.', 'Senha invalida!', {
            "progressBar": true,
            "closeButton": true,
            "showMethod": "slideDown",
            "hideMethod": "slideUp",
            "timeOut": 15000
        });
    }
}

function ValidarValor() {
    if ($('#valorParticiparProduto').val() <= $('#maiorLanceParticiparProduto').val()) {
        toastr.warning('O lance que voce esta tentando dar e menor ou igual ao maior lance ja feito nesse produto!', 'Erro!', {
            "progressBar": true,
            "closeButton": true,
            "showMethod": "slideDown",
            "hideMethod": "slideUp",
            "timeOut": 15000
        });
    }
}

function ValidarTudo() {
    ValidarSenha();
    BuscarCPF();
    BuscarEmail();
    BuscarTelefone();
}