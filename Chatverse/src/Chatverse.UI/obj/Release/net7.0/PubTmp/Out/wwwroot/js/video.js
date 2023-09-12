"use strict";
var localroomID = localStorage.getItem("RoomID");

const ROOM_ID = localroomID;
let userId = null;
let localStream = null;
const Peers = {}

const connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5273/call").build();

const myPeer = new Peer();
myPeer.on('open', id => {
    userId = id;
    const startSignalR = async () => {
        await connection.start();
        await connection.invoke("JoinRoom", ROOM_ID, userId)
    }
    startSignalR();
})

const videoGrid = document.querySelector("[video-grid]");
const myVideo = document.createElement("video");
myVideo.muted = true;

navigator.mediaDevices.getUserMedia({
    audio: true,
    video: true

}).then(stream => {
    addVideoStream(myVideo, stream);
    localStream = stream
});

connection.on("user-connected", id => {
    if (userId === id) {
        console.log("idler eynidi");
        return;
    } 
    console.log(`User connected: ${userId}`)
    connectNewUser(id, localStream)
})

connection.on("user-disconnected", id => {
    console.log(`User disconnected: ${userId}`)
    if (Peers[id]) Peers[id].close();
})


myPeer.on('call', call => {
    call.answer(localStream);

    const userVideo = document.createElement('video');

    call.on('stream', userVideoStream => {
        addVideoStream(userVideo, userVideoStream)
    })

})

const addVideoStream = (video, stream) => {
    video.srcObject = stream;
    video.addEventListener("loadedmetadata", () => {
        video.play();
    })
    videoGrid.appendChild(video);
}

const connectNewUser = (userId, localStream) => {
    const userVideo = document.createElement('video');

    const call = myPeer.call(userId, localStream)
    call.on('stream', userVideoStream => {
        addVideoStream(userVideo, userVideoStream);
    })

    call.on('close', () => {
        userVideo.remove()
    })
    Peers[userId] = call
}

