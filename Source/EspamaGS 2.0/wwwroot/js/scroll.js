$(window).on("load",
    function (e) {
        setCookie("blockinfinitescroll", 0, 1);
        setCookie("page", 0, 1);
    });

$(window).scroll(function () {
    if (getCookie("blockinfinitescroll") != 1) {
        {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                let mydata = { page: parseInt(getCookie("page")) }
                // Carregar mais conteúdo quando o usuário chegar ao final da página
                $.ajax({ url: '/Home/Scroll', data: mydata }).done(function(data) {
                    setCookie("page", parseInt(getCookie("page")) + 1, 1);
                    if (data.length != 0) {
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
                            <span class="badge rounded-pill bg-warning m-1">${jogo.idDesenvolvedoraNavigation.nome
                                }</span>
                        </div>
                        <p class="game-price m-1">${jogo.preco.toFixed(2)} €</p>
                    </div>
                    <!-- FIM TEMPLATE-->
                `)
                        );
                    } else {
                        setCookie("blockinfinitescroll", 1, 1);

                    }
                });
            
        }
        }
    }
});