"use strict";

var storedToken = localStorage.getItem("JWToken");

if (storedToken) {
    var headers = {
        Authorization: "Bearer " + storedToken
    };

    var connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5273/chatHub", { accessTokenFactory: () => storedToken })
        .build();

    connection.start()
        .then(function () {
            console.log("Hub connection started.");
        })
        .catch(function (err) {
            return console.error(err.toString());
        });
    connection.on("seeOnlineFriend", (UserName, ProfilePicture, ConnectionId) => {
        // Yeni li öğesi için gerekli HTML kodunu oluşturun
        console.log(ProfilePicture);
        var newLiHTML = `
    <li id="${ConnectionId}" class="bg-transparent list-group-item no-icon pe-0 ps-0 pt-2 pb-2 border-0 d-flex align-items-center">
        <figure class="avatar float-left mb-0 me-2">
            <img src="/${ProfilePicture}" alt="image" class="w35">
        </figure>
        <h3 class="fw-700 mb-0 mt-0">
            <a class="font-xssss text-grey-600 d-block text-dark model-popup-chat" href="#">${UserName}</a>
        </h3>
        <span class="bg-success ms-auto btn-round-xss"></span>
    </li>
`;

        // Eklenecek ul elementini seçin (örneğin ul elementi)
        var ulElement = document.getElementById('onlineUserList');

        // Yeni li öğesini ul elementinin içine ekleyin
        ulElement.insertAdjacentHTML('beforeend', newLiHTML);

    });
    connection.on("seeMyOnlineFriend", (UserName, ProfilePicture, ConnectionId) => {
        // Yeni li öğesi için gerekli HTML kodunu oluşturun
        console.log(ProfilePicture);
        var newLiHTML = `
    <li id="${ConnectionId}" class="bg-transparent list-group-item no-icon pe-0 ps-0 pt-2 pb-2 border-0 d-flex align-items-center">
        <figure class="avatar float-left mb-0 me-2">
            <img src="/${ProfilePicture}" alt="image" class="w35">
        </figure>
        <h3 class="fw-700 mb-0 mt-0">
            <a class="font-xssss text-grey-600 d-block text-dark model-popup-chat" href="#">${UserName}</a>
        </h3>
        <span class="bg-success ms-auto btn-round-xss"></span>
    </li>
`;

        // Eklenecek ul elementini seçin (örneğin ul elementi)
        var ulElement = document.getElementById('onlineUserList');

        // Yeni li öğesini ul elementinin içine ekleyin
        ulElement.insertAdjacentHTML('beforeend', newLiHTML);

    });
    connection.on("deleteOnlineUser", ConnectionId => {
        console.log(ConnectionId);
        var getliElement = document.getElementById('' + ConnectionId);
        console.log(getliElement);
        var ulElement = document.getElementById('onlineUserList');
        ulElement.removeChild(getliElement);
    });



    window.addEventListener("unload", function (event) {
        connection.stop();
    });
    window.addEventListener("beforeunload", function (event) {
        connection.stop();
    });


}
