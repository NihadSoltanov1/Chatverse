"use strict";

var storedToken = localStorage.getItem("JWToken");
var MyUserId = localStorage.getItem("MyIdentifier");
if (storedToken) {
    var headers = {
        Authorization: "Bearer " + storedToken
    };

    var connection = new signalR.HubConnectionBuilder()
        .withUrl("https://chatverseapi20230909133203.azurewebsites.net/chatHub", { accessTokenFactory: () => storedToken })
        .build();

    connection.start()
        .then(function () {
            console.log("Hub connection started.");
        })
        .catch(function (err) {
            return console.error(err.toString());
        });
    connection.on("seeOnlineFriend", (UserName, ProfilePicture, ConnectionId) => {
        console.log(ProfilePicture);

        var newLiHTML = `
                  <div id="${ConnectionId}" class="owl-item active" style="width: 71px; margin-right: 16px;">
                          <div class="item">
                                 <a href="#" class="user-status-box">
                                       <div class="avatar-xs mx-auto d-block chat-user-img online">
                                              <img src="/${ProfilePicture}" class="img-fluid rounded-circle">
                                             <span class="user-status"></span>
                                        </div>

                                         <h5 class="font-size-13 text-truncate mt-3 mb-1">${UserName}</h5>
                                 </a>
                         </div>
                  </div>
`;

        // Eklenecek ul elementini seçin (örneğin ul elementi)
        var ulElement = document.getElementById('onlineUserList');

        // Yeni li öğesini ul elementinin içine ekleyin
        ulElement.insertAdjacentHTML('beforeend', newLiHTML);

    });
    connection.on("seeMyOnlineFriend", (UserName, ProfilePicture, ConnectionId) => {
        console.log(ProfilePicture);
        var newLiHTML = `
                  <div id="${ConnectionId}" class="owl-item active" style="width: 71px; margin-right: 16px;">
                          <div class="item">
                                 <a href="#" class="user-status-box">
                                       <div class="avatar-xs mx-auto d-block chat-user-img online">
                                              <img src="/${ProfilePicture}" class="img-fluid rounded-circle">
                                             <span class="user-status"></span>
                                        </div>

                                         <h5 class="font-size-13 text-truncate mt-3 mb-1">${UserName}</h5>
                                 </a>
                         </div>
                  </div>
`;


        var ulElement = document.getElementById('onlineUserList');

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
            if (targetElement.tagName === 'LI') {
                targetElement = $(targetElement).closest('a')[0];
            }
            if (targetElement.tagName === 'DIV') {
                targetElement = $(targetElement).closest('a')[0];
            }

            if (targetElement.tagName === 'IMG') {
                targetElement = $(targetElement).closest('a')[0];
            }

            if (targetElement.tagName === 'H5') {
                targetElement = $(targetElement).closest('a')[0];
            }

            if (targetElement.tagName === 'P') {
                targetElement = $(targetElement).closest('a')[0];
            }


            var imgTag = targetElement.querySelector('img');
            console.log(imgTag)
            var srcValue = imgTag.getAttribute('src');
            console.log(srcValue)
            var h5Tag = targetElement.querySelector('h5');

            var username = h5Tag.textContent;

            const targetElementId = targetElement.getAttribute('id');
            console.log(targetElementId);
            $.ajax({
                type: 'GET',
                url: '/Messages/GetAllMessage/' + targetElementId,
                success: function (data) {
                    var divElement = document.querySelector('.messages-content');
                    var fromButton = document.querySelector('.chat-form');
                    divElement.innerHTML = '';

                    var chatForm = `
                   <div class="p-3 p-lg-4 border-bottom user-chat-topbar">
            <div class="row align-items-center">
                <div class="col-sm-4 col-8">
                    <div class="d-flex align-items-center">
                        <div class="d-block d-lg-none me-2 ms-0">
                            <a href="javascript: void(0);" class="user-chat-remove text-muted font-size-16 p-2"><i class="ri-arrow-left-s-line"></i></a>
                        </div>
                        <div class="me-3 ms-0">
                            <img src="${srcValue}" class="rounded-circle avatar-xs" alt="">
                        </div>
                        <div class="flex-grow-1 overflow-hidden">
                            <h5 class="font-size-16 mb-0 text-truncate"><a href="#" class="text-reset user-profile-show">${username}</a> <i class="ri-record-circle-fill font-size-10 text-success d-inline-block ms-1"></i></h5>
                        </div>
                    </div>
                </div>
                <div class="col-sm-8 col-4">
                    <ul class="list-inline user-chat-nav text-end mb-0">
                        <li class="list-inline-item">
                            <div class="dropdown">
                                <button class="btn nav-btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="ri-search-line"></i>
                                </button>
                                <div class="dropdown-menu p-0 dropdown-menu-end dropdown-menu-md">
                                    <div class="search-box p-2">
                                        <input type="text" class="form-control bg-light border-0" placeholder="Search..">
                                    </div>
                                </div>
                            </div>
                        </li>

                        <li class="list-inline-item d-none d-lg-inline-block me-2 ms-0">
                            <button type="button" class="btn nav-btn" data-bs-toggle="modal" data-bs-target="#audiocallModal">
                                <i class="ri-phone-line"></i>
                            </button>
                        </li>

                        <li class="list-inline-item d-none d-lg-inline-block me-2 ms-0">
                            <button type="button" class="btn nav-btn" id="openVideoCallModal">
                                <i class="ri-vidicon-line"></i>
                            </button>
                        </li>

                        <li class="list-inline-item d-none d-lg-inline-block me-2 ms-0">
                            <button type="button" class="btn nav-btn user-profile-show">
                                <i class="ri-user-2-line"></i>
                            </button>
                        </li>

                        <li class="list-inline-item">
                            <div class="dropdown">
                                <button class="btn nav-btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="ri-more-fill"></i>
                                </button>
                                <div class="dropdown-menu dropdown-menu-end">
                                    <a class="dropdown-item d-block d-lg-none user-profile-show" href="#">View profile <i class="ri-user-2-line float-end text-muted"></i></a>
                                    <a class="dropdown-item d-block d-lg-none" href="#" data-bs-toggle="modal" data-bs-target="#audiocallModal">Audio <i class="ri-phone-line float-end text-muted"></i></a>
                                    <a class="dropdown-item d-block d-lg-none" href="#" data-bs-toggle="modal" data-bs-target="#videocallModal">Video <i class="ri-vidicon-line float-end text-muted"></i></a>
                                    <a class="dropdown-item" href="#">Archive <i class="ri-archive-line float-end text-muted"></i></a>
                                    <a class="dropdown-item" href="#">Muted <i class="ri-volume-mute-line float-end text-muted"></i></a>
                                    <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <!-- end chat user head -->
        <!-- start chat conversation -->
        <div class="chat-conversation p-3 p-lg-4" data-simplebar="init">
            <ul class="list-unstyled mb-0 mainMessageContentList">



              <!-- MAIN MESSAGE Content --!>
               
              <li id="customTyping" style="display: none">
                                    <div class="conversation-list">
                                        <div class="chat-avatar">
                                            <img src="${srcValue}" alt="">
                                        </div>
                                        
                                        <div class="user-chat-content">
                                            <div class="ctext-wrap">
                                                <div class="ctext-wrap-content">
                                                    <p class="mb-0">
                                                        typing
                                                        <span class="animate-typing">
                                                            <span class="dot"></span>
                                                            <span class="dot"></span>
                                                            <span class="dot"></span>
                                                        </span>
                                                    </p>
                                                </div>
                                            </div>

                                            <div class="conversation-name">${username}</div>
                                        </div>
                                        
                                    </div>
                                </li>
            </ul>
        </div>
        <!-- end chat conversation end -->
        <!-- start chat input section -->
        <div class="chat-input-section p-3 p-lg-4 border-top mb-0">
        <input type="hidden" class="receiverIdHiddeninput" value="${targetElementId}">
            <div class="row g-0">

                <div class="col">
                    <input type="text" id="messageContent" class="form-control form-control-lg bg-light border-light" placeholder="Enter Message...">
                </div>
                <div class="col-auto">
                    <div class="chat-input-links ms-md-2 me-md-0">
                        <ul class="list-inline mb-0">
                            <li class="list-inline-item" data-bs-toggle="tooltip" data-bs-placement="top" title="Emoji">
                                <button type="button" class="btn btn-link text-decoration-none font-size-16 btn-lg waves-effect">
                                    <i class="ri-emotion-happy-line"></i>
                                </button>
                            </li>
                            <li class="list-inline-item" data-bs-toggle="tooltip" data-bs-placement="top" title="Attached File">
                                <button type="button" class="btn btn-link text-decoration-none font-size-16 btn-lg waves-effect">
                                    <i class="ri-attachment-line"></i>
                                </button>
                            </li>
                            <li class="list-inline-item">
                                <button id="sendMessage"  class="btn btn-primary font-size-16 btn-lg waves-effect waves-light">
                                    <i class="ri-send-plane-2-fill"></i>
                                </button>
                            </li>
                        </ul>
                    </div>

                </div>
            </div>
            
        </div>

                `;
                    divElement.insertAdjacentHTML('beforeend', chatForm);

                    var messageContentUlElement = document.querySelector('.mainMessageContentList');
                 
                    if (data != null) {
                        data.forEach(function (item) {
                            if (item.senderId == MyUserId) {
                                if (item.content != null && item.image != null) {
                                    var newMessageLi = document.createElement("li");
                                    newMessageLi.className = "right";
                                    newMessageLi.innerHTML = `
        
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${item.senderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            <p class="mb-0">
                                ${item.content}
                            </p>
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${item.sendDate}</span></p>
                             <br/>
                    <figure>
        <img src="/${item.image}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
    </figure>
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${item.senderUsername}</div>
                </div>
            </div>
      
    `;
                                    var typingLi = document.getElementById("customTyping");
                                    messageContentUlElement.insertBefore(newMessageLi, typingLi);


                                }
                                else if (item.content != null && item.image == null) {

                                    var newMessageLi = document.createElement("li");
                                    newMessageLi.className = "right";
                                    newMessageLi.innerHTML = `
       
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${item.senderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            <p class="mb-0">
                                ${item.content}
                            </p>
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${item.sendDate}</span></p>
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${item.senderUsername}</div>
                </div>
            </div>
       
    `;

                                    var typingLi = document.getElementById("customTyping");
                                    messageContentUlElement.insertBefore(newMessageLi, typingLi);

                                }
                                else if (item.content == null && item.image != null) {

                                    var newMessageLi = document.createElement("li");
                                    newMessageLi.className = "right";
                                    newMessageLi.innerHTML = `
        
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${item.senderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            <p class="mb-0">
                                ${item.content}
                            </p>
                             <br/>
                    <figure>
        <img src="/${item.image}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
    </figure>
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${item.sendDate}</span></p>
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${item.senderUsername}</div>
                </div>
            </div>
      
    `;

                                    var typingLi = document.getElementById("customTyping");
                                    messageContentUlElement.insertBefore(newMessageLi, typingLi);

                                }

                            } else {
                                if (item.content != null && item.image != null) {
                                    var newMessageLi = document.createElement("li");
                                    
                                    newMessageLi.innerHTML = `
       
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${item.senderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            <p class="mb-0">
                                ${item.content}
                            </p>
                             <br/>
                    <figure>
        <img src="/${item.image}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
    </figure>
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${item.sendDate}</span></p>
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${item.senderUsername}</div>
                </div>
            </div>
        
    `;

                                    var typingLi = document.getElementById("customTyping");
                                    messageContentUlElement.insertBefore(newMessageLi, typingLi);

                                }
                                else if (item.content != null && item.image == null) {
                                    var newMessageLi = document.createElement("li");

                                    newMessageLi.innerHTML = `
      
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${item.senderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            <p class="mb-0">
                                ${item.content}
                            </p>
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${item.sendDate}</span></p>
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${item.senderUsername}</div>
                </div>
            </div>
     
    `;

                                    var typingLi = document.getElementById("customTyping");
                                    messageContentUlElement.insertBefore(newMessageLi, typingLi);

                                }
                                else if (item.content == null && item.image != null) {
                                    var newMessageLi = document.createElement("li");

                                    newMessageLi.innerHTML = `
     
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${item.senderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            <p class="mb-0">
                                ${item.content}
                            </p>
                             <br/>
                    <figure>
        <img src="/${item.image}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
    </figure>
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${item.sendDate}</span></p>
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${item.senderUsername}</div>
                </div>
            </div>
     
    `;

                                    var typingLi = document.getElementById("customTyping");
                                    messageContentUlElement.insertBefore(newMessageLi, typingLi);

                                }
                            }
                        });
                    }
                    //var hiddenElement = document.createElement('div');
                    //hiddenElement.style.display = 'none';
                    //var hiddenContent = `<input type="hidden" class="receiverIdHiddeninput" value="${targetElementId}">`;


                    //hiddenElement.innerHTML = hiddenContent;
                    //divElement.appendChild(hiddenElement);
                    //fromButton.style.display = 'block';
                    setTimeout(MyButtonEventListener, 1000)
                    setTimeout(ShowTyping, 1001)
                    setTimeout(ShowVideoCallModal(srcValue, username), 1002)
                },
                error: function (error) {
                    console.error(error);
                }
            })


        })
    })

    function DeclineVideoCalling(username) {
        var declineButton = document.getElementById('cancel-call');
        declineButton.addEventListener('click', function () {
            var CallingPopupDiv = document.getElementById('customVideoModal');
            var customMessageContentDiv1 = document.querySelector('.customMessageContent');
            customMessageContentDiv1.removeChild(CallingPopupDiv);
            var audioElement2 = document.getElementById("callingMessageAudio");
            audioElement2.pause();
            audioElement2.currentTime = 0;
            connection.invoke("DeclineVideoCalling", username);
        });
    }
    let waitingToAnswer = false;
    function ShowVideoCallModal(image,username) {
        var showPopupButton = document.getElementById("openVideoCallModal");
        var customMessageContentDiv = document.querySelector('.customMessageContent');
        showPopupButton.addEventListener("click", function () {
            console.log('video call basladildi')
            var callingDiv = document.createElement('div');
            callingDiv.id = 'customVideoModal';
            callingDiv.className = 'popup';
            var callingDivContent = `<div class="popup-content">
        <div class="circle-image" id="callingImage">
            <img src="${image}" />
        </div>
        <br />
        <div class="calling-animation"></div>
        <p>${username}</p>
        <button id="cancel-call" class="circular-button">
            <span class="mdi mdi-phone" style="color: white; font-size: 28px;"></span>
        </button>
      
    </div>`;
            callingDiv.innerHTML = callingDivContent;
            customMessageContentDiv.appendChild(callingDiv);
            
            
            waitingToAnswer = true;
            setTimeout(function () {
                var audioElement2 = document.getElementById("callingMessageAudio");
                audioElement2.currentTime = 0; // Sesi sıfırla (eğer zaten çalıyorsa)
                audioElement2.play();
                audioElement2.addEventListener('ended', function () {
                    this.currentTime = 2;
                    this.play();
                });

                setTimeout(function () {
                    connection.invoke('CallingFriend', username)
                    console.log("Calling friend invoke")
                }, 1000);

                DeclineVideoCalling(username);
            }, 1500);

            setTimeout(function () {
                
                
                var CallingPopupDiv = document.getElementById('customVideoModal');
                console.log("CallingPopupDiv: " + CallingPopupDiv);
                var customMessageContentDiv1 = document.querySelector('.customMessageContent');
                console.log("CustomMessafgeContentDiv: " + customMessageContentDiv1)       
                customMessageContentDiv1.removeChild(CallingPopupDiv);
                    var audioElement2 = document.getElementById("callingMessageAudio");
                    audioElement2.pause();
                    audioElement2.currentTime = 0;
                    connection.invoke("DeclineVideoCalling", username);
            }, 80000);
            
            

        });

       
       
    }

    function DeclineVideoCallRequest(username) {
        var declineButton = document.getElementById('cancel-call');
        declineButton.addEventListener('click', function () {
            var CallingPopupDiv = document.getElementById('customacceptVideoModal');
            var customMessageContentDiv1 = document.querySelector('.customMessageContent');
            customMessageContentDiv1.removeChild(CallingPopupDiv);
            var audioElement2 = document.getElementById("acceptingMessageAudio");
            audioElement2.pause();
            audioElement2.currentTime = 0;
            connection.invoke("DeclineVideoCallRequest", username);
            
        });
    }
    function generateGUID() {
        const cryptoObj = window.crypto || window.msCrypto;
        const array = new Uint16Array(8);
        cryptoObj.getRandomValues(array);
        return `${array[0]}-${array[1]}-${array[2]}-${array[3]}-${array[4]}-${array[5]}-${array[6]}-${array[7]}`;
    }
    function VideoCallingView(username) {
        var getAnswerButton = document.getElementById("accept-call");
        getAnswerButton.addEventListener('click', () => {

            const roomId = generateGUID();

            connection.invoke("JoinRoom", roomId, username);
            window.location.href = '/Messages/VideoRoom/' + roomId;

        });

    }

    function MyButtonEventListener() {
      document.querySelectorAll('#sendMessage').forEach(item => {
        item.addEventListener('click', function () {
            console.log("On Click isledi")
            var messagePart = document.getElementById('messageContent');
            var content = messagePart.value;
            console.log("Bura Click Edildi")
            var receiverInput = document.querySelectorAll(".receiverIdHiddeninput")[0];
            var receiverId = receiverInput.value;
            console.log(receiverId);

            var response = null;
            connection.invoke('SendMessageAsync', receiverId, content, response);



            messagePart.value = "";
            messagePart.focus();
        });
    });
       

    }


    function ShowTyping() {
        var messageContentInput = document.getElementById('messageContent');
        messageContentInput.addEventListener('keydown', () => {
            var receiverInput1 = document.querySelectorAll(".receiverIdHiddeninput")[0];
            var receiverId1 = receiverInput1.value;
            setTimeout(connection.invoke("Typing", receiverId1, true),4000)
        });
        messageContentInput.addEventListener('keyup', () => {
            var receiverInput1 = document.querySelectorAll(".receiverIdHiddeninput")[0];
            var receiverId1 = receiverInput1.value;
            setTimeout(connection.invoke("Typing", receiverId1, false),2000)
            
        });
    }




    connection.on('showtousertyping', (ProfilePicture, UserName,isTyping) => {
       
        var typingLi5 = document.getElementById("customTyping");
        if (isTyping) {
            typingLi5.style.display = 'block';
        }
        else {
            typingLi5.style.display = 'none';
        }


    });

    connection.on('seeSendMessage', (SenderUsername, SenderProfilePicture, hour, content, imagePath) => {
            var messageContentUlElement2 = document.querySelector('.mainMessageContentList');
         if (content == null) {
             var newMessageLi2 = document.createElement("li");

             newMessageLi2.innerHTML = `
     
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${SenderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${hour}</span></p>
                             <br/>
                    <figure>
        <img src="/${imagePath}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
    </figure>
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${SenderUsername}</div>
                </div>
            </div>
      
    `;
         }
         else if (imagePath == null) {
             var newMessageLi2 = document.createElement("li");

             newMessageLi2.innerHTML = `
       
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${SenderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            <p class="mb-0">
                                ${content}
                            </p>
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${hour}</span></p>
             
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${SenderUsername}</div>
                </div>
            </div>
     
    `;
         }
         else {
             var newMessageLi2 = document.createElement("li");

             newMessageLi2.innerHTML = `
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${SenderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            <p class="mb-0">
                                ${content}
                            </p>
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${hour}</span></p>
                             <br/>
                    <figure>
        <img src="/${imagePath}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
    </figure>
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${SenderUsername}</div>
                </div>
            </div>
      
    `;
         }
        var typingLi2 = document.getElementById("customTyping");
        messageContentUlElement2.insertBefore(newMessageLi2, typingLi2);

            var audioElement2 = document.getElementById("receiveMessageAudio");
            audioElement2.currentTime = 0; // Sesi sıfırla (eğer zaten çalıyorsa)
            audioElement2.play(); // Sesi çal
        });

    connection.on('seeMySendMessage', (SenderUsername, SenderProfilePicture, content, hour, imagePath) => {
        var messageContentUlElement1 = document.querySelector('.mainMessageContentList');
       
        
        if (content == null) {
            var newMessageLi1 = document.createElement("li");
            newMessageLi1.className = "right";
            newMessageLi1.innerHTML = `
      
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${SenderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${hour}</span></p>
                             <br/>
                    <figure>
        <img src="/${imagePath}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
    </figure>
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${SenderUsername}</div>
                </div>
            </div>
      
    `;
        }
        else if (imagePath == null) {
            var newMessageLi1 = document.createElement("li");
            newMessageLi1.className = "right";
            newMessageLi1.innerHTML = `
      
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${SenderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            <p class="mb-0">
                                ${content}
                            </p>
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${hour}</span></p>
             
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${SenderUsername}</div>
                </div>
            </div>
        
    `;
        }
        else {
            var newMessageLi1 = document.createElement("li");
            newMessageLi1.className = "right";
            newMessageLi1.innerHTML = `
       
            <div class="conversation-list">
                <div class="chat-avatar">
                    <img src="/${SenderProfilePicture}" alt="">
                </div>

                <div class="user-chat-content">
                    <div class="ctext-wrap">
                        <div class="ctext-wrap-content">
                            <p class="mb-0">
                                ${content}
                            </p>
                            <p class="chat-time mb-0"><i class="ri-time-line align-middle"></i> <span class="align-middle">${hour}</span></p>
                             <br/>
                    <figure>
        <img src="/${imagePath}" class="img-fluid rounded-3" style="max-width: 100px; max-height: 100px;" alt="image">
    </figure>
                        </div>

                        <div class="dropdown align-self-start">
                            <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="ri-more-2-fill"></i>
                            </a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Copy <i class="ri-file-copy-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Save <i class="ri-save-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Forward <i class="ri-chat-forward-line float-end text-muted"></i></a>
                                <a class="dropdown-item" href="#">Delete <i class="ri-delete-bin-line float-end text-muted"></i></a>
                            </div>
                        </div>
                    </div>

                    <div class="conversation-name">${SenderUsername}</div>
                </div>
            </div>
       
    `;
        }
        var typingLi1 = document.getElementById("customTyping");
        messageContentUlElement1.insertBefore(newMessageLi1, typingLi1);
        

        var audioElement1 = document.getElementById("sentMessageAudio");
        console.log(audioElement1)
        audioElement1.currentTime = 0; // Sesi sıfırla (eğer zaten çalıyorsa)
        audioElement1.play();
        });

    connection.on("showCallingUserPopup", (UserName, ProfilePicture) => {

        var customMessageContentDiv = document.querySelector('.customMessageContent');

        var callingDiv = document.createElement('div');

        callingDiv.id = 'customacceptVideoModal';
        callingDiv.className = 'popup';


        var callingDivContent = `
        <div class="popup-content">
            <div class="circle-image" id="callingImage">
                <img src="/${ProfilePicture}" />
            </div>
            <br />
            <p>${UserName}</p>
            <br/>
            <p>Chatverse video çağrısı</p>
            <div class="button-container">
                <button id="accept-call" class="circular-button ">
                    <i class="mdi mdi-video" style="color: white; font-size: 28px;"></i>
                </button>

                <button id="cancel-call" class="circular-button">
                    <i class="mdi mdi-phone-hangup" style="color: white; font-size: 28px;"></i>
                </button>
            </div>
        </div>`;
        var ses = new Audio("~/sound/accepting.mp3");
        ses.play();
        ses.addEventListener('ended', function () {
            this.currentTime = 2;
            this.play();
        });
        callingDiv.innerHTML = callingDivContent;
        customMessageContentDiv.appendChild(callingDiv);

        setTimeout(DeclineVideoCallRequest(UserName), 1500);
        setTimeout(VideoCallingView(UserName), 1200);

    });
    
    connection.on("removeVideoCallingRequest", () => {
        var customMessageContentDiv = document.querySelector('.customMessageContent');
        var videoCallingRequestDiv = document.getElementById('customacceptVideoModal');
        customMessageContentDiv.removeChild(videoCallingRequestDiv);
        var audioElement2 = document.getElementById("acceptingMessageAudio");
        audioElement2.pause();
        audioElement2.currentTime = 0;
    });

    connection.on("removeVideoCallerRequest", () => {
        var customMessageContentDiv = document.querySelector('.customMessageContent');
        var videoCallingRequestDiv = document.getElementById('customVideoModal');
        customMessageContentDiv.removeChild(videoCallingRequestDiv);
        var audioElement2 = document.getElementById("callingMessageAudio");
        audioElement2.pause();
        audioElement2.currentTime = 0;
    });


    connection.on("user-connected", (roomId) => {
        window.location.href = '/Messages/VideoRoom/' + roomId;
    });
   

    window.addEventListener("unload", function (event) {
        connection.stop();
    });
    window.addEventListener("beforeunload", function (event) {
        connection.stop();
    });


}
