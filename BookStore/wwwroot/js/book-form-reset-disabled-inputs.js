const form = document.querySelector("form");

form.addEventListener("reset", function () {
    setTimeout(() => {
        scheduledAt.disabled = !scheduledRadio.checked;
        numberInSeriesInput.disabled = !seriesSelect.value;
        discountSelect.disabled = isNaN(parseFloat(priceInput.value));
    }, 0);
});