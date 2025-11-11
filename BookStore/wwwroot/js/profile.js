document.addEventListener("DOMContentLoaded", function () {
    const profileBtn = document.getElementById("profileBtn");
    const profile = document.getElementById("profile");
    const overlay = document.getElementById("overlay");
    const profileCloseBtn = document.getElementById("profileCloseBtn");

    profileBtn.addEventListener("click", function () {
        profile.classList.remove("d-none");
        overlay.classList.remove("d-none");
        document.body.style.overflow = "hidden";
    });

    [overlay, profileCloseBtn].forEach(el => {
        el?.addEventListener("click", function () {
            profile.classList.add("d-none");
            overlay.classList.add("d-none");
            document.body.style.overflow = "";
        });
    });
});