// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//#region Search Bar - ESC and Enter

$(document).on("keydown", function (e) {
    if (e.which == 27) {
        $(".searchbar").val("");
        CloseSearch();
    }
});

$(".searchbar").on("keydown", function (e) {
    if (e.which == 13) {
        setCookie("blockinfinitescroll", 1, 1);
        CloseSearch();
        $('.jogos-container').empty();
        let mydata = { nome: $('#searchbar').val(), cat: $('#Categorias').val(), plat: $('#Plataforma').val(), ordenar: $('#ordenar').prop('selectedIndex'), ordem: $('#ordem').prop('selectedIndex') }
        $.ajax({ url: '/Home/Pesquisa', data: mydata }).done(function (data) {
            if (data.length == 0) {
                $('.jogos-container').append(`<h3 class="text-white p-4 pt-0 pb-2">Não foi encontrado nenhum jogo que satisfaz os seus critérios!</h3>`);
            }
            data.forEach(jogo =>
                $('.jogos-container').append(`
                    <!--TEMPLATE-->
                <div class="gamecard col-sm-4 col-md-2 m-3 p-3 bg-dark text-white">
                        <a asp-controller="Home" asp-action="Jogo" asp-route-id="${jogo.id}">
                        <span class="gamecard-link"></span>
                    </a>
                    <div class="d-flex justify-content-center align-items-center">
                        <img class="mb-2 img-fluid game-image" src="/img/Jogos/${jogo.foto}">
			</div>
                        <p class="game-name m-1">${jogo.nome}</p>
                        <div class="d-flex">
                            <span class="badge rounded-pill bg-danger m-1">${jogo.idCategoriaNavigation.nome}</span>
                        </div>
                        <div class="d-flex">
                            <span class="badge rounded-pill bg-primary m-1">${jogo.idPlataformaNavigation.nome}</span>
                        </div>
                        <div class="d-flex">
                            <span class="badge rounded-pill bg-warning m-1">${jogo.idDesenvolvedoraNavigation.nome}</span>
                        </div>
                        <p class="game-price m-1">${jogo.preco.toFixed(2)} €</p>
                    </div>
                    <!-- FIM TEMPLATE-->
                `)
            );
        });

    }
});

function CloseSearch() {
    let hidden = document.getElementsByClassName("unhideme");
    if (document.getElementById("searchbar") === null) return;
    document.getElementById("searchico").style.stroke = "#808080";
    document.getElementById("searchico").style.zIndex = 3;
    document.getElementById("searchbar").style.removeProperty("box-shadow");
    document.getElementById("searchbar").style.opacity = "0.5";
    document.getElementById("searchbar").style.zIndex = 2;
    document.activeElement.blur();
    for (i = 0; i < hidden.length; i++)
        hidden.item(i).setAttribute("hidden", "true");
}

function onFocusSearchBar() {
    let hidden = document.getElementsByClassName("unhideme");
    document.getElementById("searchico").style.stroke = "#808080";
    document.getElementById("searchico").style.zIndex = 3;
    document.getElementById("searchbar").style.boxShadow = "0 0 0 9999px #000000";
    document.getElementById("searchbar").style.opacity = "0.75";
    document.getElementById("searchbar").style.zIndex = 2;
    for (i = 0; i < hidden.length; i++)
        hidden.item(i).removeAttribute("hidden");

}
//#endregion

//#region AddFuncionario
function onSubmitNewFunc() {
    var field1 = document.getElementById('UserFunc').value;
    var field2 = document.getElementById('TipoFunc').options[document.getElementById('TipoFunc').selectedIndex].text;

    return confirm("Tem a certeza que quer adicionar o user '" + field1 + "' como '" + field2 + "'?");
}

$('#TipoFunc').on('change', function () {
    if (this.value == 1) {
        $('.hideme').show();
    } else if (this.value == 2) {
        $('.hideme').hide();
    }
});
//#endregion

//#region Star rating

$(".feather-star").mouseover(function (e) {
    $(".feather-star").attr("fill", "none");
    $(this).attr("fill", "#FDCC0D50");
    $(this).prevAll(".feather-star").attr("fill", "#FDCC0D50");
});

$(".feather-star").click(function (e) {
    $(".feather-star").attr("fill", "none");
    $(this).attr("fill", "#FDCC0D");
    $(this).prevAll(".feather-star").attr("fill", "#FDCC0D");
    $(".feather-star").unbind('mouseover');
    var rating = $(this).attr("data-value");

    $("#rating").val(rating);

    console.log(rating);
    console.log($("#rating").val());

});
//#endregion


function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function MailNotifyChanged(e) {
    let mydata = { id: 0 }

    $.ajax({ type: "POST", url: '/Home/Settings', data: mydata }).done(function (data) {
        setTimeout(() => { window.location.reload(); }, 250);
    });

}

