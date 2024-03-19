// Import the functions you need from the SDKs you need
import { initializeApp } from "https://www.gstatic.com/firebasejs/10.8.0/firebase-app.js";
import {
    getAuth,
    GoogleAuthProvider,
    OAuthProvider,
    signInWithPopup,
} from "https://www.gstatic.com/firebasejs/10.8.0/firebase-auth.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
    apiKey: "AIzaSyCxwTXARl9VufvdK-feGDXSBqMhRHcxnSM",
    authDomain: "streamit-movie-mvc.firebaseapp.com",
    projectId: "streamit-movie-mvc",
    storageBucket: "streamit-movie-mvc.appspot.com",
    messagingSenderId: "764205575210",
    appId: "1:764205575210:web:64a10d40050e088b7de753",
    measurementId: "G-E5EN9EB8RT",
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const auth = getAuth(app);
auth.languageCode = "en";


const googleLogin = document.getElementById("google-login-btn");
    googleLogin.addEventListener("click", () => {
    const provider = new GoogleAuthProvider();
    signInWithPopup(auth, provider)
        .then((result) => {
            // This gives you a Google Access Token. You can use it to access the Google API.
            const credential = GoogleAuthProvider.credentialFromResult(result);
            const user = result.user;
            //get user data
            var data = {
                AccessToken: user.stsTokenManager.accessToken,
                RefreshToken: user.stsTokenManager.refreshToken,
                DisplayName: user.displayName,
                Email: user.email,
                Avatar: user.photoURL
            };
            //fetch to server
            fetchToServer(data, "LoginWithGoogle");
            
        })
        .catch((error) => {
            // Handle Errors here.
            const errorCode = error.code;
            const errorMessage = error.message;
        });
});

const microsoftLogin = document.getElementById("microsoft-login-btn");
microsoftLogin.addEventListener("click", () => {
    const provider = new OAuthProvider('microsoft.com');
    signInWithPopup(auth, provider)
        .then((result) => {
            // This gives you a Google Access Token. You can use it to access the Google API.
            const credential = GoogleAuthProvider.credentialFromResult(result);
            const user = result.user;
            //get user data
            var data = {
                AccessToken: user.stsTokenManager.accessToken,
                RefreshToken: user.stsTokenManager.refreshToken,
                DisplayName: user.displayName,
                Email: user.email,
                Avatar: user.photoURL
            };

            //fetch to server
            fetchToServer(data, "LoginWithMicrosoft");

        })
        .catch((error) => {
            // Handle Errors here.
            const errorCode = error.code;
            const errorMessage = error.message;
        });
});

//fetch to server
function fetchToServer(data, method) {
    $.ajax({
        type: "POST",
        url: "Login/" + method, // Thay đổi đường dẫn tới Controller và Action cần chuyển hướng đến
        data: data,
        success: function (response) {
            // Xử lý kết quả phản hồi từ Controller (nếu cần)
            window.location.href = "/Home";
            window.onload = function () {
                createSuccess(response);
            };
        },
        error: function (error) {
            // Xử lý lỗi (nếu có)
            console.log(error);
        }
    });
}

function getCookie(name) {
    var cookieName = name + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var cookieArray = decodedCookie.split(';');

    for (var i = 0; i < cookieArray.length; i++) {
        var cookie = cookieArray[i];
        while (cookie.charAt(0) === ' ') {
            cookie = cookie.substring(1);
        }

        if (cookie.indexOf(cookieName) === 0) {
            return cookie.substring(cookieName.length, cookie.length);
        }
    }

    return "";
}