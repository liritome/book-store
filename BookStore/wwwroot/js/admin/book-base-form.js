const numberInSeriesInput = document.getElementById("numberInSeriesInput");
const seriesSelect = document.getElementById("seriesSelect");

function toggleNumberInSeriesInput() {
    if (seriesSelect.value) {
        numberInSeriesInput.disabled = false;
    }
    else {
        numberInSeriesInput.value = "";
        numberInSeriesInput.disabled = true;
    }
}

seriesSelect.addEventListener("change", toggleNumberInSeriesInput);
toggleNumberInSeriesInput();

form.addEventListener("reset", function () {
    setTimeout(() => {
        numberInSeriesInput.disabled = !seriesSelect.value;
    }, 0);
});