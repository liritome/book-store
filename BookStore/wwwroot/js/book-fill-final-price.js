const priceInput = document.getElementById("priceInput");
const discountSelect = document.getElementById("discountSelect");
const finalPriceInput = document.getElementById("finalPriceInput");

function updateFinalPrice() {

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

priceInput.addEventListener("input", updateFinalPrice);
discountSelect.addEventListener("change", updateFinalPrice);