const hasEbookCheck = document.getElementById("hasDigitalBook");
const ebookDetails = document.getElementById("digitalBookDetails");
const ebookStockAndPricing = document.getElementById("digitalBookStockAndPricing");

function toggleEbookSectionsVisibility() {
    const isChecked = hasEbookCheck.checked;
    ebookDetails.classList.toggle("d-none", !isChecked);
    ebookStockAndPricing.classList.toggle("d-none", !isChecked);
}

toggleEbookSectionsVisibility();

hasEbookCheck.addEventListener("change", toggleEbookSectionsVisibility);