const scheduledRadio = document.getElementById("scheduledAtRadio");
const scheduledAt = document.getElementById("scheduledAt");

document.querySelectorAll('input[name="IsVisible"]').forEach(radio => {
    radio.addEventListener('change', () => {
        if (scheduledRadio.checked) {
            scheduledAt.disabled = false;
        }
        else {
            scheduledAt.value = "";
            scheduledAt.disabled = true;
        }
    });
});