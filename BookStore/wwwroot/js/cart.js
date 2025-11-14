document.addEventListener("DOMContentLoaded", function () {
    const cartBtn = document.getElementById("cartBtn");
    const cart = document.getElementById("cart");
    const overlay = document.getElementById("overlay");
    const cartCloseBtn = document.getElementById("cartCloseBtn");

    cartBtn.addEventListener("click", function () {
        cart.classList.remove("d-none");
        overlay.classList.remove("d-none");
        document.body.style.overflow = "hidden";
    });

    [overlay, cartCloseBtn].forEach(el => {
        el?.addEventListener("click", function () {
            cart.classList.add("d-none");
            overlay.classList.add("d-none");
            document.body.style.overflow = "";
        });
    });
});