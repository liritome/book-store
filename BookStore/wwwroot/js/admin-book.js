const form = document.querySelector("form");
const priceInput = document.getElementById("priceInput");
const discountSelect = document.getElementById("discountSelect");
const finalPriceInput = document.getElementById("finalPriceInput");

form.addEventListener("reset", function () {
    setTimeout(() => {
        discountSelect.disabled = isNaN(parseFloat(priceInput.value));
    }, 0);
});

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
discountSelect.addEventListener("change", () => updateFinalPrice(priceInput, discountSelect, finalPriceInput));