var lastScrollTop = 0;
var isUpScrolling = false;

export function handleScrollEvent() {
    var messagesContainer = document.getElementById("messages-container");

    messagesContainer.addEventListener('scroll', (event) => {
        var scrollTop = window.pageYOffset || messagesContainer.scrollTop;
        if (scrollTop > lastScrollTop) {
            isUpScrolling = false;
        } else {
            isUpScrolling = true;
        }
        lastScrollTop = scrollTop <= 0 ? 0 : scrollTop; // For Mobile or negative scrolling
    });
}


export function isScrollUp() {
    return isUpScrolling;
}

export function hasScrolledToTop() {
    var messagesContainer = document.getElementById("messages-container");
    return messagesContainer.scrollTop == 0;
}

export function getMessage() {
    let inputElement = document.getElementById('message-box');
    let text = '';
    if (inputElement != null) {
        text = inputElement.value;
        document.getElementById('message-box').value = '';
    }

    return text;
}

export function scrollToBottom() {
    var messagesContainer = document.getElementById("messages-container");

    if ((messagesContainer.scrollHeight - messagesContainer.clientHeight) > 0) {
        messagesContainer.scrollTop = messagesContainer.scrollHeight;// - messagesContainer.clientHeight;
    }
    else {
        messagesContainer.scrollTop = 200;
    }
}
