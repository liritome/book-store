const form = document.querySelector("form");
const scheduledRadio = document.getElementById("scheduledAtRadio");
const scheduledAt = document.getElementById("scheduledAt");
const numberInSeriesInput = document.getElementById("numberInSeriesInput");
const seriesSelect = document.getElementById("seriesSelect");
const hasEbookCheck = document.getElementById("hasDigitalBook");
const ebookDetails = document.getElementById("digitalBookDetails");
const ebookStockAndPricing = document.getElementById("digitalBookStockAndPricing");
const priceInput = document.getElementById("priceInput");
const discountSelect = document.getElementById("discountSelect");
const finalPriceInput = document.getElementById("finalPriceInput");
const digitalPriceInput = document.getElementById("digitalPriceInput");
const digitalDiscountSelect = document.getElementById("digitalDiscountSelect");
const digitalFinalPriceInput = document.getElementById("digitalFinalPriceInput");

document.querySelectorAll('input[name="IsVisible"]').forEach(radio => {
    radio.addEventListener("change", () => {
        if (scheduledRadio.checked) {
            scheduledAt.disabled = false;
        }
        else {
            scheduledAt.value = "";
            scheduledAt.disabled = true;
        }
    });
});

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
        scheduledAt.disabled = !scheduledRadio.checked;
        numberInSeriesInput.disabled = !seriesSelect.value;
        discountSelect.disabled = isNaN(parseFloat(priceInput.value));
        digitalDiscountSelect.disabled = isNaN(parseFloat(digitalPriceInput.value));
        ebookDetails.classList.toggle("d-none", !hasEbookCheck.checked);
        ebookStockAndPricing.classList.toggle("d-none", !hasEbookCheck.checked);
    }, 0);
});

function toggleEbookSectionsVisibility() {
    ebookDetails.classList.toggle("d-none", !hasEbookCheck.checked);
    ebookStockAndPricing.classList.toggle("d-none", !hasEbookCheck.checked);
}

toggleEbookSectionsVisibility();
hasEbookCheck.addEventListener("change", toggleEbookSectionsVisibility);

function updateFinalPrice(priceInput, discountSelect, finalPriceInput) {

    let price = parseFloat(priceInput.value);
    let finalPrice = price;
    let discountType = discountSelect.selectedOptions[0].dataset.type;
    let discountValue = parseInt(discountSelect.selectedOptions[0].dataset.value);

    if (isNaN(price)) {
        finalPriceInput.value = "";
        discountSelect.value = "";
        discountSelect.disabled = true;
        return;
    }
    else {
        discountSelect.disabled = false;
    }

    if (discountSelect.value) {
        if (discountType == "AmountDiscount") {
            finalPrice = Math.max(price - discountValue, 1);
        }
        else {
            finalPrice = Math.round(price * (1 - discountValue / 100));
        }
    }
    finalPriceInput.value = finalPrice;
}

priceInput.addEventListener("input", () => updateFinalPrice(priceInput, discountSelect, finalPriceInput));
digitalPriceInput.addEventListener("input", () => updateFinalPrice(digitalPriceInput, digitalDiscountSelect, digitalFinalPriceInput));
discountSelect.addEventListener("change", () => updateFinalPrice(priceInput, discountSelect, finalPriceInput));
digitalDiscountSelect.addEventListener("change", () => updateFinalPrice(digitalPriceInput, digitalDiscountSelect, digitalFinalPriceInput));