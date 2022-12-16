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
        CloseSearch();
        $('.jogos-container').empty().append("<p>asdasdas</p>");
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
    console.log(document.getElementById("searchico"));
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