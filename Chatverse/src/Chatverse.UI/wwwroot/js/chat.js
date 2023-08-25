"use strict";

var storedToken = localStorage.getItem("JWToken");
var MyUserId = localStorage.getItem("MyIdentifier");
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
    

    document.querySelectorAll('.myMessageFriends').forEach(item => {
        item.addEventListener('click', e => {
            e.preventDefault();
            var targetElement = e.target;
            console.log(targetElement);
            const targetElementId = targetElement.getAttribute('id');
            console.log(targetElementId);
            $.ajax({
                type: 'GET',
                url: '/Messages/GetAllMessage/' + targetElementId,
                success: function (data) {
                    var divElement = document.querySelector('.messages-content');
                    var fromButton = document.querySelector('.chat-form');
                    divElement.innerHTML = '';

                    if (data != null) {
                        data.forEach(function (item) {
                            if (item.senderId == MyUserId) {
                                if (item.content != null && item.image != null) {
                                    var newElement = document.createElement('div');
                                    newElement.className = 'message-item outgoing-message';

                                    var content = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${item.senderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${item.senderUsername}</h5>
                        <div class="time">${item.sendDate}<i class="ti-double-check text-info"></i></div>
                    </div>
                </div>

                <div class="message-wrap myMessageContent">${item.content}</div>
                <br/>
                <figure>
    <img src="/${item.image}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
</figure>
            `;

                                    newElement.innerHTML = content;
                                    divElement.appendChild(newElement);
                                }
                                else if (item.content != null && item.image == null) {
                                    var newElement = document.createElement('div');
                                    newElement.className = 'message-item outgoing-message';

                                    var content = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${item.senderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${item.senderUsername}</h5>
                        <div class="time">${item.sendDate}<i class="ti-double-check text-info"></i></div>
                    </div>
                </div>

                <div class="message-wrap myMessageContent">${item.content}</div>
                
               
            `;

                                    newElement.innerHTML = content;
                                    divElement.appendChild(newElement);
                                }
                                else if (item.content == null && item.image != null) {
                                    var newElement = document.createElement('div');
                                    newElement.className = 'message-item outgoing-message';

                                    var content = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${item.senderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${item.senderUsername}</h5>
                        <div class="time">${item.sendDate}<i class="ti-double-check text-info"></i></div>
                    </div>
                </div>

                <br/>
                <figure>
    <img src="/${item.image}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
</figure>
            `;

                                    newElement.innerHTML = content;
                                    divElement.appendChild(newElement);
                                }
                              
                            } else {
                                if (item.content != null && item.image != null) {
                                    var receiverElement = document.createElement('div');
                                    receiverElement.className = 'message-item receiverMessage';

                                    var content = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${item.senderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${item.senderUsername}</h5>
                        <div class="time">${item.sendDate}</div>
                    </div>
                </div>
                <div class="message-wrap">${item.content}</div>
                 <br/>
                <figure>
    <img src="/${item.image}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
</figure>
            `;

                                    receiverElement.innerHTML = content;
                                    divElement.appendChild(receiverElement);
                                }
                                else if (item.content != null && item.image == null) {
                                    var receiverElement = document.createElement('div');
                                    receiverElement.className = 'message-item receiverMessage';

                                    var content = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${item.senderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${item.senderUsername}</h5>
                        <div class="time">${item.sendDate}</div>
                    </div>
                </div>
                <div class="message-wrap">${item.content}</div>
       
            `;

                                    receiverElement.innerHTML = content;
                                    divElement.appendChild(receiverElement);
                                }
                                else if (item.content == null && item.image != null) {
                                    var receiverElement = document.createElement('div');
                                    receiverElement.className = 'message-item receiverMessage';

                                    var content = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${item.senderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${item.senderUsername}</h5>
                        <div class="time">${item.sendDate}</div>
                    </div>
                </div>
                 <br/>
                <figure>
    <img src="/${item.image}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
</figure>
            `;

                                    receiverElement.innerHTML = content;
                                    divElement.appendChild(receiverElement);
                                }
                            }
                        });
                    }
                    var hiddenElement = document.createElement('div');
                    hiddenElement.style.display = 'none';
                    var hiddenContent = `<input type="hidden" class="receiverIdHiddeninput" value="${targetElementId}">`;
                    

                    hiddenElement.innerHTML = hiddenContent;
                    divElement.appendChild(hiddenElement);
                    fromButton.style.display = 'block';
                },
                error: function (error) {
                    // AJAX isteği başarısız olduğunda burası çalışır
                    console.error(error); // Hata mesajını konsolda gösterme, isteğe bağlı
                }
            })


        })
    })
    // Dosya seçme butonuna tıkladığınızda
    
    document.querySelectorAll('.sendMessage').forEach(item => {
        item.addEventListener('click', function (e) {
            e.preventDefault();

            var messagePart = document.getElementById('messageContent');
            var content = messagePart.value;

            var receiverInput = document.querySelectorAll(".receiverIdHiddeninput")[0];
            var receiverId = receiverInput.value; // receiverInput öğesinin id değeri alınır
            console.log(receiverId); // id değeri konsola yazdırılır
            var formData = new FormData();
            var imagePath;
            var imageFile = $('#messageImageInput')[0].files[0];
            formData.append('formFile', imageFile);
            
            
            
            $.ajax({
                type: 'POST',
                url: '/Messages/UploadImageToRoot',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    connection.invoke('SendMessageAsync', receiverId, content, response);
                },
                error: function () {
                    console.error('Ajax isteği başarısız.');
                }
            });
            

            messagePart.value = "";
            messagePart.focus();
            var imagePreview = document.getElementById('imagePreview');
            imagePreview.innerHTML = "";
            imagePreview.style.display = 'none';

        });
    })

    connection.on('seeSendMessage', (SenderUsername, SenderProfilePicture, hour, content, imagePath) => {
        var divElement = document.querySelector('.messages-content');
        var receiverElement = document.createElement('div');
        receiverElement.className = 'message-item receiverMessage';
        var content1;
        if (content != null && imagePath != null) {
            content1 = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${SenderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${SenderUsername}</h5>
                        <div class="time">${hour}</div>
                    </div>
                </div>
                <div class="message-wrap">${content}</div>
                <br/>
                <figure>
    <img src="/${imagePath}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
</figure>

            `;
        }
        else if (content != null && imagePath == null) {
            content1 = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${SenderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${SenderUsername}</h5>
                        <div class="time">${hour}</div>
                    </div>
                </div>
                <div class="message-wrap">${content}</div>
                
            `;
        }
        else if (content == null && imagePath != null) {
            content1 = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${SenderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${SenderUsername}</h5>
                        <div class="time">${hour}</div>
                    </div>
                </div>
                <br/>
                <figure>
    <img src="/${imagePath}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
</figure>
            `;
        }
       
        var audioElement2 = document.getElementById("receiveMessageAudio");
        audioElement2.currentTime = 0; // Sesi sıfırla (eğer zaten çalıyorsa)
        audioElement2.play(); // Sesi çal
        receiverElement.innerHTML = content1;
        divElement.appendChild(receiverElement);
    });

    connection.on('seeMySendMessage', (SenderUsername, SenderProfilePicture, content, hour, imagePath) => {
        var divElement = document.querySelector('.messages-content');
        var newElement = document.createElement('div');
        newElement.className = 'message-item outgoing-message';
        if (content != null && imagePath != null) {
            var content2 = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${SenderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${SenderUsername}</h5>
                        <div class="time">${hour}<i class="ti-double-check text-info"></i></div>
                    </div>
                </div>
                <div class="message-wrap myMessageContent">${content}</div>
               < br/>
                <figure>
    <img src="/${imagePath}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
</figure>
            `;
        }
        else if (content != null && imagePath == null) {
            var content2 = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${SenderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${SenderUsername}</h5>
                        <div class="time">${hour}<i class="ti-double-check text-info"></i></div>
                    </div>
                </div>
                <div class="message-wrap myMessageContent">${content}</div>
             
            `;
        }

        else if (content == null && imagePath != null) {
            var content2 = `
                <div class="message-user">
                    <figure class="avatar">
                        <img src="/${SenderProfilePicture}" alt="image">
                    </figure>
                    <div>
                        <h5>${SenderUsername}</h5>
                        <div class="time">${hour}<i class="ti-double-check text-info"></i></div>
                    </div>
                </div>
                <br/>
                <figure>
    <img src="/${imagePath}" class="img-fluid rounded-3" style="max-width: 400px; max-height: 400px;" alt="image">
</figure>
            `;
        }

        var audioElement1 = document.getElementById("sentMessageAudio");
            audioElement1.currentTime = 0; // Sesi sıfırla (eğer zaten çalıyorsa)
            audioElement1.play(); // Sesi çal
       
        newElement.innerHTML = content2;
        divElement.appendChild(newElement);
    });








   

    // Dosya seçme alanında değişiklik olduğunda





    window.addEventListener("unload", function (event) {
        connection.stop();
    });
    window.addEventListener("beforeunload", function (event) {
        connection.stop();
    });


}
