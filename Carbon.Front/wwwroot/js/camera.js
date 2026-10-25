// Funções da webcam usadas pela tela de verificação facial (VerificacaoFacial.razor)
window.carbonCamera = {
    start: async function (video) {
        const stream = await navigator.mediaDevices.getUserMedia({
            video: { width: { ideal: 640 }, height: { ideal: 480 }, facingMode: "user" },
            audio: false
        });
        video.srcObject = stream;
        await video.play();
    },

    // Tira a foto e devolve em JPEG base64 ("data:image/jpeg;base64,...")
    capture: function (video) {
        const maxWidth = 640;
        const scale = Math.min(1, maxWidth / video.videoWidth);
        const canvas = document.createElement("canvas");
        canvas.width = Math.round(video.videoWidth * scale);
        canvas.height = Math.round(video.videoHeight * scale);
        canvas.getContext("2d").drawImage(video, 0, 0, canvas.width, canvas.height);
        return canvas.toDataURL("image/jpeg", 0.9);
    },

    stop: function (video) {
        if (video && video.srcObject) {
            video.srcObject.getTracks().forEach(t => t.stop());
            video.srcObject = null;
        }
    }
};
